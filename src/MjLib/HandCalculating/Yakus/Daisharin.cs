using MjLib.HandCalculating.Dividings;
using static MjLib.TileKinds.Tile;

namespace MjLib.HandCalculating.Yakus;

internal record Daisharin : Yaku
{
    public Daisharin(int id)
        : base(id) { }

    public override string Name => "大車輪"; public override int HanOpen => 0; public override int HanClosed => 13; public override bool IsYakuman => true;

    public static bool Valid(TileListList hand, GameRules rules)
    {
        return hand.Equals(
            [
                new () { Pin2, Pin2 },
                new () { Pin3, Pin3 },
                new () { Pin4, Pin4 },
                new () { Pin5, Pin5 },
                new () { Pin6, Pin6 },
                new () { Pin7, Pin7 },
                new () { Pin8, Pin8 },
            ]) && rules.Daisharin;
    }
}