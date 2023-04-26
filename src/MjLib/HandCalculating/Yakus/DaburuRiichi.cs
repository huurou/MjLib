namespace MjLib.HandCalculating.Yakus;

internal record DaburuRiichi : Yaku
{
    public DaburuRiichi(int id)
        : base(id) { }

    public override string Name => "ダブル立直";
    public override int HanOpen => 0;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(WinSituation situation_)
    {
        return situation_.DaburuRiichi;
    }
}