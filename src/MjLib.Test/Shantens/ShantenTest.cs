using MjLib.Shantens;
using MjLib.TileKinds;

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
            Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(0));
        });
    }

    [Test]
    public void ForRegularNotCompletedTest()
    {
        var kindList = TileKindList.Parse(man: "567", pin: "1", sou: "111345677");
        Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(1));

        kindList = TileKindList.Parse(man: "567", sou: "111345677");
        Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(1));

        kindList = TileKindList.Parse(man: "56", sou: "111345677");
        Assert.That(Shanten.CalculateForRegular(kindList), Is.EqualTo(0));
    }

    [Test]
    public void ForChiitoitsuTest()
    {
        var kindList = TileKindList.Parse(man: "77", pin: "114477", sou: "114477");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(Shanten.AGARI_STATE));

        kindList = TileKindList.Parse(man: "76", pin: "114477", sou: "114477");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(0));

        kindList = TileKindList.Parse(man: "76", pin: "114479", sou: "114477");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(1));

        kindList = TileKindList.Parse(man: "76", pin: "14479", sou: "114477", honor: "1");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(2));

        kindList = TileKindList.Parse(man: "76", pin: "13479", sou: "114477", honor: "1");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(3));

        kindList = TileKindList.Parse(man: "76", pin: "13479", sou: "114467", honor: "1");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(4));

        kindList = TileKindList.Parse(man: "76", pin: "13479", sou: "113467", honor: "1");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(5));

        kindList = TileKindList.Parse(man: "76", pin: "13479", sou: "123467", honor: "1");
        Assert.That(Shanten.CalculateForChiitoitsu(kindList), Is.EqualTo(6));
    }

    [Test]
    public void ForKokushiTest()
    {
        var kindList = TileKindList.Parse(man: "19", pin: "19", sou: "19", honor: "12345677");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(Shanten.AGARI_STATE));

        kindList = TileKindList.Parse(man: "19", pin: "19", sou: "129", honor: "1234567");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(0));

        kindList = TileKindList.Parse(man: "19", pin: "129", sou: "129", honor: "123456");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(1));

        kindList = TileKindList.Parse(man: "129", pin: "129", sou: "129", honor: "12345");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(2));

        kindList = TileKindList.Parse(man: "129", pin: "129", sou: "1239", honor: "2345");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(3));

        kindList = TileKindList.Parse(man: "129", pin: "1239", sou: "1239", honor: "345");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(4));

        kindList = TileKindList.Parse(man: "1239", pin: "1239", sou: "1239", honor: "45");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(5));

        kindList = TileKindList.Parse(man: "1239", pin: "1239", sou: "12349", honor: "5");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(6));

        kindList = TileKindList.Parse(man: "1239", pin: "12349", sou: "12349");
        Assert.That(Shanten.CalculateForKokushi(kindList), Is.EqualTo(7));
    }

    [Test]
    public void ForOpenSetsTest()
    {
        var kindList = TileKindList.Parse(pin: "222567", sou: "44467778");
        Assert.That(Shanten.Calculate(kindList), Is.EqualTo(Shanten.AGARI_STATE));

        kindList = TileKindList.Parse(pin: "222567", sou: "44468");
        Assert.That(Shanten.Calculate(kindList), Is.EqualTo(0));

        kindList = TileKindList.Parse(pin: "222567", sou: "68");
        Assert.That(Shanten.Calculate(kindList), Is.EqualTo(0));

        kindList = TileKindList.Parse(pin: "222567", sou: "68");
        Assert.That(Shanten.Calculate(kindList), Is.EqualTo(0));

        kindList = TileKindList.Parse(sou: "68");
        Assert.That(Shanten.Calculate(kindList), Is.EqualTo(0));

        kindList = TileKindList.Parse(sou: "88");
        Assert.That(Shanten.Calculate(kindList), Is.EqualTo(Shanten.AGARI_STATE));
    }
}