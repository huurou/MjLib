using MjLib.Fuuros;
using MjLib.HandCalculating;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.TileKinds;
using static MjLib.Fuuros.FuuroType;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class FuTest
{
    [Test]
    public void ChitoitsuTest()
    {
        var hand = TileKindList.Parse(man: "115599", pin: "66", sou: "112244");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var actual = FuCalculator.Calc(devided, winTile, TileKindList.Parse(pin: "66"), new());
        var expected = new FuList { Fu.Chitoitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(25));
    }

    [Test]
    public void OpenHandBaseTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "11", sou: "222678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var fuuroList = new FuuroList { new(Pon, TileKindList.Parse(man: "222")) };
        var actual = FuCalculator.Calc(devided, winTile, TileKindList.Parse(pin: "678"), new());
        var expected = new FuList { Fu.Base, Fu.ChuchanMinko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(25));
    }
}