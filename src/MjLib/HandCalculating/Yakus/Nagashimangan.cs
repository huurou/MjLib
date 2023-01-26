namespace MjLib.HandCalculating.Yakus;

internal record Nagashimangan : Yaku
{
    public Nagashimangan(int id)
        : base(id) { }

    public override string Name => "流し満貫";
    public override int HanOpen => 5;
    public override int HanClosed => 5;
    public override bool IsYakuman => false;
}