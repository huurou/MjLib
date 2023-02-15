using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.HandCalculating;

internal static class HandDivider
{
    public static IList<List<TileKindList>> DivideHand(Tiles34 tiles34, IEnumerable<Meld>? melds = null)
    {
        if (melds is null)
        {
            melds = new List<Meld>();
        }

        var closedArray = new Tiles34(tiles34.Select(x => x));
        var openKindList = melds.Any()
            ? melds.Select(x => x.KindList).Aggregate((x, y) => new TileKindList(Enumerable.Concat(x, y)))
            : new TileKindList();
        foreach (var openItem in openKindList)
        {
            closedArray[openItem.Value]--;
        }
        var pairs = FindPairs(closedArray);

        var hands = new List<List<TileKindList>>();

        foreach (var pair in pairs)
        {
            var copiedArray = new Tiles34(tiles34.Select(x => x));

            //すでに鳴いている牌は形が決まっているので外す
            foreach (var openItem in openKindList)
            {
                copiedArray[openItem.Value]--;
            }

            //雀頭候補を外す
            copiedArray[pair.Value] -= 2;

            var man = FindValidCombinations(copiedArray, 0, 8);
            var pin = FindValidCombinations(copiedArray, 9, 17);
            var sou = FindValidCombinations(copiedArray, 18, 26);

            var honor = new List<IEnumerable<int>>();
            var honors = new List<IEnumerable<TileKindList>>();
            foreach (var x in HONOR_INDICES)
            {
                if (copiedArray[x] == 3)
                {
                    honor.Add(Enumerable.Repeat(x, 3));
                }
            }
            if (honor.Count != 0)
            {
                honors = new List<IEnumerable<TileKindList>>
                {
                    honor.Select(x => new TileKindList(x))
                };
            }

            var suits = new List<IEnumerable<IEnumerable<TileKindList>>>
            {
                new List<IEnumerable<TileKindList>>
                {
                    new List<TileKindList>
                    {
                        new TileKindList(Enumerable.Repeat(pair.Value,2))
                    }
                }
            };
            if (man.Any())
            {
                suits.Add(man);
            }
            if (pin.Any())
            {
                suits.Add(pin);
            }
            if (sou.Any())
            {
                suits.Add(sou);
            }
            if (honors.Count != 0)
            {
                suits.Add(honors);
            }
            foreach (var meld in melds)
            {
                suits.Add(new List<IEnumerable<TileKindList>>
                {
                    new List<TileKindList>
                    {
                        meld.KindList
                    }
                });
            }

            foreach (var s in Product(suits))
            {
                var hand = new List<TileKindList>();
                foreach (var item in s)
                {
                    foreach (var x in item)
                    {
                        
                        hand.Add(x);
                    }
                }
                if (hand.Count == 5)
                {
                    hand.Sort((x, y) => x[0].CompareTo(y[0]));
                    hands.Add(hand);
                }
            }
        }
        var unique_hands = new List<List<TileKindList>>();
        foreach (var hand in hands)
        {
            hand.Sort((x, y) => x[0].CompareTo(y[0]) > 0 ? 1
                    : x[0].CompareTo(y[0]) < 0 ? -1
                    : x[1].CompareTo(y[1]));
            if (unique_hands.All(x =>
            {
                var lx = x.ToList();
                var lhands = hand.ToList();
                for (var i = 0; i < lx.Count; i++)
                {
                    if (!lx[i].Equals(lhands[i])) return true;
                }
                return false;
            }))
            {
                unique_hands.Add(hand);
            }
        }
        hands = unique_hands;

        if (pairs.Count == 7)
        {
            var hand = new List<TileKindList>();
            foreach (var index in pairs)
            {
                hand.Add(new TileKindList(Enumerable.Repeat(index.Value, 2)));
            }
            hands.Add(hand);
        }
        hands.Sort((x, y) =>
        {
            var min = Math.Min(x.Count, y.Count);
            for (var i = 0; i < min; i++)
            {
                if (x[i].CompareTo(y[i]) > 0) return 1;
                if (x[i].CompareTo(y[i]) < 0) return -1;
            }
            return x.Count > y.Count ? 1 : x.Count < y.Count ? -1 : 0;
        });
        return hands;
    }

    private static TileKindList FindPairs(Tiles34 tiles34,
        int firstIndex = 0,
        int secondIndex = 33)
    {
        var pairIndices = new TileKindList();
        for (var x = firstIndex; x <= secondIndex; x++)
        {
            //字牌の刻子は無視する（途中で分断して対子にはできない）
            if (HONOR_INDICES.Contains(x) && tiles34[x] != 2) continue;

            if (tiles34[x] >= 2)
            {
                pairIndices.Add(new TileKind(x));
            }
        }
        return pairIndices;
    }

    private static IEnumerable<IEnumerable<TileKindList>> FindValidCombinations(Tiles34 tiles34,
        int firstIndex,
        int secondIndex,
        bool handNotCompleted = false)
    {
        //Tiles34[0,1,1,1,2,...]=>TileKinds[1,2,3,4,4,...]
        var kindList = new TileKindList();
        for (var x = firstIndex; x <= secondIndex; x++)
        {
            if (tiles34[x] > 0)
            {
                var l = kindList.ToList();
                l.AddRange(Enumerable.Repeat(new TileKind(x), tiles34[x]));
                kindList = new TileKindList(l);
            }
        }
        if (kindList.Count == 0)
            return new List<IEnumerable<TileKindList>>();

        //TileKindsのnP3全順列を列挙
        //[1,2,3,4,4]=>[[1,2,3],[1,2,4],[1,3,2],[1,3,4],[1,4,2],[1,4,3],[1,4,4],...]
        var t = kindList.Permutations(3);
        var allPerms = t.Select(x => new TileKindList(x));

        //刻子、順子の形をしている順列を抜きだす
        var validPerms = new List<TileKindList>();
        foreach (var perm in allPerms)
        {
            if (perm.IsChi || perm.IsPon)
            {
                validPerms.Add(perm);
            }
        }
        if (validPerms.Count == 0)
            return new List<IEnumerable<TileKindList>>();

        var neededPermsCount = kindList.Count / 3;

        //有り得る順列のセットが一通りしかないとき
        if (neededPermsCount == validPerms.Count
            && kindList.Equals(validPerms.Aggregate(
                    (x, y) => new TileKindList(Enumerable.Concat(x, y)))))
            return new List<IEnumerable<TileKindList>> { validPerms };

        foreach (var perm in validPerms.ToList())
        {
            if (!perm.IsPon) continue;
            var countOfSets = 1;
            var countOfTiles = 0;
            while (countOfSets > countOfTiles)
            {
                countOfTiles = kindList.Count(x => perm[0].Equals(x)) / 3;
                countOfSets = validPerms.Count(x => perm[0].Equals(x[0])
                                                            && perm[1].Equals(x[1])
                                                            && perm[2].Equals(x[2]));
                if (countOfSets > countOfTiles)
                {
                    validPerms.Remove(perm);
                }
            }
        }
        foreach (var perm in validPerms.ToList())
        {
            if (!perm.IsChi) continue;
            var countOfSets = 5;
            var countOfPossibleSets = 4;
            while (countOfSets > countOfPossibleSets)
            {
                countOfSets = validPerms.Count(x => perm[0].Equals(x[0])
                                                            && perm[1].Equals(x[1])
                                                            && perm[2].Equals(x[2]));
                if (countOfSets > countOfPossibleSets)
                {
                    validPerms.Remove(perm);
                }
            }
        }

        if (handNotCompleted)
            return new List<IEnumerable<TileKindList>>() { validPerms };

        var possibleCombinations =
            new TileKindList(Enumerable.Range(0, validPerms.Count))
                .Permutations(neededPermsCount);

        var combinationsResults = new List<IEnumerable<TileKindList>>();
        foreach (var perm in possibleCombinations)
        {
            var res = new List<TileKind>();
            foreach (var item in perm)
            {
                res.AddRange(validPerms[item.Value]);
            }
            res.Sort((x, y) => x.CompareTo(y));

            if (!kindList.Equals(new TileKindList(res))) continue;

            var results = new List<TileKindList>();
            foreach (var item in perm)
            {
                results.Add(validPerms[item.Value]);
            }
            results.Sort((x, y) => x[0].CompareTo(y[0]));
            if (!combinationsResults.Contains_(results))
            {
                combinationsResults.Add(results);
            }
        }
        return combinationsResults;
    }

    /// <summary>
    /// 各TileKindsが取りうる順列を列挙します。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IList<TileKindList> Permutations(this TileKindList source, int count)
    {
        var result = new List<TileKindList>();
        PermutationsInnner(source, count, new TileKindList(), result);
        return result;
    }

    private static void PermutationsInnner(TileKindList stock, int depth, TileKindList current, IList<TileKindList> result)
    {
        if (depth == 0)
        {
            result.Add(current);
            return;
        }
        for (var i = 0; i < stock.Count; i++)
        {
            var copyOfStock = new TileKindList(stock);
            var copyOfCurrent = new TileKindList(current) { copyOfStock[i] };
            copyOfStock.RemoveAt(i);
            PermutationsInnner(copyOfStock, depth - 1, copyOfCurrent, result);
        }
    }

    private static bool Contains_<T>(this IEnumerable<T> source, T item) where T : IEnumerable<TileKindList>
    {
        return source.Any(x =>
        {
            var y = x.ToList();
            var z = item.ToList();
            if (y.Count != z.Count) return false;
            for (var i = 0; i < y.Count; i++)
            {
                if (!z[i].Equals(y[i])) return false;
            }
            return true;
        });
    }

    /// <summary>
    /// 各色の牌姿の直積を求めます。
    /// </summary>
    /// <param name="suits"></param>
    /// <returns></returns>
    private static IEnumerable<IEnumerable<IEnumerable<TileKindList>>> Product(IList<IEnumerable<IEnumerable<TileKindList>>> suits)
    {
        var result = new List<IEnumerable<IEnumerable<TileKindList>>>();
        return suits.Count switch
        {
            1 => suits,
            2 => suits[0].SelectMany(i => suits[1].Select(j => new[] { i, j }.AsEnumerable())).ToList(),
            3 => suits[0].SelectMany(i => suits[1].SelectMany(j => suits[2].Select(k => new[] { i, j, k }.AsEnumerable()))).ToList(),
            4 => suits[0].SelectMany(i => suits[1].SelectMany(j => suits[2].SelectMany(k => suits[3].Select(l => new[] { i, j, k, l }.AsEnumerable())))).ToList(),
            5 => suits[0].SelectMany(i => suits[1].SelectMany(j => suits[2].SelectMany(k => suits[3].SelectMany(l => suits[4].Select(m => new[] { i, j, k, l, m }.AsEnumerable()))))).ToList(),
            _ => result,
        };
        //var countOfArrays = suits.Count;
        //if (countOfArrays == 1)
        //{
        //    return suits;
        //}
        //var result = new List<IEnumerable<IEnumerable<TileKindList>>>();
        //if (countOfArrays == 2)
        //{
        //    foreach (var i in suits[0])
        //    {
        //        foreach (var j in suits[1])
        //        {
        //            result.Add(new List<IEnumerable<TileKindList>> { i, j });
        //        }
        //    }
        //}
        //if (countOfArrays == 3)
        //{
        //    foreach (var i in suits[0])
        //    {
        //        foreach (var j in suits[1])
        //        {
        //            foreach (var k in suits[2])
        //            {
        //                result.Add(new List<IEnumerable<TileKindList>> { i, j, k });
        //            }
        //        }
        //    }
        //}
        //if (countOfArrays == 4)
        //{
        //    foreach (var i in suits[0])
        //    {
        //        foreach (var j in suits[1])
        //        {
        //            foreach (var k in suits[2])
        //            {
        //                foreach (var l in suits[3])
        //                {
        //                    result.Add(new List<IEnumerable<TileKindList>> { i, j, k, l });
        //                }
        //            }
        //        }
        //    }
        //}
        //if (countOfArrays == 5)
        //{
        //    foreach (var i in suits[0])
        //    {
        //        foreach (var j in suits[1])
        //        {
        //            foreach (var k in suits[2])
        //            {
        //                foreach (var l in suits[3])
        //                {
        //                    foreach (var m in suits[4])
        //                    {
        //                        result.Add(new List<IEnumerable<TileKindList>> { i, j, k, l, m });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //return result;
    }
}