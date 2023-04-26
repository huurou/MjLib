using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Chinitsu : Yaku
{
    public Chinitsu(int id)
        : base(id) { }

    public override string Name => "清一色";
    public override int HanOpen => 5;
    public override int HanClosed => 6;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var man = hand.Count(x => x[0].IsMan);
        var pin = hand.Count(x => x[0].IsPin);
        var sou = hand.Count(x => x[0].IsSou);
        var honor = hand.Count(x => x[0].IsHonor);
        return new[] { man, pin, sou }.Count(x => x != 0) == 1 && honor == 0;
    }
}