using MjLib.TileCountArrays;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test;

public class TileKindListTest
{
    [Test]
    public void GetIsolationsTest()
    {
        var kindList = TileKindList.Parse(man: "25", pin: "15678", sou: "1369", honor: "124");
        var isolatations = kindList.GetIsolations();
        Assert.That(isolatations, Does.Not.Contain(Man1));
        Assert.That(isolatations, Does.Not.Contain(Man2));
        Assert.That(isolatations, Does.Not.Contain(Man3));
        Assert.That(isolatations, Does.Not.Contain(Man4));
        Assert.That(isolatations, Does.Not.Contain(Man5));
        Assert.That(isolatations, Does.Not.Contain(Man6));
        Assert.That(isolatations, Does.Contain(Man7));
        Assert.That(isolatations, Does.Contain(Man8));
        Assert.That(isolatations, Does.Contain(Man9));
        Assert.That(isolatations, Does.Not.Contain(Pin1));
        Assert.That(isolatations, Does.Not.Contain(Pin2));
        Assert.That(isolatations, Does.Contain(Pin3));
        Assert.That(isolatations, Does.Not.Contain(Pin4));
        Assert.That(isolatations, Does.Not.Contain(Pin5));
        Assert.That(isolatations, Does.Not.Contain(Pin6));
        Assert.That(isolatations, Does.Not.Contain(Pin7));
        Assert.That(isolatations, Does.Not.Contain(Pin8));
        Assert.That(isolatations, Does.Not.Contain(Pin9));
        Assert.That(isolatations, Does.Not.Contain(Sou1));
        Assert.That(isolatations, Does.Not.Contain(Sou2));
        Assert.That(isolatations, Does.Not.Contain(Sou3));
        Assert.That(isolatations, Does.Not.Contain(Sou4));
        Assert.That(isolatations, Does.Not.Contain(Sou5));
        Assert.That(isolatations, Does.Not.Contain(Sou6));
        Assert.That(isolatations, Does.Not.Contain(Sou7));
        Assert.That(isolatations, Does.Not.Contain(Sou8));
        Assert.That(isolatations, Does.Not.Contain(Sou9));
        Assert.That(isolatations, Does.Not.Contain(Ton));
        Assert.That(isolatations, Does.Not.Contain(Nan));
        Assert.That(isolatations, Does.Contain(Sha));
        Assert.That(isolatations, Does.Not.Contain(Pei));
        Assert.That(isolatations, Does.Contain(Haku));
        Assert.That(isolatations, Does.Contain(Hatsu));
        Assert.That(isolatations, Does.Contain(Chun));
    }

    [Test]
    public void ToTileCountArrayTest()
    {
        var kindList = TileKindList.Parse(man: "122333444456789", pin: "123455666777788889", sou: "123456778889999", honor: "1123334555567");
        var actual = kindList.ToTileCountArray();
        var expected = new TileCountArray(new[] { 1, 2, 3, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 2, 1, 3, 1, 4, 1, 1 });
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ParseOneLineTest()
    {
        var oneline = "123456789m123456789p123456789s1234567z";
        var actual = TileKindList.Parse(oneline);
        var expected = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
        Assert.That(actual, Is.EqualTo(expected));

        oneline = "1111m2222p3333s4444z";
        actual = TileKindList.Parse(oneline);
        expected = new TileKindList(new[] { 0, 0, 0, 0, 10, 10, 10, 10, 20, 20, 20, 20, 30, 30, 30, 30 });
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ParseLinesTest()
    {
        var actual = TileKindList.Parse(man: "123456789", pin: "123456789", sou: "123456789", honor: "1234567");
        var expected = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
        Assert.That(actual, Is.EqualTo(expected));

        actual = TileKindList.Parse(man: "1111", pin: "2222", sou: "3333", honor: "4444");
        expected = new TileKindList(new[] { 0, 0, 0, 0, 10, 10, 10, 10, 20, 20, 20, 20, 30, 30, 30, 30 });
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToOneLineTest()
    {
        var kindList = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
        var actual = kindList.ToOneLine();
        var expected = "123456789m123456789p123456789s1234567z";
        Assert.That(actual, Is.EqualTo(expected));

        kindList = new TileKindList(new[] { 0, 0, 0, 0, 10, 10, 10, 10, 20, 20, 20, 20, 30, 30, 30, 30 });
        actual = kindList.ToOneLine();
        expected = "1111m2222p3333s4444z";
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToStringTest()
    {
        var kindList = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
        var actual = kindList.ToString();
        var expected = "一二三四五六七八九(1)(2)(3)(4)(5)(6)(7)(8)(9)123456789東南西北白發中";
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExceptionTest()
    {
        // 同種牌5枚
        var kindList = TileKindList.Parse(pin: "45566777778999");
        Assert.That(() => { var a = kindList.ToTileCountArray(); }, Throws.ArgumentException);
    }
}