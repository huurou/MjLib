namespace MjLib.HandCalculating.Yakus;

internal class Shousangen : Yaku
{
    public Shousangen(int id)
        : base(id) { }

    public override string Name => "小三元";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}