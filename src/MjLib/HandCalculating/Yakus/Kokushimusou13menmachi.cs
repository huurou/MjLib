using MjLib.TileCountArrays;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal record Kokushimusou13 : Yaku
{
    public Kokushimusou13(int id)
        : base(id) { }

    public override string Name => "国士無双十三面待ち";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;

    public static bool Valid(CountArray countArray, Tile? winTile, GameRules rules)
    {
        return winTile is not null &&
            rules.DaburuYakuman &&
            countArray[winTile] == 2 &&
            Tile.AllKind.Where(x => x.IsYaochuu).Aggregate(1, (x, y) => x * countArray[y]) == 2;
    }
}