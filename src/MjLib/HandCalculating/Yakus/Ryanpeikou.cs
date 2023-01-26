using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Ryanpeikou : Yaku
{
    public Ryanpeikou(int id)
        : base(id) { }

    public override string Name => "二盃口";
    public override int HanOpen => 0;
    public override int HanClosed => 3;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        if (!fuuroList.HasOpen) return false;
        var shuntsus = hand.Where(x => x.IsShuntsu);
        var counts = shuntsus.Select(x => shuntsus.Count(x.Equals));
        return counts.Count(x => x >= 2) == 4;
    }
}