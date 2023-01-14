namespace MjLib.HandCalculating.Yakus;

internal class JunseiChuurenpoutou : Yaku
{
    public JunseiChuurenpoutou(int id)
        : base(id) { }

    public override string Name => "純正九蓮宝燈";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}