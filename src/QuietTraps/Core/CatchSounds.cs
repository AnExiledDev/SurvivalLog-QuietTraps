using System;
using System.Collections.Generic;

namespace QuietTraps.Core;

/// <summary>Which catch sounds to mute. The game has three: two traps have their own, the other three share one.</summary>
public sealed record MuteChoice(bool LiveCaptureCage, bool SimpleBirdNet, bool OtherTraps);

/// <summary>
/// The sounds a trap loops while it holds a catch. The game picks them by trap id in TrapManager.GetCaptureSoundPath:
/// id 2 and id 3 have their own, and every other trap gets the shared one. Setting, moving and checking a trap play
/// other sounds, which are never on this list.
/// </summary>
public static class CatchSounds
{
    public const string LiveCaptureCage = "event:/UI/xianjing_bushoulong";
    public const string SimpleBirdNet = "event:/UI/xianjing_buniaolong";

    /// <summary>Played by the Simple Mousetrap, the Iron Animal Trap and the Auto Trap Cage.</summary>
    public const string OtherTraps = "event:/UI/xianjing_bushujia02";

    /// <summary>
    /// The Iron Animal Trap's rattle, on top of the shared sound. Not in the game's code: its caught model plays it, so
    /// it was found with the sound log, starting together with the other catch sounds when a game loads.
    /// </summary>
    public const string IronAnimalTrapRattle = "event:/UI/xianjing_Metalbushujia";

    public static IReadOnlySet<string> PathsToMute(MuteChoice choice)
    {
        var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (choice.LiveCaptureCage) paths.Add(LiveCaptureCage);
        if (choice.SimpleBirdNet) paths.Add(SimpleBirdNet);
        if (choice.OtherTraps)
        {
            paths.Add(OtherTraps);
            paths.Add(IronAnimalTrapRattle);
        }

        return paths;
    }

    public static bool ShouldMute(string? eventPath, IReadOnlySet<string> mutedPaths) =>
        eventPath is not null && mutedPaths.Contains(eventPath);
}
