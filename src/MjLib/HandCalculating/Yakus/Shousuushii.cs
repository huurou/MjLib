namespace MjLib.HandCalculating.Yakus;

internal class Shousuushii : Yaku
{
    public Shousuushii(int id)
        : base(id) { }

    public override string Name => "小四喜";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}