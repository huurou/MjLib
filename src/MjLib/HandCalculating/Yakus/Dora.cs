namespace MjLib.HandCalculating.Yakus;

internal record Dora : Yaku
{
    public Dora(int id)
        : base(id) { }

    public override string Name => "ドラ";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}
