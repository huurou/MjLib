namespace MjLib.HandCalculating.Yakus;

internal record DaisuushiiDaburu : Yaku
{
    public DaisuushiiDaburu(int id)
        : base(id) { }

    public override string Name => "大四喜";
    public override int HanOpen => 26;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}