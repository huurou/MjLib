namespace MjLib.HandCalculating.Yakus;

internal class Hatsu : Yaku
{
    public Hatsu(int id)
        : base(id) { }

    public override string Name => "發";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}