using System;
using System.Collections.Generic;
using BepInEx.Logging;
using FMOD;
using FMOD.Studio;
using HarmonyLib;
using QuietTraps.Core;

namespace QuietTraps.Adapters;

/// <summary>
/// Hooks on FMOD's EventInstance.start and EventDescription.createInstance, which every sound passes through, including
/// the ones a model plays on its own without going through the game's FmodManager. A muted sound is never started, so
/// nothing it would trigger plays either; that is what silences it. Its volume is also set to zero when it is created,
/// as a fallback in case something starts it another way. With logging on, the first starts of each path go to the
/// sound log.
/// </summary>
internal static class InstanceGate
{
    // Harmony postfixes are static, so their state is too. FMOD instances are created on Unity's main thread.
    private static readonly Dictionary<IntPtr, string?> PathsByDescription = new();
    private static IReadOnlySet<string> _mutedPaths = new HashSet<string>();
    private static SoundSightings? _sightings;
    private static Action<string> _write = _ => { };
    private static ManualLogSource? _log;
    private static bool _hasReportedFailure;

    public static void Apply(Harmony harmony, ManualLogSource log)
    {
        _log = log;

        Hook(harmony, typeof(EventDescription), nameof(EventDescription.createInstance), postfix: nameof(SilenceIfMuted));
        Hook(harmony, typeof(EventInstance), nameof(EventInstance.start), prefix: nameof(SkipStartIfMuted));
    }

    public static void Mute(IReadOnlySet<string> mutedPaths) => _mutedPaths = mutedPaths;

    /// <param name="sightings">Null when sound logging is off.</param>
    public static void LogSounds(SoundSightings? sightings, Action<string> write)
    {
        _sightings = sightings;
        _write = write;
    }

    private static void Hook(Harmony harmony, Type type, string method, string? prefix = null, string? postfix = null)
    {
        var name = $"{type.Name}.{method}";

        try
        {
            var original = AccessTools.Method(type, method) ?? throw new MissingMethodException(type.Name, method);

            harmony.Patch(original,
                prefix: prefix is null ? null : new HarmonyMethod(typeof(InstanceGate), prefix),
                postfix: postfix is null ? null : new HarmonyMethod(typeof(InstanceGate), postfix));
            _log?.LogInfo("hooked " + name);
        }
        catch (Exception ex)
        {
            _log?.LogError($"could not hook {name}: {ex.Message}");
        }
    }

    private static void SilenceIfMuted(ref EventInstance __0)
    {
        try
        {
            var path = PathOf(__0);

            if (path is not null && CatchSounds.ShouldMute(path, _mutedPaths)) __0.setVolume(0f);
        }
        catch (Exception ex)
        {
            ReportFailureOnce(ex);
        }
    }

    /// <summary>Returning false skips the start and reports success, so the caller carries on as if the sound played.</summary>
    private static bool SkipStartIfMuted(ref EventInstance __instance, ref RESULT __result)
    {
        try
        {
            var path = PathOf(__instance);

            if (path is null) return true;

            var isMuted = CatchSounds.ShouldMute(path, _mutedPaths);

            if (_sightings?.ShouldLog(path) == true) _write($"started {path}{(isMuted ? " (muted)" : "")}");
            if (!isMuted) return true;

            __result = RESULT.OK;

            return false;
        }
        catch (Exception ex)
        {
            ReportFailureOnce(ex);

            return true;
        }
    }

    /// <summary>Cached per description: looking a path up asks FMOD for a string, and this runs on every sound.</summary>
    private static string? PathOf(EventInstance instance)
    {
        if (instance.getDescription(out var description) != RESULT.OK) return null;
        if (PathsByDescription.TryGetValue(description.handle, out var known)) return known;

        var path = description.getPath(out var found) == RESULT.OK ? found : null;

        PathsByDescription[description.handle] = path;

        return path;
    }

    private static void ReportFailureOnce(Exception ex)
    {
        if (_hasReportedFailure) return;

        _hasReportedFailure = true;
        _log?.LogWarning("sound check failed, further failures are not logged: " + ex.Message);
    }
}
