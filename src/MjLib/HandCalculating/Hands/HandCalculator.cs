using MjLib.Agaris;
using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Scores;
using MjLib.HandCalculating.Yakus;
using MjLib.TileKinds;
using System.Diagnostics.CodeAnalysis;

namespace MjLib.HandCalculating.Hands;

internal static class HandCalculator
{
    /// <summary>
    /// 手を計算します。
    /// </summary>
    /// <param name="hand">手牌 副露を含まない手牌+アガリ牌</param>
    /// <param name="winTile"></param>
    /// <param name="fuuroList"></param>
    /// <param name="doraIndicators"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static HandResult Calculate(
        TileKindList hand,
        TileKind winTile,
        FuuroList? fuuroList = null,
        TileKindList? doraIndicators = null,
        HandConfig? config = null)
    {
        fuuroList ??= new();
        doraIndicators ??= new();
        config ??= new();

        // 流し満貫は手牌の形に関係ないので特別にここで返す
        if (CheckNagashimangan(config, out var result)) return result;
        if (CheckError(hand, winTile, fuuroList, config, out result)) return result;
        var countArray = hand.ToTileCountArray();
        var devidedHand = HandDevider.Devide(hand);
        var results = new List<HandResult>();
        foreach (var h in devidedHand)
        {
            var winGroups = h.Where(x => x.Contains(winTile)).Distinct();
            foreach (var winGroup in winGroups)
            {
            }
        }

        throw new NotImplementedException();
    }

    private static bool CheckNagashimangan(HandConfig config, [NotNullWhen(true)] out HandResult? result)
    {
        result = null;
        if (config.IsNagashimangan)
        {
            var score = ScoreCalcurator.Calculate(30, Yaku.Nagashimangan.HanClosed, config);
            var yakuList = new YakuList { Yaku.Nagashimangan };
            var fuList = new FuList();
            result = new(score, yakuList, fuList);
        }
        return result is not null;
    }

    private static bool CheckError(
        TileKindList hand,
        TileKind winTile,
        FuuroList fuuroList,
        HandConfig config,
        [NotNullWhen(true)] out HandResult? result)
    {
        result = null;
        if (!Agari.IsAgari(hand)) result = new("手牌がアガリ形ではありません。");
        if (!hand.Contains(winTile)) result = new("手牌にアガリ牌がありません。");
        if (config.IsRiichi && fuuroList.HasOpen) result = new("リーチと非面前は両立できません。");
        if (config.IsDaburuRiichi && fuuroList.HasOpen) result = new("ダブルリーチと非面前は両立できません。");
        if (config.IsIppatsu && fuuroList.HasOpen) result = new("一発と非面前は両立できません。");
        if (config.IsIppatsu && !config.IsRiichi && !config.IsDaburuRiichi) result = new("一発はリーチorダブルリーチ時にしか成立しません。");
        if (config.IsChankan && config.IsTsumo) result = new("槍槓とツモアガリは両立できません。");
        if (config.IsRinshan && !config.IsTsumo) result = new("嶺上開花とロンsアガリは両立できません。");
        if (config.IsHaitei && !config.IsTsumo) result = new("海底撈月とロンアガリは両立できません。");
        if (config.IsHoutei && config.IsTsumo) result = new("河底撈魚とツモアガリは両立できません。");
        if (config.IsHaitei && config.IsRinshan) result = new("海底撈月と嶺上開花は両立できません。");
        if (config.IsHoutei && config.IsChankan) result = new("河底撈魚と槍槓は両立できません。");
        if (config.IsTenhou && config.IsDealer) result = new("天和はプレイヤーが親の時のみ有効です。");
        if (config.IsTenhou && !config.IsTsumo) result = new("天和とロンアガリは両立できません。");
        if (config.IsTenhou && fuuroList.Any()) result = new("副露を伴う天和は無効です。");
        if (config.IsChiihou && !config.IsDealer) result = new("地和はプレイヤーが子の時のみ有効です。");
        if (config.IsChiihou && !config.IsTsumo) result = new("地和とロンアガリは両立できません。");
        if (config.IsChiihou && fuuroList.Any()) result = new("副露を伴う地和は無効です。");
        if (config.IsRenhou && !config.IsDealer) result = new("人和はプレイヤーが子の時のみ有効です。");
        if (config.IsRenhou && config.IsTsumo) result = new("人和とロンアガリは両立できません。");
        if (config.IsRenhou && fuuroList.Any()) result = new("副露を伴う人和は無効です。");

        return result is not null;
    }
}