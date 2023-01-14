namespace MjLib.HandCalculating.Yakus;

internal class Kokushimusou : Yaku
{
    public Kokushimusou(int id)
        : base(id) { }

    public override string Name => "国士無双";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}