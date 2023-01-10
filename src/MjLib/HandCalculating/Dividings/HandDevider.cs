using MjLib.TileCountArrays;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Dividings;

internal static class HandDevider
{
    private static TileKindList hand_ = new();
    private static TileCountArray countArray_ = new();

    /// <summary>
    /// 手牌が分割されうる全組み合わせを返します。
    /// </summary>
    /// <param name="hand">手牌 副露を含まない手牌+アガリ牌</param>
    /// <returns>分割された手牌のリスト</returns>
    public static List<DevidedHand> Devide(TileKindList hand)
    {
        hand_ = hand;
        countArray_ = hand.ToTileCountArray();

        var toitsuKinds = FindToitsuKinds();

        return new List<DevidedHand>();
    }

    private static List<TileKind> FindToitsuKinds()
    {
        // 字牌の同種3or4枚は対子になり得ない
        return TileKind.AllKind.Where(x => x.IsHonor && countArray_[x] == 2 || !x.IsHonor && countArray_[x] >= 2).ToList();
    }
}