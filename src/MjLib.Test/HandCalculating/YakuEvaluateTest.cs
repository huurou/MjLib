using MjLib.HandCalculating;
using MjLib.HandCalculating.Yakus;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class YakuEvaluateTest
{
    [Test]
    public void RiichiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsRiichi = true };
        var actual = YakuEvaluator.Evaluate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Riichi };
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(actual.HanClosed, Is.EqualTo(1));
    }
}