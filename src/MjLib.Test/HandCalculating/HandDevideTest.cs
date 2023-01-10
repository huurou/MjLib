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
        var expected = new DevidedHand
        {
            TileKindList.Parse(man:"234"),
            TileKindList.Parse(man:"567"),
            TileKindList.Parse(sou:"234"),
            TileKindList.Parse(sou:"55"),
            TileKindList.Parse(honor:"777"),
        };
        Assert.That(actual, Has.Count.EqualTo(1));
        Assert.That(actual[0], Is.EqualTo(expected));
    }
}