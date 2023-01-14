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

    public static bool Valid(TileKindListList hand)
    {
        var shuntus = hand.Count(x => x.IsShuntsu);
        var routou = hand.Count(x => x.Any(y => y.IsRoutou));
        var honor = hand.Count(x => x[0].IsHonor);
        return shuntus != 0 &&
            routou + honor == 5 &&
            routou != 0 && honor != 0;
    }
}