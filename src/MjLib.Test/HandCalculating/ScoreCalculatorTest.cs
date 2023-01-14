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
        var expected = new Score { Main = 1500};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 2, config: config);
        expected = new Score { Main = 2900};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score { Main = 5800};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score { Main = 11600};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score { Main = 12000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score { Main = 18000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score { Main = 24000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score { Main = 36000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score { Main = 48000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score { Main = 96000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score { Main = 144000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score { Main = 192000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score { Main = 240000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score { Main = 288000};
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
        var expected = new Score { Main = 1000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 110, han: 1, config: config);
        expected = new Score { Main = 3600};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 2, config: config);
        expected = new Score { Main = 2000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score { Main = 3900};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score { Main = 7700};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 40, han: 4, config: config);
        expected = new Score { Main = 8000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score { Main = 8000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score { Main = 12000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score { Main = 16000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score { Main = 24000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score { Main = 32000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score { Main = 64000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score { Main = 96000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score { Main = 128000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score { Main = 160000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score { Main = 192000};
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
        var expected = new Score { Main = 500, Sub = 500 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score { Main = 2000, Sub = 2000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score { Main = 3900, Sub = 3900 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score { Main = 3900, Sub = 3900 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score { Main = 4000, Sub = 4000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score { Main = 6000, Sub = 6000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score { Main = 8000, Sub = 8000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score { Main = 12000, Sub = 12000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score { Main = 16000, Sub = 16000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score { Main = 32000, Sub = 32000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score { Main = 48000, Sub = 48000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score { Main = 64000, Sub = 64000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score { Main = 80000, Sub = 80000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score { Main = 96000, Sub = 96000 };
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
        var expected = new Score { Main = 500, Sub = 300 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 3, config: config);
        expected = new Score { Main = 2000, Sub = 1000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score { Main = 3900, Sub = 2000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score { Main = 3900, Sub = 2000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 5, config: config);
        expected = new Score { Main = 4000, Sub = 2000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 6, config: config);
        expected = new Score { Main = 6000, Sub = 3000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 8, config: config);
        expected = new Score { Main = 8000, Sub = 4000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 11, config: config);
        expected = new Score { Main = 12000, Sub = 6000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 13, config: config);
        expected = new Score { Main = 16000, Sub = 8000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 26, config: config);
        expected = new Score { Main = 32000, Sub = 16000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 39, config: config);
        expected = new Score { Main = 48000, Sub = 24000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 52, config: config);
        expected = new Score { Main = 64000, Sub = 32000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 65, config: config);
        expected = new Score { Main = 80000, Sub = 40000 };
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 0, han: 78, config: config);
        expected = new Score { Main = 96000, Sub = 48000 };
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
        var expected = new Score { Main = 12000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score { Main = 12000};
        Assert.That(actual, Is.EqualTo(expected));

        config = new HandConfig
        {
            Rurles = new() { Kiriage = true },
            PlayerWind = Wind.South,
        };
        actual = ScoreCalcurator.Calculate(fu: 30, han: 4, config: config);
        expected = new Score { Main = 8000};
        Assert.That(actual, Is.EqualTo(expected));

        actual = ScoreCalcurator.Calculate(fu: 60, han: 3, config: config);
        expected = new Score { Main = 8000};
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
        var expected = new Score { Main = 32000};
        Assert.That(actual, Is.EqualTo(expected));

        config = new()
        {
            Rurles = new() { KazoeLimit = Kazoe.Sanbaiman },
            PlayerWind = Wind.South,
        };
        actual = ScoreCalcurator.Calculate(fu: 30, han: 13, config: config, isYakuman: false);
        expected = new Score { Main = 24000};
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void FailedTest()
    {
        var config = new HandConfig();
        Assert.That(() => ScoreCalcurator.Calculate(fu: 30, han: 0, config: config), Throws.ArgumentException);
    }
}