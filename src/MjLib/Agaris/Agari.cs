using MjLib.TileCountArrays;
using MjLib.TileKinds;
using System.Collections.Immutable;
using static MjLib.TileKinds.Tile;

namespace MjLib.Agaris;

internal static class Agari
{
    private static CountArray counts_ = new();

    /// <summary>
    /// アガリ形かどうか判定する
    /// </summary>
    /// <param name="hand">手牌 副露を含まない手牌+アガリ牌</param>
    /// <returns>アガリ形かどうか</returns>
    public static bool IsAgari(TileList hand)
    {
        counts_ = hand.ToTileCountArray();

        // 字牌が4枚以上ならアガリ形ではない
        if (AllKind.Where(x => x.IsHonor).Any(x => counts_[x] >= 4)) return false;
        if (IsAgariForKokushi()) return true;
        // 国士無双でないのに1枚だけの字牌があるならアガリ形ではない
        if (AllKind.Where(x => x.IsHonor).Any(x => counts_[x] == 1)) return false;
        if (IsAgariForChiitoitsu()) return true;
        return IsAgariForRegular();
    }

    // 国士無双のアガリ形かどうか
    private static bool IsAgariForKokushi()
    {
        return AllKind.Where(x => x.IsYaochuu).Aggregate(1, (x, y) => x * counts_[y]) == 2;
    }

    // 七対子のアガリ形かどうか
    private static bool IsAgariForChiitoitsu()
    {
        return AllKind.Count(x => counts_[x] == 2) == 7;
    }

    private static bool IsAgariForRegular()
    {
        // (1,4,7),(2,5,8),(3,6,9)それぞれの個数を集めたもの
        // 萬子
        var man147 = counts_[Man1] + counts_[Man4] + counts_[Man7];
        var man258 = counts_[Man2] + counts_[Man5] + counts_[Man8];
        var man369 = counts_[Man3] + counts_[Man6] + counts_[Man9];
        // 筒子
        var pin147 = counts_[Pin1] + counts_[Pin4] + counts_[Pin7];
        var pin258 = counts_[Pin2] + counts_[Pin5] + counts_[Pin8];
        var pin369 = counts_[Pin3] + counts_[Pin6] + counts_[Pin9];
        // 索子
        var sou147 = counts_[Sou1] + counts_[Sou4] + counts_[Sou7];
        var sou258 = counts_[Sou2] + counts_[Sou5] + counts_[Sou8];
        var sou369 = counts_[Sou3] + counts_[Sou6] + counts_[Sou9];
        // 3つずつ取ったときの余り
        var manA = (man147 + man258 + man369) % 3;
        var pinA = (pin147 + pin258 + pin369) % 3;
        var souA = (sou147 + sou258 + sou369) % 3;
        // 数牌が1つ余っているならアガリ形ではない
        if (manA == 1 || pinA == 1 || souA == 1) return false;
        // 対子が2つ以上or数牌が2つ余っていて字牌の対子があるならアガリ形ではない
        if (new[] { manA, pinA, souA }.Concat(AllKind.Where(x => x.IsHonor).Select(x => counts_[x])).Count(x => x == 2) != 1) return false;
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
        if (AllKind.Where(x => x.IsHonor).Any(x => counts_[x] == 2))
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
    private static ImmutableList<int> ToCounts(Tile startKind)
    {
        return ImmutableList.Create(counts_[startKind.Id..(startKind.Id + 9)]);
    }

    // すべて面子で構成されているかどうか
    private static bool IsMentsu(IEnumerable<int> counts)
    {
        var counts2 = counts.ToList();
        // a:左端の値
        var (a, b, c) = (counts2[0], 0, 0);
        // 1or4個のとき、次とその次の1個で順子を構成するはず
        if (a is 1 or 4) b = c = 1;
        // 2個のとき、次とその次の2個で順子を構成するはず
        else if (a is 2) b = c = 2;
        // 左端を削除する
        counts2.RemoveAt(0);
        // 想定される個数分引かれた左端の値
        a = counts2[0] - b;
        // 想定された個数を満たしていないならアガリ形ではない
        if (a < 0) { return false; }
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
            counts2.RemoveAt(0);
            a = counts2[0] - b;
            if (a < 0) { return false; }
        }
        counts2.RemoveAt(0);
        a = counts2[0] - c;
        return a is 0 or 3;
    }

    // 対子1つと面子で構成されているかどうか
    private static bool IsHeadAndMentsu(int nokori, IEnumerable<int> counts)
    {
        var counts2 = counts.ToList();
        if (nokori == 0)
        {
            if (RemoveToitsuAndCheck(3, counts2)) { return true; }
            if (RemoveToitsuAndCheck(6, counts2)) { return true; }
            if (RemoveToitsuAndCheck(9, counts2)) { return true; }
        }
        else if (nokori == 1)
        {
            if (RemoveToitsuAndCheck(2, counts2)) { return true; }
            if (RemoveToitsuAndCheck(5, counts2)) { return true; }
            if (RemoveToitsuAndCheck(8, counts2)) { return true; }
        }
        else if (nokori == 2)
        {
            if (RemoveToitsuAndCheck(1, counts2)) { return true; }
            if (RemoveToitsuAndCheck(4, counts2)) { return true; }
            if (RemoveToitsuAndCheck(7, counts2)) { return true; }
        }
        return false;

        static bool RemoveToitsuAndCheck(int number, IEnumerable<int> counts)
        {
            var counts2 = counts.ToList();
            if (counts2[number - 1] >= 2)
            {
                counts2[number - 1] -= 2;
                if (IsMentsu(counts2)) { return true; }
            }
            return false;
        }
    }
}