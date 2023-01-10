using MjLib.HandCalculating;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class FuTest
{
    [Test]
    public void ChiitoitsuTest()
    {
        var hand = TileKindList.Parse(man: "115599", pin: "66", sou: "112244");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var actual = FuCalculator.Calc(devided, winTile, TileKindList.Parse(pin: "66"),new());
        var expected = new FuList { Fu.Chiitoitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(25));
    }
}