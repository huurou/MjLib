namespace MjLib.HandCalculating.Yakus;

internal class Haku : Yaku
{
    public Haku(int id)
        : base(id) { }

    public override string Name => "白";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}