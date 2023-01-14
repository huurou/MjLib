namespace MjLib.HandCalculating.Yakus;

internal class YakuhaiOfRound : Yaku
{
    public YakuhaiOfRound(int id)
        : base(id) { }

    public override string Name => "場風牌";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}