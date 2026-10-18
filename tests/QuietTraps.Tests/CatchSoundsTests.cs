using QuietTraps.Core;
using Xunit;

namespace QuietTraps.Tests;

public class CatchSoundsTests
{
    private const string SetTrapSound = "event:/UI/anzhuangxianjing";
    private static readonly MuteChoice All = new(true, true, true);

    [Fact]
    public void By_default_every_trap_catch_sound_is_muted()
    {
        var muted = CatchSounds.PathsToMute(All);

        Assert.True(CatchSounds.ShouldMute(CatchSounds.LiveCaptureCage, muted));
        Assert.True(CatchSounds.ShouldMute(CatchSounds.SimpleBirdNet, muted));
        Assert.True(CatchSounds.ShouldMute(CatchSounds.OtherTraps, muted));
        Assert.True(CatchSounds.ShouldMute(CatchSounds.IronAnimalTrapRattle, muted));
    }

    [Fact]
    public void Trap_sounds_that_are_not_a_catch_still_play()
    {
        var muted = CatchSounds.PathsToMute(All);

        Assert.False(CatchSounds.ShouldMute(SetTrapSound, muted));
        Assert.False(CatchSounds.ShouldMute("event:/UI/Event_zhuanggexianjing", muted));
        Assert.False(CatchSounds.ShouldMute(null, muted));
    }

    [Fact]
    public void Turning_one_trap_off_leaves_only_that_trap_audible()
    {
        var muted = CatchSounds.PathsToMute(new MuteChoice(LiveCaptureCage: true, SimpleBirdNet: false, OtherTraps: true));

        Assert.False(CatchSounds.ShouldMute(CatchSounds.SimpleBirdNet, muted));
        Assert.True(CatchSounds.ShouldMute(CatchSounds.LiveCaptureCage, muted));
        Assert.True(CatchSounds.ShouldMute(CatchSounds.OtherTraps, muted));
    }

    [Fact]
    public void Each_setting_mutes_its_own_sound_and_no_other()
    {
        Assert.Equal(new[] { CatchSounds.LiveCaptureCage }, CatchSounds.PathsToMute(new MuteChoice(true, false, false)));
        Assert.Equal(new[] { CatchSounds.SimpleBirdNet }, CatchSounds.PathsToMute(new MuteChoice(false, true, false)));
        Assert.Equal(new[] { CatchSounds.OtherTraps, CatchSounds.IronAnimalTrapRattle }, CatchSounds.PathsToMute(new MuteChoice(false, false, true)));
        Assert.Empty(CatchSounds.PathsToMute(new MuteChoice(false, false, false)));
    }

    [Fact]
    public void A_path_is_matched_whatever_its_letter_case()
    {
        var muted = CatchSounds.PathsToMute(All);

        Assert.True(CatchSounds.ShouldMute("event:/UI/XIANJING_BUSHUJIA02", muted));
    }

    [Fact]
    public void The_paths_are_the_ones_the_game_plays()
    {
        // Copied from TrapManager.GetCaptureSoundPath and, for the rattle, the sound log; a typo here would mute nothing and every other test would still pass.
        Assert.Equal("event:/UI/xianjing_bushoulong", CatchSounds.LiveCaptureCage);
        Assert.Equal("event:/UI/xianjing_buniaolong", CatchSounds.SimpleBirdNet);
        Assert.Equal("event:/UI/xianjing_bushujia02", CatchSounds.OtherTraps);
        Assert.Equal("event:/UI/xianjing_Metalbushujia", CatchSounds.IronAnimalTrapRattle);
    }
}
