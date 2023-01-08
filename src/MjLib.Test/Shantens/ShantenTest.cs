using MjLib.Shantens;
using MjLib.TileKinds;
using System.Net.NetworkInformation;

namespace MjLib.Test.Shantens;

public class ShantenTest
{
    [Test]
    public void ForRegularTest()
    {
        Assert.Multiple(() =>
        {
            var kindList = TileKindList.Parse(man: "567", pin: "11", sou: "111234567");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(Shanten.AGARI_STATE));

            kindList = TileKindList.Parse(man: "567", pin: "11", sou: "111345677");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(0));

            kindList = TileKindList.Parse(man: "567", pin: "15", sou: "111345677");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(1));

            kindList = TileKindList.Parse(man: "1578", pin: "15", sou: "11134567");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(2));

            kindList = TileKindList.Parse(man: "1358", pin: "1358", sou: "113456");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(3));

            kindList = TileKindList.Parse(man: "1358", pin: "13588", sou: "1589", honor: "1");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(4));

            kindList = TileKindList.Parse(man: "1358", pin: "13588", sou: "159", honor: "12");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(5));

            kindList = TileKindList.Parse(man: "1358", pin: "258", sou: "1589", honor: "123");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(6));

            kindList = TileKindList.Parse(sou: "11123456788999");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(Shanten.AGARI_STATE));

            kindList = TileKindList.Parse(sou: "11122245679999");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(0));

            kindList = TileKindList.Parse(man: "8", pin: "1367", sou: "4566677", honor: "12");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(2));

            kindList = TileKindList.Parse(man: "3678", pin: "3356", sou: "15", honor: "2567");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(4));

            kindList = TileKindList.Parse(man: "359", pin: "17", sou: "159", honor: "123567");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(7));

            kindList = TileKindList.Parse(man: "1111222235555", honor: "1");
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(1));
        });
    }
}