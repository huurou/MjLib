using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Chiitoitsu : Yaku
{
    public Chiitoitsu(int id)
        : base(id) { }

    public override string Name => "七対子";
    public override int HanOpen => 0;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand)
    {
        return hand.Count == 7 && hand.All(x => x.IsToitsu);
    }
}