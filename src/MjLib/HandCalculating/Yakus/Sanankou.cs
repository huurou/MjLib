using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal class Sanankou : Yaku
{
    public Sanankou(int id)
        : base(id) { }

    public override string Name => "三暗刻";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, TileKindList winGroup, FuuroList fuuroList, WinSituation situation)
    {
        var anko = situation.Tsumo
            ? hand.Where(x => x.IsKoutsu)
            : hand.Where(x => x.IsKoutsu && x != winGroup);
        var ankan = fuuroList.Where(x => x.IsAnkan).Select(x => x.KindList);
        return ankan.Count() + ankan.Count() == 3;
    }
}