using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Iipeikou : Yaku
{
    public Iipeikou(int id)
        : base(id) { }

    public override string Name => "一盃口";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand, FuuroList fuuroList)
    {
        if (fuuroList.HasOpen) return false;
        var shuntsus = hand.Where(x => x.IsShuntsu);
        var count = shuntsus.Max(x => shuntsus.Count(x.Equals));
        return count >= 2;
    }
}