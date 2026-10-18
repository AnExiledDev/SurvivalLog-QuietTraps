using System;
using System.IO;

namespace QuietTraps.Adapters;

/// <summary>The sound log file, started fresh each game start so it never grows. A write that fails is dropped.</summary>
internal sealed class SoundLog
{
    private readonly string _path;
    private readonly object _gate = new();

    public SoundLog(string path)
    {
        _path = path;
        Append("", isFresh: true);
    }

    public void Write(string message) => Append($"{DateTime.Now:HH:mm:ss.fff} {message}{Environment.NewLine}", isFresh: false);

    private void Append(string text, bool isFresh)
    {
        try
        {
            lock (_gate)
            {
                if (isFresh) File.WriteAllText(_path, text);
                else File.AppendAllText(_path, text);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
