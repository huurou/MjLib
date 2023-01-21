using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal class SuuankouTanki : Yaku
{
    public SuuankouTanki(int id)
        : base(id) { }

    public override string Name => "四暗刻単騎";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, TileKindList winGroup, TileKind winTile, FuuroList fuuroList, WinSituation situation, GameRules rules)
    {
        if(!rules.DaburuYakuman) return false;
        var jantou = hand.Where(x => x.IsToitsu).First();
        var anko = situation.Tsumo
            ? hand.Where(x => x.IsKoutsu)
            : hand.Where(x => x.IsKoutsu && x != winGroup);
        var ankan = fuuroList.Where(x => x.IsAnkan).Select(x => x.KindList);
        return jantou[0] == winTile && anko.Count() + ankan.Count() == 4;
    }
}