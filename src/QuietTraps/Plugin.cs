using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using ExiledToolkit;
using HarmonyLib;
using QuietTraps.Adapters;
using QuietTraps.Core;

namespace QuietTraps;

[BepInPlugin(Guid, "Quiet Traps", Version)]
[BepInDependency(ExiledToolkit.Plugin.Guid, ">=0.4.0")]
public sealed class Plugin : BasePlugin
{
    private const string Guid = "com.dev.survivallog.quiettraps";
    private const string Version = "0.2.0";

    private SoundLog? _soundLog;

    public override void Load()
    {
        var liveCaptureCage = BindOneTrap("LiveCaptureCage", "Live Capture Cage", "「活捉笼」");
        var simpleBirdNet = BindOneTrap("SimpleBirdNet", "Simple Bird Net", "「简易捕鸟网」");
        var otherTraps = Bind("OtherTraps",
            "When true, the catch sound the Simple Mousetrap, Iron Animal Trap and Auto Trap Cage share is muted, and so is the Iron Animal Trap's rattle. The game gives all three the same sound, so they share this setting. Every other trap sound still plays.",
            "设为 true 时，「简易捕鼠夹」、「铁制捕兽夹」和「自动诱捕笼」捕获猎物后发出的声音会被静音（游戏中这三种陷阱使用同一个声音），「铁制捕兽夹」的哐啷声也会被静音。其他陷阱声音照常播放。");
        var isLoggingSounds = Config.Bind("Diagnostics", "LogSounds", false,
            "When true, the first three times each sound starts are written to BepInEx\\quiet-traps.log with the time, marked (muted) when this mod silenced it. Turn it on to find a trap sound that still plays.\n" +
            "设为 true 时，每个声音的前三次播放都会连同时间写入 BepInEx\\quiet-traps.log，被本模组静音的会标注 (muted)。想找出仍在播放的陷阱声音时再打开。");
        var harmony = new Harmony(Guid);

        Log.LogInfo($"===== Quiet Traps {Version} started");
        SoundGate.Apply(harmony, Log);
        InstanceGate.Apply(harmony, Log);
        LiveSettings.Watch(() => Mute(new MuteChoice(liveCaptureCage.Value, simpleBirdNet.Value, otherTraps.Value)), liveCaptureCage, simpleBirdNet, otherTraps);
        LiveSettings.Watch(isLoggingSounds, LogSounds);
    }

    private void Mute(MuteChoice choice)
    {
        var mutedPaths = CatchSounds.PathsToMute(choice);

        SoundGate.Mute(mutedPaths);
        InstanceGate.Mute(mutedPaths);
        Log.LogInfo($"muting {mutedPaths.Count} of 4 trap sounds");
    }

    private void LogSounds(bool isOn)
    {
        if (!isOn)
        {
            InstanceGate.LogSounds(null, _ => { });

            return;
        }

        if (_soundLog is null)
        {
            _soundLog = new SoundLog(Path.Combine(Paths.BepInExRootPath, "quiet-traps.log"));
            _soundLog.Write($"===== Quiet Traps {Version} started");
        }

        InstanceGate.LogSounds(new SoundSightings(), _soundLog.Write);
    }

    private ConfigEntry<bool> BindOneTrap(string key, string englishTrap, string chineseTrap) =>
        Bind(key,
            $"When true, the sound the {englishTrap} makes while it holds a catch is muted. Every other trap sound still plays.",
            $"设为 true 时，{chineseTrap}捕获猎物后发出的声音会被静音。其他陷阱声音照常播放。");

    private ConfigEntry<bool> Bind(string key, string english, string chinese) =>
        Config.Bind("Mute", key, true, english + "\n" + chinese);
}
