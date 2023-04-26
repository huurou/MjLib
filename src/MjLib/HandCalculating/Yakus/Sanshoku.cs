using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Sanshoku : Yaku
{
    public Sanshoku(int id)
        : base(id) { }

    public override string Name => "三色同順";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        var shuntsus = hand.Concat(fuuroList.KindLists).Where(x => x.IsShuntsu);
        if (shuntsus.Count() < 3) return false;
        var mans = shuntsus.Where(x => x[0].IsMan);
        var pins = shuntsus.Where(x => x[0].IsPin);
        var sous = shuntsus.Where(x => x[0].IsSou);
        foreach (var man in mans)
        {
            foreach (var pin in pins)
            {
                foreach (var sou in sous)
                {
                    var manNum = man.Select(x => x.Number);
                    var pinNum = pin.Select(x => x.Number);
                    var souNum = sou.Select(x => x.Number);
                    if (manNum.SequenceEqual(pinNum) &&
                        pinNum.SequenceEqual(souNum) &&
                        souNum.SequenceEqual(manNum))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}