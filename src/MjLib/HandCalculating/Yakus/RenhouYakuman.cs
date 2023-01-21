namespace MjLib.HandCalculating.Yakus;

internal class RenhouYakuman : Yaku
{
    public RenhouYakuman(int id)
        : base(id) { }

    public override string Name => "人和";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(WinSituation situation, GameRules rules)
    {
        return situation.Renhou && rules.RenhouAsYakuman;
    }
}