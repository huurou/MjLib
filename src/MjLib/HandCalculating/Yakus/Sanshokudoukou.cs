namespace MjLib.HandCalculating.Yakus;

internal class Sanshokudoukou : Yaku
{
    public Sanshokudoukou(int id)
        : base(id) { }

    public override string Name => "三色同刻";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}