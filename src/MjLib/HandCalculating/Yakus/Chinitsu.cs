using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal record Chinitsu : Yaku
{
    public Chinitsu(int id)
        : base(id) { }

    public override string Name => "清一色";
    public override int HanOpen => 5;
    public override int HanClosed => 6;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand, FuuroList fuuroList)
    {
        IEnumerable<TileList> tileLists = [.. hand, .. fuuroList.TileLists];
        var tiles = tileLists.SelectMany(x => x);
        return
            tiles.All(x => x.IsMan) ||
            tiles.All(x => x.IsPin) ||
            tiles.All(x => x.IsSou);
    }
}