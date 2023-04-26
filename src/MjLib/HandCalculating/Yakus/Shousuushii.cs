using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Shousuushii : Yaku
{
    public Shousuushii(int id)
        : base(id) { }

    public override string Name => "小四喜";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList_)
    {
        var tileKinds = hand.Concat(fuuroList_.KindLists);
        var koutsus = tileKinds.Where(x => x.IsKoutsu || x.IsKantsu);
        var toitsus = tileKinds.Where(x => x.IsToitsu);
        return koutsus.Count(x => x[0].IsWind) == 3 && toitsus.Count(x => x[0].IsWind) == 1;
    }
}