namespace MjLib.HandCalculating.Yakus;

internal class Sanankou : Yaku
{
    public Sanankou(int id)
        : base(id) { }

    public override string Name => "三暗刻";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}