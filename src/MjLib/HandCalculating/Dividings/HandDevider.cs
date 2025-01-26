using MjLib.TileCountArrays;
using MjLib.TileKinds;
using static MjLib.TileKinds.Tile;

namespace MjLib.HandCalculating.Dividings;

internal static class HandDevider
{
    private static CountArray countArray_ = new();

    /// <summary>
    /// 手牌が分割されうる全組み合わせを返します。
    /// </summary>
    /// <param name="hand">手牌 副露を含まない手牌+アガリ牌</param>
    /// <returns>分割された手牌のリスト</returns>
    public static List<TileListList> Devide(TileList hand)
    {
        countArray_ = hand.ToTileCountArray();

        var hands = new List<TileListList>();
        var requiredMentsuCount = hand.Count / 3 + 1;
        var toitsuKinds = FindToitsuKinds();
        foreach (var toitsuKind in toitsuKinds)
        {
            var copiedArray = new CountArray(countArray_.ToArray());
            // 雀頭候補を外す
            copiedArray[toitsuKind] -= 2;
            var man = FindValidCombinations(copiedArray, Man1);
            var pin = FindValidCombinations(copiedArray, Pin1);
            var sou = FindValidCombinations(copiedArray, Sou1);
            var honor = new TileListList(AllKind.Where(x => x.IsHonor && copiedArray[x] == 3).Select(x => new TileList(Enumerable.Repeat(x, 3))));
            var suits = new List<List<TileListList>> { new() { new() { new(Enumerable.Repeat(toitsuKind, 2)) } } };
            if (man.Any()) suits.Add(man);
            if (pin.Any()) suits.Add(pin);
            if (sou.Any()) suits.Add(sou);
            if (honor.Any()) suits.Add([honor]);
            foreach (var p in Product(suits))
            {
                var h = new TileListList(p.SelectMany(x => x));
                if (h.Count == requiredMentsuCount)
                {
                    h.Sort((x, y) => x[0].CompareTo(y[0]));
                    hands.Add(h);
                }
            }
        }
        var uniqueHands = new List<TileListList>();
        foreach (var h in hands)
        {
            h.Sort((x, y) => x[0].CompareTo(y[0]) > 0 ? 1
                           : x[0].CompareTo(y[0]) < 0 ? -1
                           : x[1].CompareTo(y[1]));
            if (uniqueHands.All(x => !x.SequenceEqual(h)))
            {
                uniqueHands.Add(h);
            }
        }
        hands = uniqueHands;
        if (toitsuKinds.Count == 7)
        {
            hands.Add(new(toitsuKinds.Select(x => new TileList(Enumerable.Repeat(x, 2)))));
        }
        hands.Sort((x, y) =>
        {
            var min = Math.Min(x.Count, y.Count);
            for (var i = 0; i < min; i++)
            {
                if (x[i] > y[i]) return 1;
                if (x[i] < y[i]) return -1;
            }
            return x.Count.CompareTo(y.Count);
        });
        return hands;
    }

    private static List<Tile> FindToitsuKinds()
    {
        // 字牌の同種3or4枚は対子になり得ない
        return AllKind.Where(x => x.IsHonor && countArray_[x] == 2 || !x.IsHonor && countArray_[x] >= 2).ToList();
    }

    private static List<TileListList> FindValidCombinations(CountArray countArray, Tile startKind, bool handNotCompleted = false)
    {
        // countArrayから指定のスートのTileKindListを作成する
        var kindList = new TileList(Enumerable.Range(startKind.Id, 9).SelectMany(x => Enumerable.Repeat(new Tile(x), countArray[x])));
        if (kindList.Count == 0) return [];
        // TileKindListのnP3全順列を列挙する
        // [1,2,3,4,4]=>[[1,2,3],[1,2,4],[1,3,2],[1,3,4],[1,4,2],[1,4,3],[1,4,4],...]
        var perms = GetPermutations(kindList, 3).Select(x => new TileList(x));
        var validPerms = new TileListList(perms.Where(x => x.IsShuntsu || x.IsKoutsu));
        if (validPerms.Count == 0) return [];
        var neededPermsCount = kindList.Count / 3;
        // あり得る順列のセットが一通りしかない
        if (neededPermsCount == validPerms.Count &&
            kindList == validPerms.Aggregate((x, y) => new(Enumerable.Concat(x, y))))
        {
            return [validPerms];
        }
        // あり得ない順子を取り除く
        foreach (var perm in validPerms.ToList())
        {
            if (!perm.IsShuntsu) continue;
            while (true)
            {
                var setsCount = validPerms.Count(x => x.SequenceEqual(perm));
                if (setsCount > 4) validPerms.Remove(perm);
                else break;
            }
        }
        // あり得ない刻子を取り除く
        foreach (var perm in validPerms.ToList())
        {
            if (!perm.IsKoutsu) continue;
            while (true)
            {
                var setsCount = validPerms.Count(x => x.SequenceEqual(perm));
                var tilesCount = kindList.Count(x => perm[0] == x) / 3;
                if (setsCount > tilesCount) validPerms.Remove(perm);
                else break;
            }
        }
        if (handNotCompleted) return [validPerms];
        // 可能な組み合わせを探す
        var combinationsResults = new List<TileListList>();
        foreach (var perm in GetPermutations(Enumerable.Range(0, validPerms.Count), neededPermsCount))
        {
            var result = perm.SelectMany(x => validPerms[x]).OrderBy(x => x.Id);
            if (!kindList.SequenceEqual(result)) continue;
            var results = new TileListList(perm.Select(x => validPerms[x]).OrderByDescending(x => x[0]));
            if (!combinationsResults.Contains(results))
            {
                combinationsResults.Add(results);
            }
        }
        return combinationsResults;
    }

    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> collection, int count)
    {
        var result = new List<IEnumerable<T>>();
        Rec(collection, count, new List<T>(), result);
        return result;

        static void Rec(IEnumerable<T> stock, int count, IEnumerable<T> current, List<IEnumerable<T>> result)
        {
            if (count == 0)
            {
                result.Add(current);
            }
            else
            {
                for (var i = 0; i < stock.Count(); i++)
                {
                    var copiedStock = stock.ToList();
                    var copiedCurrent = new List<T>(current) { copiedStock[i] };
                    copiedStock.RemoveAt(i);
                    Rec(copiedStock, count - 1, copiedCurrent, result);
                }
            }
        }
    }

    private static List<List<TileListList>> Product(List<List<TileListList>> suits)
    {
        return suits.Count switch
        {
            1 => suits,
            2 => suits[0].SelectMany(s0 => suits[1].Select(s1 => new[] { s0, s1 }.ToList())).ToList(),
            3 => suits[0].SelectMany(s0 => suits[1].SelectMany(s1 => suits[2].Select(s2 => new[] { s0, s1, s2 }.ToList()))).ToList(),
            4 => suits[0].SelectMany(s0 => suits[1].SelectMany(s1 => suits[2].SelectMany(s2 => suits[3].Select(s3 => new[] { s0, s1, s2, s3 }.ToList())))).ToList(),
            5 => suits[0].SelectMany(s0 => suits[1].SelectMany(s1 => suits[2].SelectMany(s2 => suits[3].SelectMany(s3 => suits[4].Select(s4 => new[] { s0, s1, s2, s3, s4 }.ToList()))))).ToList(),
            _ => [],
        };
    }
}