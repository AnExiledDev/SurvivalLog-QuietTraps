using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using ExiledToolkit;
using ExiledToolkit.Game;
using ExiledToolkit.Kit;
using HarmonyLib;
using QuietTraps.Adapters;
using QuietTraps.Core;

namespace QuietTraps;

[BepInPlugin(Guid, "Quiet Traps", Version)]
[BepInDependency(ExiledToolkit.Plugin.Guid, ">=0.19.0")]
public sealed class Plugin : BasePlugin
{
    private const string Guid = "com.dev.survivallog.quiettraps";
    private const string Version = "0.2.3";

    /// <summary>The sound log keeps earlier game starts, so past 1 MB the file is moved aside and started again.</summary>
    private const long MaxLogBytes = 1024 * 1024;

    private bool _hasMarkedSoundLog;

    public override void Load()
    {
        var liveCaptureCage = BindOneTrap("LiveCaptureCage", "Live Capture Cage", "「活捉笼」");
        var simpleBirdNet = BindOneTrap("SimpleBirdNet", "Simple Bird Net", "「简易捕鸟网」");
        var otherTraps = Bind("OtherTraps",
            "When true, the sound the Simple Mousetrap, Iron Animal Trap and Auto Trap Cage make while they hold a catch is muted, along with the Iron Animal Trap's rattle. The game gives all three traps the same sound, so one setting covers them. Every other trap sound still plays.",
            "设为 true 时，「简易捕鼠夹」、「铁制捕兽夹」和「自动诱捕笼」捕获猎物后发出的声音会被静音，「铁制捕兽夹」的哐啷声也会被静音。游戏里这三种陷阱用的是同一个声音，所以共用这一项设置。其他陷阱声音照常播放。");
        var isLoggingSounds = Config.Bind("Diagnostics", "LogSounds", false,
            new ConfigDescription("When true, the first three times each sound plays are written to BepInEx\\quiet-traps.log with the time. Sounds this mod muted are marked (muted). Turn it on to find a trap sound that still plays.\n" +
            "设为 true 时，每个声音的前三次播放会连同时间写入 BepInEx\\quiet-traps.log，被本模组静音的声音会标注 (muted)。如果有陷阱声音仍在播放，可以打开它来查找。", null, "ModManager.Advanced"));
        // The file is the sound log, so it stays off until LogSounds turns it on; every line still goes to the BepInEx log.
        var log = new ModLog(Log, "quiet-traps.log") { MaxFileBytes = MaxLogBytes, IsFileOn = false };
        var patches = new PatchKit(new Harmony(Guid), log.Write);

        log.Write($"===== Quiet Traps {Version} started");
        SoundGate.Apply(patches);
        InstanceGate.Apply(patches);
        LiveSettings.Watch(() => Mute(new MuteChoice(liveCaptureCage.Value, simpleBirdNet.Value, otherTraps.Value)), liveCaptureCage, simpleBirdNet, otherTraps);
        LiveSettings.Watch(isLoggingSounds, isOn => LogSounds(isOn, log));
    }

    private void Mute(MuteChoice choice)
    {
        var mutedPaths = CatchSounds.PathsToMute(choice);

        SoundGate.Mute(mutedPaths);
        InstanceGate.Mute(mutedPaths);
        Log.LogInfo($"muting {mutedPaths.Count} of 4 trap sounds");
    }

    private void LogSounds(bool isOn, ModLog log)
    {
        log.IsFileOn = isOn;

        if (!isOn)
        {
            InstanceGate.LogSounds(null, _ => { });

            return;
        }

        if (!_hasMarkedSoundLog)
        {
            _hasMarkedSoundLog = true;
            log.WriteFileOnly($"===== Quiet Traps {Version} started");
        }

        InstanceGate.LogSounds(new SoundSightings(), log.WriteFileOnly);
    }

    private ConfigEntry<bool> BindOneTrap(string key, string englishTrap, string chineseTrap) =>
        Bind(key,
            $"When true, the sound the {englishTrap} makes while it holds a catch is muted. Every other trap sound still plays.",
            $"设为 true 时，{chineseTrap}捕获猎物后发出的声音会被静音。其他陷阱声音照常播放。");

    private ConfigEntry<bool> Bind(string key, string english, string chinese) =>
        Config.Bind("Mute", key, true, english + "\n" + chinese);
}
