using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal class Suuankou : Yaku
{
    public Suuankou(int id)
        : base(id) { }

    public override string Name => "四暗刻";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, TileKindList winGroup, FuuroList fuuroList, HandConfig config)
    {
        var anko = config.IsTsumo
            ? hand.Where(x => x.IsKoutsu)
            : hand.Where(x => x.IsKoutsu && x != winGroup);
        var ankan = fuuroList.Where(x => x.IsAnkan).Select(x => x.KindList);
        return anko.Count() + ankan.Count() == 4;
    }
}