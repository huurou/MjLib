using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.HandCalculating.Yakus;

internal class PlayerWind : Yaku
{
    public PlayerWind(int id)
        : base(id) { }

    public override string Name => "自風牌";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList_, HandConfig config_)
    {
        return hand.Concat(fuuroList_.KindLists)
                   .Where(x => (x.IsKoutsu || x.IsKantsu) && x[0] == WindToTileKind(config_.PlayerWind))
                   .Any();
    }

    private static TileKind WindToTileKind(Wind wind)
    {
        return wind switch
        {
            Wind.East => Ton,
            Wind.South => Nan,
            Wind.West => Sha,
            Wind.North => Pei,
            _ => throw new NotSupportedException()
        };
    }
}