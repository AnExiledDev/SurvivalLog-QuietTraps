using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using QuietTraps.Adapters;
using QuietTraps.Core;

namespace QuietTraps;

[BepInPlugin(Guid, "Quiet Traps", Version)]
public sealed class Plugin : BasePlugin
{
    private const string Guid = "com.dev.survivallog.quiettraps";
    private const string Version = "0.1.2";

    public override void Load()
    {
        var choice = new MuteChoice(
            Bind("LiveCaptureCage", "Live Capture Cage", "「活捉笼」"),
            Bind("SimpleBirdNet", "Simple Bird Net", "「简易捕鸟网」"),
            Bind("OtherTraps", "Simple Mousetrap, Iron Animal Trap and Auto Trap Cage (the game gives all three the same sound; this also mutes the Iron Animal Trap's rattle)", "「简易捕鼠夹」、「铁制捕兽夹」和「自动诱捕笼」（游戏中这三种使用同一个声音；同时会静音「铁制捕兽夹」的晃动声）"));
        var isLoggingSounds = Config.Bind("Diagnostics", "LogSounds", false,
            "When true, the first three times each sound starts are written to BepInEx/quiet-traps.log with the time, marked (muted) when this mod silenced it. Turn it on to find a trap sound that still plays.\n" +
            "设为 true 时，每个声音的前三次播放都会连同时间写入 BepInEx/quiet-traps.log，被本模组静音的会标注 (muted)。想找出仍在播放的陷阱声音时再打开。").Value;
        var mutedPaths = CatchSounds.PathsToMute(choice);
        var harmony = new Harmony(Guid);

        Log.LogInfo($"===== Quiet Traps {Version} started, muting {mutedPaths.Count} of 4 catch sounds");
        SoundGate.Apply(harmony, mutedPaths, Log);
        StartInstanceGate(harmony, mutedPaths, isLoggingSounds);
    }

    private void StartInstanceGate(Harmony harmony, IReadOnlySet<string> mutedPaths, bool isLoggingSounds)
    {
        if (!isLoggingSounds)
        {
            InstanceGate.Apply(harmony, mutedPaths, null, _ => { }, Log);

            return;
        }

        var soundLog = new SoundLog(Path.Combine(Paths.BepInExRootPath, "quiet-traps.log"));

        soundLog.Write($"===== Quiet Traps {Version} started");
        InstanceGate.Apply(harmony, mutedPaths, new SoundSightings(), soundLog.Write, Log);
    }

    private bool Bind(string key, string englishTraps, string chineseTraps) =>
        Config.Bind("Mute", key, true,
            $"When true, the sound the {englishTraps} makes while it holds a catch is muted. Every other trap sound still plays.\n" +
            $"设为 true 时，{chineseTraps}捕获猎物后发出的声音会被静音。其他陷阱声音照常播放。").Value;
}
