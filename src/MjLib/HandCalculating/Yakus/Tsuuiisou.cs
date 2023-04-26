using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal class Tsuuiisou : Yaku
{
    public Tsuuiisou(int id)
        : base(id) { }

    public override string Name => "字一色";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand)
    {
        return hand.SelectMany(x => x).All(x => x.IsHonor);
    }
}