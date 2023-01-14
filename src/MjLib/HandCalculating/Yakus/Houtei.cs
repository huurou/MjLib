namespace MjLib.HandCalculating.Yakus;

internal class Houtei : Yaku
{
    public Houtei(int id)
        : base(id) { }

    public override string Name => "河底撈魚";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsHoutei;
    }
}