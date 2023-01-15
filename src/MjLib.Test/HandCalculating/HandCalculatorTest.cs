using MjLib.Fuuros;
using MjLib.HandCalculating;
using MjLib.HandCalculating.Hands;
using MjLib.HandCalculating.Yakus;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class HandCalculatorTest
{
    [Test]
    public void RiichiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsRiichi = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Riichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }
}