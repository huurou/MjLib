namespace MjLib.HandCalculating.Yakus;

internal record Riichi : Yaku
{
    public Riichi(int id)
        : base(id) { }

    public override string Name => "立直";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(WinSituation situation_)
    {
        return situation_.Riichi && !situation_.DaburuRiichi;
    }
}