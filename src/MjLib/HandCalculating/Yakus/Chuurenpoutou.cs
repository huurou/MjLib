namespace MjLib.HandCalculating.Yakus;

internal class Chuurenpoutou : Yaku
{
    public Chuurenpoutou(int id)
        : base(id) { }

    public override string Name => "九蓮宝燈";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}