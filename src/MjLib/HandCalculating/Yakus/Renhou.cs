namespace MjLib.HandCalculating.Yakus;

internal class Renhou : Yaku
{
    public Renhou(int id)
        : base(id) { }

    public override string Name => "人和";
    public override int HanOpen => 0;
    public override int HanClosed => 5;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config, OptionalRules rules)
    {
        return config.IsRenhou && !rules.RenhouAsYakuman;
    }
}