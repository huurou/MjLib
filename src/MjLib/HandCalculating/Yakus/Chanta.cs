using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal class Chanta : Yaku
{
    public Chanta(int id)
        : base(id) { }

    public override string Name => "混全帯幺九";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        var kindLists = hand.Concat(fuuroList.KindLists);
        var shuntus = kindLists.Count(x => x.IsShuntsu);
        var routou = kindLists.Count(x => x.Any(y => y.IsRoutou));
        var honor = kindLists.Count(x => x[0].IsHonor);
        return shuntus != 0 &&
            routou + honor == 5 &&
            routou != 0 && honor != 0;
    }
}