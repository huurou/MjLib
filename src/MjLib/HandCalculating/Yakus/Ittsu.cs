using MjLib.HandCalculating.Dividings;
using System.Diagnostics.CodeAnalysis;

namespace MjLib.HandCalculating.Yakus;

internal class Ittsu : Yaku
{
    public Ittsu(int id)
        : base(id) { }

    public override string Name => "一気通貫";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var shuntsus = hand.Where(x => x.IsShuntsu);
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
            return casted.Contains(new[] { 1, 2, 3 }, IntEnumerableEqualityComparer.Singleton)
                && casted.Contains(new[] { 4, 5, 6 }, IntEnumerableEqualityComparer.Singleton)
                && casted.Contains(new[] { 7, 8, 9 }, IntEnumerableEqualityComparer.Singleton);
        }
        return false;
    }

    private class IntEnumerableEqualityComparer : IEqualityComparer<IEnumerable<int>>
    {
        public static IntEnumerableEqualityComparer Singleton { get; } = new();

        private IntEnumerableEqualityComparer()
        { }

        public bool Equals(IEnumerable<int>? x, IEnumerable<int>? y)
        {
            return (x is null && y is null) ||
                (x is not null && y is not null && x.SequenceEqual(y));
        }

        public int GetHashCode([DisallowNull] IEnumerable<int> obj)
        {
            return obj.GetHashCode();
        }
    }
}