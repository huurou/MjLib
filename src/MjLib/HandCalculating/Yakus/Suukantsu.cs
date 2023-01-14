namespace MjLib.HandCalculating.Yakus;

internal class Suukantsu : Yaku
{
    public Suukantsu(int id)
        : base(id) { }

    public override string Name => "四槓子";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}