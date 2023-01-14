using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal class Toitoihou : Yaku
{
    public Toitoihou(int id)
        : base(id) { }

    public override string Name => "対々和";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        return hand.Concat(fuuroList.KindLists).Count(x => x.IsKoutsu || x.IsKantsu) == 4;
    }
}