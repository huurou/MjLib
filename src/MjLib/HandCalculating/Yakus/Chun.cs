namespace MjLib.HandCalculating.Yakus;

internal class Chun : Yaku
{
    public Chun(int id)
        : base(id) { }

    public override string Name => "中";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}