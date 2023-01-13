using MjLib.HandCalculating;
using MjLib.HandCalculating.Scores;

namespace MjLib.Test.HandCalculating;

public class ScoreCalculatorTest
{
    [Test]
    public void CalculateScoresAndRonByDealerTest()
    {
        var config = new HandConfig
        {
            Rurles = new() { KazoeLimit = Kazoe.Nolimit },
            PlayerWind = Wind.East,
        };
        var actual = ScoreCalcurator.Calculate(fu: 30, han: 1, config: config);
        var expected = new Score(30, 1, 1500, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 2, config: config);
        expected = new Score(30, 2, 2900, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score(30, 3, 5800, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score(30, 4, 11600, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score(0, 5, 12000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score(0, 6, 18000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score(0, 8, 24000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score(0, 11, 36000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score(0, 13, 48000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score(0, 26, 96000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score(0, 39, 144000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score(0, 52, 192000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score(0, 65, 240000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score(0, 78, 288000, 0);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateScoresAndRonTest()
    {
        var config = new HandConfig
        {
            Rurles = new() { KazoeLimit = Kazoe.Nolimit },
            PlayerWind = Wind.South,
        };
        var actual = ScoreCalcurator.Calculate(fu: 30, han: 1, config: config);
        var expected = new Score(30, 1, 1000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 110, han: 1, config: config);
        expected = new Score(110, 1, 3600, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 2, config: config);
        expected = new Score(30, 2, 2000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score(30, 3, 3900, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score(30, 4, 7700, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 40, han: 4, config: config);
        expected = new Score(40, 4, 8000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score(0, 5, 8000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score(0, 6, 12000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score(0, 8, 16000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score(0, 11, 24000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score(0, 13, 32000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score(0, 26, 64000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score(0, 39, 96000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score(0, 52, 128000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score(0, 65, 160000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score(0, 78, 192000, 0);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CalcylateScoresAndTsumoByDealerTest()
    {
        var config = new HandConfig
        {
            Rurles = new() { KazoeLimit = Kazoe.Nolimit },
            IsTsumo = true,
            PlayerWind = Wind.East,
        };
        var actual = ScoreCalcurator.Calculate(fu: 30, han: 1, config: config);
        var expected = new Score(30, 1, 500, 500);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score(30, 3, 2000, 2000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score(60, 3, 3900, 3900);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score(30, 4, 3900, 3900);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score(0, 5, 4000, 4000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score(0, 6, 6000, 6000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score(0, 8, 8000, 8000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score(0, 11, 12000, 12000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score(0, 13, 16000, 16000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score(0, 26, 32000, 32000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score(0, 39, 48000, 48000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score(0, 52, 64000, 64000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score(0, 65, 80000, 80000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score(0, 78, 96000, 96000);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateScoresAndTsumo()
    {
        var config = new HandConfig
        {
            Rurles = new() { KazoeLimit = Kazoe.Nolimit },
            IsTsumo = true,
            PlayerWind = Wind.South,
        };
        var actual = ScoreCalcurator.Calculate(fu: 30, han: 1, config: config);
        var expected = new Score(30, 1, 500, 300);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score(30, 3, 2000, 1000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score(60, 3, 3900, 2000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score(30, 4, 3900, 2000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score(0, 5, 4000, 2000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score(0, 6, 6000, 3000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score(0, 8, 8000, 4000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score(0, 11, 12000, 6000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score(0, 13, 16000, 8000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score(0, 26, 32000, 16000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score(0, 39, 48000, 24000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score(0, 52, 64000, 32000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score(0, 65, 80000, 40000);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score(0, 78, 96000, 48000);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void KiriageManganTest()
    {
        var config = new HandConfig
        {
            Rurles = new() { Kiriage = true },
            PlayerWind = Wind.East,
        };
        var actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        var expected = new Score(30, 4, 12000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score(60, 3, 12000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        config = new HandConfig
        {
            Rurles = new() { Kiriage = true },
            PlayerWind = Wind.South,
        };
        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score(30, 4, 8000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score(60, 3, 8000, 0);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void KazoeLimitTest()
    {
        var config = new HandConfig
        {
            Rurles = new() { KazoeLimit = Kazoe.Limited },
            PlayerWind = Wind.South,
        };
        var actual = ScoreCalcurator.Calculate(fu: 30, han: 13, config: config, isYakuman: false);
        var expected = new Score(30, 13, 32000, 0);
        Assert.That(actual, Is.EqualTo(expected));

        config = new()
        {
            Rurles = new() { KazoeLimit = Kazoe.Sanbaiman },
            PlayerWind = Wind.South,
        };
        actual = ScoreCalcurator.Calculate(fu: 30, han: 13, config: config, isYakuman: false);
        expected = new Score(30, 13, 24000, 0);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void FailedTest()
    {
        var config = new HandConfig();
        Assert.That(() => ScoreCalcurator.Calculate(fu: 30, han: 0, config: config), Throws.ArgumentException);
    }
}