namespace MjLib.HandCalculating.Yakus;

internal class Akadora : Yaku
{
    public Akadora(int id)
        : base(id) { }

    public override string Name => "赤ドラ";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}