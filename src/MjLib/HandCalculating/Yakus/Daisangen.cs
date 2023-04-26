using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Daisangen : Yaku
{
    public Daisangen(int id)
        : base(id) { }

    public override string Name => "大三元";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList_)
    {
        return hand.Concat(fuuroList_.KindLists).Count(x => (x.IsKoutsu || x.IsKantsu) && x[0].IsDragon) == 3;
    }
}