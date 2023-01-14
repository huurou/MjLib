using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal class Sankantsu : Yaku
{
    public Sankantsu(int id)
        : base(id) { }

    public override string Name => "三槓子";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        return hand.Concat(fuuroList.KindLists).Count(x => x.IsKantsu) == 3;
    }
}