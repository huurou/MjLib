namespace MjLib.HandCalculating.Yakus;

internal class SuuankouTanki : Yaku
{
    public SuuankouTanki(int id)
        : base(id) { }

    public override string Name => "四暗刻単騎";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}