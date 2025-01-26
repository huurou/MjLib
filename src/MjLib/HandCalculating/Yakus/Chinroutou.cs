using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Chinroutou : Yaku
{
    public Chinroutou(int id)
        : base(id) { }

    public override string Name => "清老頭";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileListList hand)
    {
        return hand.SelectMany(x => x).All(x => x.IsRoutou);
    }
}