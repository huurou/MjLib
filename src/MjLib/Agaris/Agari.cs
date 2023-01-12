using MjLib.TileCountArrays;
using MjLib.TileKinds;
using System.Collections.Immutable;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Agaris;

internal static class Agari
{
    private static TileCountArray countArray_ = new();

    /// <summary>
    /// アガリ形かどうか判定する
    /// </summary>
    /// <param name="hand">手牌 副露を含まない手牌+アガリ牌</param>
    /// <returns>アガリ形かどうか</returns>
    public static bool IsAgari(TileKindList hand)
    {
        countArray_ = hand.ToTileCountArray();

        // 字牌が4枚以上ならアガリ形ではない
        if (AllKind.Where(x => x.IsHonor).Any(x => countArray_[x] >= 4)) return false;
        if (IsAgariForKokushi()) return true;
        // 国士無双でないのに1枚だけの字牌があるならアガリ形ではない
        if (AllKind.Where(x => x.IsHonor).Any(x => countArray_[x] == 1)) return false;
        if (IsAgariForChitoitsu()) return true;
        return IsAgariForRegular();
    }

    // 国士無双のアガリ形かどうか
    private static bool IsAgariForKokushi()
    {
        return AllKind.Where(x => x.IsYaochu).Aggregate(1, (result, current) => result * countArray_[current]) == 2;
    }

    // 七対子のアガリ形かどうか
    private static bool IsAgariForChitoitsu()
    {
        return AllKind.Count(x => countArray_[x] == 2) == 7;
    }

    private static bool IsAgariForRegular()
    {
        // (1,4,7),(2,5,8),(3,6,9)それぞれの個数を集めたもの
        // 萬子
        var man147 = countArray_[Man1] + countArray_[Man4] + countArray_[Man7];
        var man258 = countArray_[Man2] + countArray_[Man5] + countArray_[Man8];
        var man369 = countArray_[Man3] + countArray_[Man6] + countArray_[Man9];
        // 筒子
        var pin147 = countArray_[Pin1] + countArray_[Pin4] + countArray_[Pin7];
        var pin258 = countArray_[Pin2] + countArray_[Pin5] + countArray_[Pin8];
        var pin369 = countArray_[Pin3] + countArray_[Pin6] + countArray_[Pin9];
        // 索子
        var sou147 = countArray_[Sou1] + countArray_[Sou4] + countArray_[Sou7];
        var sou258 = countArray_[Sou2] + countArray_[Sou5] + countArray_[Sou8];
        var sou369 = countArray_[Sou3] + countArray_[Sou6] + countArray_[Sou9];
        // 3つずつ取ったときの余り
        var manA = (man147 + man258 + man369) % 3;
        var pinA = (pin147 + pin258 + pin369) % 3;
        var souA = (sou147 + sou258 + sou369) % 3;
        // 数牌が1つ余っているならアガリ形ではない
        if (manA == 1 || pinA == 1 || souA == 1) return false;
        // 対子が2つ以上or数牌が2つ余っていて字牌の対子があるならアガリ形ではない
        if (new[] { manA, pinA, souA }.Concat(AllKind.Where(x => x.IsHonor).Select(x => countArray_[x])).Count(x => x == 2) != 1) return false;
        // 面子を消した残り
        // (1,4,7)%3=1,(2,5,8)%3=2, (3,6,9)%3=0
        // [123]=>(1*1+2*1=3)=0, [444]=>(1*3)%3=0
        // 0=>残りなしor(3,6,9)が2個残っている
        // 1=>(2,5,8)が2個残っている
        // 2=>(1,4,7)が2個残っている
        var manN = (man147 * 1 + man258 * 2) % 3;
        var pinN = (pin147 * 1 + pin258 * 2) % 3;
        var souN = (sou147 * 1 + sou258 * 2) % 3;
        // それぞれのスートの牌の個数のリスト
        var manCounts = ToCounts(Man1);
        var pinCounts = ToCounts(Pin1);
        var souCounts = ToCounts(Sou1);
        // 字牌に対子がある
        if (AllKind.Where(x => x.IsHonor).Any(x => countArray_[x] == 2))
        {
            return (manA | manN | souA | souN | pinA | pinN) == 0 &&
                IsMentsu(manCounts) &&
                IsMentsu(pinCounts) &&
                IsMentsu(souCounts);
        }
        // 萬子が2枚余っている
        if (manA == 2)
        {
            return (pinA | pinN | souA | souN) == 0 &&
                IsMentsu(pinCounts) &&
                IsMentsu(souCounts) &&
                IsHeadAndMentsu(manN, manCounts);
        }
        // 筒子が2枚余っている
        if (pinA == 2)
        {
            return (souA | souN | manA | manN) == 0 &&
                IsMentsu(souCounts) &&
                IsMentsu(manCounts) &&
                IsHeadAndMentsu(pinN, pinCounts);
        }
        // 索子が2枚余っている
        if (souA == 2)
        {
            return (manA | manN | pinA | pinN) == 0 &&
                IsMentsu(manCounts) &&
                IsMentsu(pinCounts) &&
                IsHeadAndMentsu(souN, souCounts);
        }
        return false;
    }

    // あるスートについての牌の個数のリストを返す
    private static ImmutableList<int> ToCounts(TileKind startKind)
    {
        return ImmutableList.Create(countArray_[startKind.Id..(startKind.Id + 9)]);
    }

    // すべて面子で構成されているかどうか
    private static bool IsMentsu(ImmutableList<int> counts)
    {
        // a:左端の値
        var (a, b, c) = (counts[0], 0, 0);
        // 1or4個のとき、次とその次の1個で順子を構成するはず
        if (a is 1 or 4) b = c = 1;
        // 2個のとき、次とその次の2個で順子を構成するはず
        else if (a is 2) b = c = 2;
        // 左端を削除する
        counts = counts.RemoveAt(0);
        // 想定される個数分引かれた左端の値
        a = counts[0] - b;
        // 想定された個数を満たしていないならアガリ形ではない
        if (a < 0) return false;
        for (var _ = 0; _ < 6; _++)
        {
            (b, c) = (c, 0);
            if (a is 1 or 4)
            {
                b++;
                c++;
            }
            if (a is 2)
            {
                b += 2;
                c += 2;
            }
            counts = counts.RemoveAt(0);
            a = counts[0] - b;
            if (a < 0) return false;
        }
        counts = counts.RemoveAt(0);
        a = counts[0] - c;
        return a is 0 or 3;
    }

    // 対子1つと面子で構成されているかどうか
    private static bool IsHeadAndMentsu(int nokori, ImmutableList<int> counts)
    {
        if (nokori == 0)
        {
            if (RemoveToitsuAndCheck(3, counts)) return true;
            if (RemoveToitsuAndCheck(6, counts)) return true;
            if (RemoveToitsuAndCheck(9, counts)) return true;
        }
        else if (nokori == 1)
        {
            if (RemoveToitsuAndCheck(2, counts)) return true;
            if (RemoveToitsuAndCheck(5, counts)) return true;
            if (RemoveToitsuAndCheck(8, counts)) return true;
        }
        else if (nokori == 2)
        {
            if (RemoveToitsuAndCheck(1, counts)) return true;
            if (RemoveToitsuAndCheck(4, counts)) return true;
            if (RemoveToitsuAndCheck(7, counts)) return true;
        }
        return false;

        static bool RemoveToitsuAndCheck(int number, ImmutableList<int> counts)
        {
            if (counts[number - 1] >= 2)
            {
                counts = counts.SetItem(number - 1, counts[number - 1] - 2);
                if (IsMentsu(counts)) return true;
            }
            return false;
        }
    }
}