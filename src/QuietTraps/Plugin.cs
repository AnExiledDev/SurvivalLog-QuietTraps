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
    private const string Version = "0.1.0";

    public override void Load()
    {
        var choice = new MuteChoice(
            Bind("LiveCaptureCage", "Live Capture Cage", "「活捉笼」"),
            Bind("SimpleBirdNet", "Simple Bird Net", "「简易捕鸟网」"),
            Bind("OtherTraps", "Simple Mousetrap, Iron Animal Trap and Auto Trap Cage (the game gives all three the same sound)", "「简易捕鼠夹」、「铁制捕兽夹」和「自动诱捕笼」（游戏中这三种使用同一个声音）"));
        var mutedPaths = CatchSounds.PathsToMute(choice);

        Log.LogInfo($"===== Quiet Traps {Version} started, muting {mutedPaths.Count} of 3 catch sounds");
        SoundGate.Apply(new Harmony(Guid), mutedPaths, Log);
    }

    private bool Bind(string key, string englishTraps, string chineseTraps) =>
        Config.Bind("Mute", key, true,
            $"When true, the sound the {englishTraps} makes while it holds a catch is muted. Every other trap sound still plays.\n" +
            $"设为 true 时，{chineseTraps}捕获猎物后发出的声音会被静音。其他陷阱声音照常播放。").Value;
}
