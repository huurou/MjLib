using MjLib.TileKinds;

namespace MjLib.Test.TileKinds;

public class TileKindListTest
{
    [Test]
    public void ParseOneLine()
    {
        Assert.Multiple(() =>
        {
            var oneline = "123456789m123456789p123456789s1234567z";
            var actual = TileKindList.Parse(oneline);
            var expected = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
            Assert.That(actual, Is.EqualTo(expected));

            oneline = "1111m2222p3333s4444z";
            actual = TileKindList.Parse(oneline);
            expected = new TileKindList(new[] { 0, 0, 0, 0, 10, 10, 10, 10, 20, 20, 20, 20, 30, 30, 30, 30 });
            Assert.That(actual, Is.EqualTo(expected));
        });
    }

    [Test]
    public void ParseLines()
    {
        Assert.Multiple(() =>
        {
            var actual = TileKindList.Parse(man: "123456789", pin: "123456789", sou: "123456789", honor: "1234567");
            var expected = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
            Assert.That(actual, Is.EqualTo(expected));

            actual = TileKindList.Parse(man: "1111", pin: "2222", sou: "3333", honor: "4444");
            expected = new TileKindList(new[] { 0, 0, 0, 0, 10, 10, 10, 10, 20, 20, 20, 20, 30, 30, 30, 30 });
            Assert.That(actual, Is.EqualTo(expected));
        });
    }

    [Test]
    public void ToOneLine()
    {
        Assert.Multiple(() =>
        {
            var kindList = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
            var actual = kindList.ToOneLine();
            var expected = "123456789m123456789p123456789s1234567z";
            Assert.That(actual, Is.EqualTo(expected));

            kindList = new TileKindList(new[] { 0, 0, 0, 0, 10, 10, 10, 10, 20, 20, 20, 20, 30, 30, 30, 30 });
            actual = kindList.ToOneLine();
            expected = "1111m2222p3333s4444z";
            Assert.That(actual, Is.EqualTo(expected));
        });
    }

    [Test]
    public void ToStringTest()
    {
        var kindList = new TileKindList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });
        var actual = kindList.ToString();
        var expected = "一二三四五六七八九(1)(2)(3)(4)(5)(6)(7)(8)(9)123456789東南西北白發中";
        Assert.That(actual, Is.EqualTo(expected));
    }
}