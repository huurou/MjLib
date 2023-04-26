using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Honrouto : Yaku
{
    public Honrouto(int id)
        : base(id) { }

    public override string Name => "混老頭";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        return hand.SelectMany(x => x).All(x => x.IsYaochuu);
    }
}