namespace MjLib.HandCalculating.Yakus;

internal class Chiihou : Yaku
{
    public Chiihou(int id)
        : base(id) { }

    public override string Name => "地和";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(HandConfig config)
    {
        return config.IsChiihou;
    }
}