using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;
using static MjLib.TileKinds.Tile;

namespace MjLib.HandCalculating.Yakus;

internal record Ryuuiisou : Yaku
{
    public Ryuuiisou(int id)
        : base(id) { }

    public override string Name => "緑一色";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileListList hand)
    {
        var greens = new[] { Sou2, Sou3, Sou4, Sou6, Sou8, Tile.Hatsu };
        return hand.SelectMany(x => x).Distinct().All(x => greens.Contains(x));
    }
}