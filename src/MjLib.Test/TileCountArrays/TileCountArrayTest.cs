﻿using MjLib.TileCountArrays;
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
}