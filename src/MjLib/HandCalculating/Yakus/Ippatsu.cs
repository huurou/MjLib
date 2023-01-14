namespace MjLib.HandCalculating.Yakus;

internal class Ippatsu : Yaku
{
    public Ippatsu(int id)
        : base(id) { }

    public override string Name => "一発";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsIppatsu;
    }
}