namespace MjLib.HandCalculating.Yakus;

internal class YakuhaiOfPlayer : Yaku
{
    public YakuhaiOfPlayer(int id)
        : base(id) { }

    public override string Name => "自風牌";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}