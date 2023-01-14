namespace MjLib.HandCalculating.Yakus;

internal class Daisangen : Yaku
{
    public Daisangen(int id)
        : base(id) { }

    public override string Name => "大三元";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}