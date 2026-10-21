using System;
using System.Collections.Generic;
using BepInEx.Logging;
using GameCore.HotUpdate;
using HarmonyLib;
using QuietTraps.Core;

namespace QuietTraps.Adapters;

/// <summary>
/// Harmony prefixes on the two FmodManager calls a trap's catch sound goes through. A muted sound never starts, so the
/// game's later stop call finds nothing to stop. Each hook swallows its own exceptions: a sound mod must never be the crash.
/// </summary>
internal static class SoundGate
{
    // Harmony prefixes are static, so the plan they read is too. Set and read on the main thread only.
    private static IReadOnlySet<string> _mutedPaths = new HashSet<string>();
    private static ManualLogSource? _log;

    public static void Apply(Harmony harmony, ManualLogSource log)
    {
        _log = log;

        foreach (var name in new[] { "PlayEnv3dAction", "PlayBattle3dAction" })
        {
            Patch(harmony, name);
        }
    }

    public static void Mute(IReadOnlySet<string> mutedPaths) => _mutedPaths = mutedPaths;

    private static void Patch(Harmony harmony, string target)
    {
        try
        {
            var original = AccessTools.Method(typeof(FmodManager), target) ?? throw new MissingMethodException(nameof(FmodManager), target);

            harmony.Patch(original, prefix: new HarmonyMethod(typeof(SoundGate), nameof(SkipIfMuted)));
            _log?.LogInfo($"hooked FmodManager.{target}");
        }
        catch (Exception ex)
        {
            _log?.LogError($"could not hook FmodManager.{target}: {ex.Message}");
        }
    }

    /// <summary>The first argument of both targets is the event path. False skips the sound.</summary>
    private static bool SkipIfMuted(string __0)
    {
        try
        {
            return !CatchSounds.ShouldMute(__0, _mutedPaths);
        }
        catch (Exception ex)
        {
            _log?.LogWarning("sound check failed: " + ex.Message);

            return true;
        }
    }
}
