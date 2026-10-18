using System;
using System.Collections.Generic;
using BepInEx.Logging;
using FMOD;
using FMOD.Studio;
using HarmonyLib;
using QuietTraps.Core;

namespace QuietTraps.Adapters;

/// <summary>
/// A postfix on FMOD's EventDescription.createInstance, which every sound passes through, including the ones a model
/// plays on its own without going through the game's FmodManager. A muted sound gets its volume set to zero, and with
/// logging on, the first starts of each path go to the sound log.
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

    /// <param name="sightings">Null when sound logging is off.</param>
    public static void Apply(Harmony harmony, IReadOnlySet<string> mutedPaths, SoundSightings? sightings, Action<string> write, ManualLogSource log)
    {
        _mutedPaths = mutedPaths;
        _sightings = sightings;
        _write = write;
        _log = log;

        try
        {
            var original = AccessTools.Method(typeof(EventDescription), nameof(EventDescription.createInstance))
                ?? throw new MissingMethodException(nameof(EventDescription), nameof(EventDescription.createInstance));

            harmony.Patch(original, postfix: new HarmonyMethod(typeof(InstanceGate), nameof(SilenceIfMuted)));
            log.LogInfo("hooked EventDescription.createInstance");
        }
        catch (Exception ex)
        {
            log.LogWarning("could not hook EventDescription.createInstance: " + ex.Message);
        }
    }

    private static void SilenceIfMuted(ref EventInstance __0)
    {
        try
        {
            var path = PathOf(__0);

            if (path is null) return;

            var isMuted = CatchSounds.ShouldMute(path, _mutedPaths);

            if (isMuted) __0.setVolume(0f);
            if (_sightings?.ShouldLog(path) == true) _write($"started {path}{(isMuted ? " (muted)" : "")}");
        }
        catch (Exception ex)
        {
            ReportFailureOnce(ex);
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
