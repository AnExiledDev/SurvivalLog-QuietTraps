using QuietTraps.Core;
using Xunit;

namespace QuietTraps.Tests;

public class SoundSightingsTests
{
    private const string Rattle = "event:/Model/Object/rattle";

    [Fact]
    public void A_repeating_sound_is_logged_a_few_times_to_show_its_rhythm_then_stops()
    {
        var sightings = new SoundSightings();
        var logged = Enumerable.Range(0, 10).Count(_ => sightings.ShouldLog(Rattle));

        Assert.Equal(SoundSightings.PerPath, logged);
    }

    [Fact]
    public void Each_path_gets_its_own_count()
    {
        var sightings = new SoundSightings();

        for (var i = 0; i < SoundSightings.PerPath; i++) sightings.ShouldLog(Rattle);

        Assert.False(sightings.ShouldLog(Rattle));
        Assert.True(sightings.ShouldLog("event:/Model/Object/shake"));
    }
}
