namespace MjLib.HandCalculating.Yakus;

internal class Kokushimusou13 : Yaku
{
    public Kokushimusou13(int id)
        : base(id) { }

    public override string Name => "国士無双十三面待ち";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}