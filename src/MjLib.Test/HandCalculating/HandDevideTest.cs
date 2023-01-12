using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.Test.HandCalculating;

public class HandDevideTest
{
    [Test]
    public void SimpleHandTest()
    {
        var hand = TileKindList.Parse(man: "234567", sou: "23455", honor: "777");
        var actual = HandDevider.Devide(hand);
        var expected = new TileKindListList("234m", "567m", "234s", "55s", "777z");
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual[0], Is.EqualTo(expected));

        hand = TileKindList.Parse(man: "123", pin: "123", sou: "123", honor: "11222");
        actual = HandDevider.Devide(hand);
        expected = new TileKindListList("123m", "123p", "123s", "11z", "222z");
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual[0], Is.EqualTo(expected));
    }

    [Test]
    public void HandWithPairsDividingTest()
    {
        var hand = TileKindList.Parse(man: "23444", pin: "344556", sou: "333");
        var actual = HandDevider.Devide(hand);
        var expected = new TileKindListList("234m", "44m", "345p", "456p", "333s");
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual[0], Is.EqualTo(expected));
    }

    [Test]
    public void OneSuitHandDividingTest()
    {
        var hand = TileKindList.Parse(man: "11122233388899");
        var actual = HandDevider.Devide(hand);
        var expected1 = new TileKindListList("111m", "222m", "333m", "888m", "99m");
        var expected2 = new TileKindListList("123m", "123m", "123m", "888m", "99m");
        Assert.That(actual, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(actual[0], Is.EqualTo(expected1));
            Assert.That(actual[1], Is.EqualTo(expected2));
        });

        hand = TileKindList.Parse(sou: "111123666789", honor: "11");
        actual = HandDevider.Devide(hand);
        var expected = new TileKindListList("111s", "123s", "666s", "789s", "11z");
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual[0], Is.EqualTo(expected));
    }

    [Test]
    public void FuuroHandDividingTest()
    {
        var hand = TileKindList.Parse(pin: "778899", honor: "22");
        var actual = HandDevider.Devide(hand);
        var expected = new TileKindListList("789p", "789p", "22z");
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual[0], Is.EqualTo(expected));
    }

    [Test]
    public void ChitoitsuLikeHandDividingTest()
    {
        var hand = TileKindList.Parse(man: "112233", pin: "99", sou: "445566");
        var actual = HandDevider.Devide(hand);
        var expected1 = new TileKindListList("11m", "22m", "33m", "99p", "44s", "55s", "66s");
        var expected2 = new TileKindListList("123m", "123m", "99p", "456s", "456s");
        Assert.That(actual, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(actual[0], Is.EqualTo(expected1));
            Assert.That(actual[1], Is.EqualTo(expected2));
        });
    }
}