namespace MjLib.HandCalculating.Yakus;

internal class Suuankou : Yaku
{
    public Suuankou(int id)
        : base(id) { }

    public override string Name => "四暗刻";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}