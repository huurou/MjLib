using MjLib.TileCountArrays;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal record Kokushimusou : Yaku
{
    public Kokushimusou(int id)
        : base(id) { }

    public override string Name => "国士無双";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    internal static bool Valid(TileCountArray countArray)
    {
        return TileKind.AllKind.Where(x => x.IsYaochuu).Aggregate(1, (x, y) => x * countArray[y]) == 2;
    }
}