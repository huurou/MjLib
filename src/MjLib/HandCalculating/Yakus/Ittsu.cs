using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Ittsu : Yaku
{
    public Ittsu(int id)
        : base(id) { }

    public override string Name => "一気通貫";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        var shuntsus = hand.Concat(fuuroList.KindLists).Where(x => x.IsShuntsu);
        if (shuntsus.Count() < 3) return false;
        var suits = new[]
        {
            shuntsus.Where(x=>x[0].IsMan),
            shuntsus.Where(x=>x[0].IsPin),
            shuntsus.Where(x=>x[0].IsSou),
        };
        foreach (var suit in suits)
        {
            if (suit.Count() < 3) continue;
            var casted = suit.Select(x => x.Select(y => y.Number));
            return casted.Any(x => x.SequenceEqual(new[] { 1, 2, 3 })) &&
                casted.Any(x => x.SequenceEqual(new[] { 4, 5, 6 })) &&
                casted.Any(x => x.SequenceEqual(new[] { 7, 8, 9 }));
        }
        return false;
    }
}