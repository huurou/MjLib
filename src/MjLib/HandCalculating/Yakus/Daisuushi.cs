using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Daisuushii : Yaku
{
    public Daisuushii(int id)
        : base(id) { }

    public override string Name => "大四喜";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList_)
    {
        return hand.Concat(fuuroList_.KindLists).Count(x => (x.IsKoutsu || x.IsKantsu) && x[0].IsWind) == 4;
    }
}
