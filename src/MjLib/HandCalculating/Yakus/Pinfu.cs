using MjLib.Fuuros;
using MjLib.HandCalculating.Fus;

namespace MjLib.HandCalculating.Yakus;

internal class Pinfu : Yaku
{
    public Pinfu(int id)
        : base(id) { }

    public override string Name => "平和";
    public override int HanOpen => 0;
    public override int HanClosed => 1; 
    public override bool IsYakuman => false;

    public static bool Valid(FuList fuList, FuuroList fuuroList)
    {
        return !fuuroList.HasOpen &&
            (fuList.Contains(Fu.Base) && fuList.Count == 1 ||
            fuList.Contains(Fu.Base) && fuList.Contains(Fu.Menzen) && fuList.Count == 2);
    }
}