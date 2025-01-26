using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;
using static MjLib.TileKinds.Tile;

namespace MjLib.HandCalculating.Yakus;

internal record PlayerWind : Yaku
{
    public PlayerWind(int id)
        : base(id) { }

    public override string Name => "自風牌";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand, FuuroList fuuroList_, WinSituation situation_)
    {
        return hand.Concat(fuuroList_.TileLists)
                   .Where(x => (x.IsKoutsu || x.IsKantsu) && x[0] == WindToTileKind(situation_.Player))
                   .Any();
    }

    private static Tile WindToTileKind(Wind wind)
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