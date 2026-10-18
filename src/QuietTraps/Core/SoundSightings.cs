using System;
using System.Collections.Generic;

namespace QuietTraps.Core;

/// <summary>
/// Decides which sound starts the log records: the first few of each path, so a sound that repeats shows how often
/// it fires without flooding the file.
/// </summary>
public sealed class SoundSightings
{
    public const int PerPath = 3;

    private readonly Dictionary<string, int> _counts = new(StringComparer.OrdinalIgnoreCase);

    public bool ShouldLog(string eventPath)
    {
        _counts.TryGetValue(eventPath, out var seen);

        if (seen >= PerPath) return false;

        _counts[eventPath] = seen + 1;

        return true;
    }
}
