using MjLib.Agaris;
using MjLib.TileCountArrays;
using MjLib.TileKinds;

namespace MjLib.Test.TileCountArrays;

internal class TileCountArrayTest
{
    [Test]
    public void ToTileKindList()
    {
        var countArray = new TileCountArray(new[] { 1, 2, 3, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 2, 1, 3, 1, 4, 1, 1 });
        var actual = countArray.ToTileKindList();
        var expected = TileKindList.Parse(man: "122333444456789", pin: "123455666777788889", sou: "123456778889999", honor: "1123334555567");
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToStringTest()
    {
        var countArray = new TileCountArray(new[] { 1, 2, 3, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 4, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 2, 1, 3, 1, 4, 1, 1 });
        var actual = countArray.ToString();
        Assert.That(actual, Is.EqualTo("m[123411111] p[111123441] s[111111234] z[2131411]"));
    }

    [Test]
    public void ExceptionTest()
    {
        Assert.That(() => { var a = new TileCountArray(new[] { 1, 2, 3 }); }, Throws.ArgumentException);
    }
}