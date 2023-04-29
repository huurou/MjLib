using MjLib.Agaris;
using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.HandCalculating.Scores;
using MjLib.HandCalculating.Yakus;
using MjLib.TileKinds;
using System.Diagnostics.CodeAnalysis;
using Chiitoitsu = MjLib.HandCalculating.Yakus.Chiitoitsu;
using Tsumo = MjLib.HandCalculating.Yakus.Tsumo;

namespace MjLib.HandCalculating.Hands;

internal static class HandCalculator
{
    /// <summary>
    /// 手を計算します。
    /// </summary>
    /// <param name="hand">手牌 副露を含まない手牌+アガリ牌</param>
    /// <param name="winTile">アガリ牌</param>
    /// <param name="fuuroList">副露のリスト</param>
    /// <param name="doraIndicators">ドラ表示牌</param>
    /// <param name="uradoraIndicators">裏ドラ表示牌</param>
    /// <param name="situation">和了した時の状況</param>
    /// <returns>結果</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static HandResult Calculate(
        TileKindList hand,
        TileKind? winTile,
        FuuroList? fuuroList = null,
        TileKindList? doraIndicators = null,
        TileKindList? uradoraIndicators = null,
        WinSituation? situation = null,
        GameRules? rules = null)
    {
        winTile ??= TileKind.Man1;
        fuuroList ??= new();
        doraIndicators ??= new();
        uradoraIndicators ??= new();
        situation ??= new();
        rules ??= new();
        // 流し満貫は手牌の形に関係ないので特別にここで返す
        if (EvaluateNagashimangan(situation, rules, out var result)) return result;
        if (CheckError(hand, winTile, fuuroList, situation, out result)) return result;
        var countArray = hand.ToTileCountArray();
        var devidedHands = HandDevider.Devide(hand);
        var results = new List<HandResult>();
        // 国士無双と判定された場合天和・地和・人和以外の役は付かない
        if (Kokushimusou.Valid(countArray))
        {
            var yakuList = new YakuList
            {
                Kokushimusou13.Valid(countArray, winTile, rules)
                    ? Yaku.Kokushimusou13
                    : Yaku.Kokushimusou
            };
            EvaluateCommon(yakuList, situation, rules);
            var fu = 0;
            var han = yakuList.Sum(x => x.HanClosed);
            var score = ScoreCalcurator.Calculate(fu, han, situation, rules, true);
            result = new(fu, han, score, yakuList, new());
        }
        else
        {
            foreach (var devidedHand in devidedHands)
            {
                var winGroups = devidedHand.Where(x => x.Contains(winTile)).Distinct();
                foreach (var winGroup in winGroups)
                {
                    var yakuList = new YakuList();
                    EvaluateCommon(yakuList, situation, rules);
                    var fuList = FuCalculator.Calculate(devidedHand, winTile, winGroup, fuuroList, situation, rules);
                    EvaluateYaku(yakuList, devidedHand, winTile, winGroup, fuuroList, fuList, situation, rules);
                    var yakumanList = new YakuList(yakuList.Where(x => x.IsYakuman));
                    if (yakumanList.Any())
                    {
                        yakuList = yakumanList;
                    }
                    else if (yakuList.Any())
                    {
                        AddDora(yakuList, devidedHand, doraIndicators, uradoraIndicators, situation);
                    }
                    var han = fuuroList.HasOpen ? yakuList.Sum(x => x.HanOpen) : yakuList.Sum(x => x.HanClosed);
                    if (han == 0)
                    {
                        results.Add(new("役がありません。"));
                    }
                    else
                    {
                        var fu = fuList.Total;
                        var score = ScoreCalcurator.Calculate(fu, han, situation, rules, yakumanList.Any());
                        results.Add(new(fu, han, score, yakuList, fuList));
                    }
                }
            }
            results.Sort((x, y) => x.Han < y.Han ? 1 : x.Han > y.Han ? -1 : x.Fu < y.Fu ? 1 : x.Fu > y.Fu ? -1 : 0);
            result = results[0];
        }
        result.YakuList.Sort((x, y) => x.Id.CompareTo(y.Id));
        return result;
    }

    private static bool EvaluateNagashimangan(WinSituation situation, GameRules rules, [NotNullWhen(true)] out HandResult? result)
    {
        result = null;
        if (situation.Nagashimangan)
        {
            var (fu, han) = (30, Yaku.Nagashimangan.HanClosed);
            var score = ScoreCalcurator.Calculate(fu, han, situation, rules);
            var yakuList = new YakuList { Yaku.Nagashimangan };
            var fuList = new FuList();
            result = new(fu, han, score, yakuList, fuList);
        }
        return result is not null;
    }

    // エラーをチェックする
    private static bool CheckError(TileKindList hand, TileKind winTile, FuuroList fuuroList, WinSituation situation, [NotNullWhen(true)] out HandResult? result)
    {
        result = null;
        if (!Agari.IsAgari(hand)) result = new("手牌がアガリ形ではありません。");
        if (!hand.Contains(winTile)) result = new("手牌にアガリ牌がありません。");
        if (situation.Riichi && fuuroList.HasOpen) result = new("リーチと非面前は両立できません。");
        if (situation.DaburuRiichi && fuuroList.HasOpen) result = new("ダブルリーチと非面前は両立できません。");
        if (situation.Ippatsu && fuuroList.HasOpen) result = new("一発と非面前は両立できません。");
        if (situation.Ippatsu && !situation.Riichi && !situation.DaburuRiichi) result = new("一発はリーチorダブルリーチ時にしか成立しません。");
        if (situation.Chankan && situation.Tsumo) result = new("槍槓とツモアガリは両立できません。");
        if (situation.Rinshan && !situation.Tsumo) result = new("嶺上開花とロンアガリは両立できません。");
        if (situation.Haitei && !situation.Tsumo) result = new("海底撈月とロンアガリは両立できません。");
        if (situation.Houtei && situation.Tsumo) result = new("河底撈魚とツモアガリは両立できません。");
        if (situation.Haitei && situation.Rinshan) result = new("海底撈月と嶺上開花は両立できません。");
        if (situation.Houtei && situation.Chankan) result = new("河底撈魚と槍槓は両立できません。");
        if (situation.Tenhou && situation.IsDealer) result = new("天和はプレイヤーが親の時のみ有効です。");
        if (situation.Tenhou && !situation.Tsumo) result = new("天和とロンアガリは両立できません。");
        if (situation.Tenhou && fuuroList.Any()) result = new("副露を伴う天和は無効です。");
        if (situation.Chiihou && !situation.IsDealer) result = new("地和はプレイヤーが子の時のみ有効です。");
        if (situation.Chiihou && !situation.Tsumo) result = new("地和とロンアガリは両立できません。");
        if (situation.Chiihou && fuuroList.Any()) result = new("副露を伴う地和は無効です。");
        if (situation.Renhou && situation.IsDealer) result = new("人和はプレイヤーが子の時のみ有効です。");
        if (situation.Renhou && situation.Tsumo) result = new("人和とロンアガリは両立できません。");
        if (situation.Renhou && fuuroList.Any()) result = new("副露を伴う人和は無効です。");

        return result is not null;
    }

    // 天和・地和・人和(役満)を判定する
    private static void EvaluateCommon(YakuList yakuList, WinSituation situation, GameRules rules)
    {
        if (RenhouYakuman.Valid(situation, rules))
        {
            yakuList.Add(Yaku.RenhouYakuman);
        }
        if (Tenhou.Valid(situation))
        {
            yakuList.Add(Yaku.Tenhou);
        }
        if (Chiihou.Valid(situation))
        {
            yakuList.Add(Yaku.Chiihou);
        }
    }

    // 役を判定する
    private static void EvaluateYaku(YakuList yakuList, TileKindListList hand, TileKind winTile, TileKindList winGroup, FuuroList fuuroList, FuList fuList, WinSituation situation, GameRules rules)
    {
        EvaluateFormless(yakuList, hand, fuuroList, situation, rules);
        if (hand.Count == 7)
        {
            EvaluateChiitoisu(yakuList, hand, rules);
        }
        if (hand.Concat(fuuroList.KindLists).Any(x => x.IsShuntsu))
        {
            EvaluateShuntsu(yakuList, hand, fuuroList, fuList);
        }
        if (hand.Concat(fuuroList.KindLists).Any(x => x.IsKoutsu || x.IsKantsu))
        {
            EvaluateKoutsu(yakuList, hand, winTile, winGroup, fuuroList, situation, rules);
        }
    }

    // 形を問わない役を判定する
    private static void EvaluateFormless(YakuList yakuList, TileKindListList hand, FuuroList fuuroList, WinSituation situation, GameRules rules)
    {
        if (Tsumo.Valid(situation, fuuroList))
        {
            yakuList.Add(Yaku.Tsumo);
        }
        if (Riichi.Valid(situation))
        {
            yakuList.Add(Yaku.Riichi);
        }
        if (DaburuRiichi.Valid(situation))
        {
            yakuList.Add(Yaku.DaburuRiichi);
        }
        if (Tanyao.Valid(hand, fuuroList, rules))
        {
            yakuList.Add(Yaku.Tanyao);
        }
        if (Ippatsu.Valid(situation))
        {
            yakuList.Add(Yaku.Ippatsu);
        }
        if (Rinshan.Valid(situation))
        {
            yakuList.Add(Yaku.Rinshan);
        }
        if (Chankan.Valid(situation))
        {
            yakuList.Add(Yaku.Chankan);
        }
        if (Haitei.Valid(situation))
        {
            yakuList.Add(Yaku.Haitei);
        }
        if (Houtei.Valid(situation))
        {
            yakuList.Add(Yaku.Houtei);
        }
        if (Renhou.Valid(situation, rules))
        {
            yakuList.Add(Yaku.Renhou);
        }
        if (Honitsu.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Honitsu);
        }
        if (Chinitsu.Valid(hand))
        {
            yakuList.Add(Yaku.Chinitsu);
        }
        if (Tsuuiisou.Valid(hand))
        {
            yakuList.Add(Yaku.Tsuuiisou);
        }
        if (Honrouto.Valid(hand))
        {
            yakuList.Add(Yaku.Honroto);
        }
        if (Ryuuiisou.Valid(hand))
        {
            yakuList.Add(Yaku.Ryuuiisou);
        }
    }

    // 七対子形の役を判定する
    private static void EvaluateChiitoisu(YakuList yakuList, TileKindListList hand, GameRules rules)
    {
        if (Chiitoitsu.Valid(hand))
        {
            yakuList.Add(Yaku.Chiitoitsu);
        }
        if (Daisharin.Valid(hand, rules))
        {
            yakuList.Add(Yaku.Daisharin);
        }
    }

    // 順子が必要な役を判定する
    private static void EvaluateShuntsu(YakuList yakuList, TileKindListList hand, FuuroList fuuroList, FuList fuList)
    {
        if (Pinfu.Valid(fuList, fuuroList))
        {
            yakuList.Add(Yaku.Pinfu);
        }
        if (Chanta.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Chanta);
        }
        if (Junchan.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Junchan);
        }
        if (Ittsu.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Ittsu);
        }
        // 一盃口と二盃口は重複しない
        if (Ryanpeikou.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Ryanpeikou);
        }
        else if (Iipeikou.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Iipeikou);
        }
        if (Sanshoku.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Sanshoku);
        }
    }

    // 刻子が必要な役を判定する
    private static void EvaluateKoutsu(YakuList yakuList, TileKindListList hand, TileKind winTile, TileKindList winGroup, FuuroList fuuroList, WinSituation situation, GameRules rules)
    {
        if (Toitoihou.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Toitoihou);
        }
        if (Sanankou.Valid(hand, winGroup, fuuroList, situation))
        {
            yakuList.Add(Yaku.Sanankou);
        }
        if (Sanshokudoukou.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Sanshokudoukou);
        }
        if (Shousangen.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Shousangen);
        }
        if (Haku.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Haku);
        }
        if (Hatsu.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Hatsu);
        }
        if (Chun.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Chun);
        }
        if (PlayerWind.Valid(hand, fuuroList, situation))
        {
            yakuList.Add(Yaku.PlayerWind);
        }
        if (RoundWind.Valid(hand, fuuroList, situation))
        {
            yakuList.Add(Yaku.RoundWind);
        }
        if (Daisangen.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Daisangen);
        }
        if (Shousuushii.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Shousuushii);
        }
        if (Daisuushii.Valid(hand, fuuroList))
        {
            yakuList.Add(rules.DaburuYakuman ? Yaku.DaisuushiiDaburu : Yaku.Daisuushii);
        }
        if (Chuurenpoutou.Valid(hand))
        {
            yakuList.Add(JunseiChuurenpoutou.Valid(hand, winTile, rules)
                ? Yaku.JunseiChuurenpoutou
                : Yaku.Chuurenpoutou);
        }
        if (Suuankou.Valid(hand, winGroup, fuuroList, situation))
        {
            yakuList.Add(SuuankouTanki.Valid(hand, winGroup, winTile, fuuroList, situation, rules)
                ? Yaku.SuuankouTanki
                : Yaku.Suuankou);
        }
        if (Chinroutou.Valid(hand))
        {
            yakuList.Add(Yaku.Chinroutou);
        }
        if (Sankantsu.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Sankantsu);
        }
        if (Suukantsu.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Suukantsu);
        }
    }

    // ドラを追加する
    private static void AddDora(YakuList yakuList, TileKindListList devidedHand, TileKindList doraIndicators, TileKindList uradoraIndicators, WinSituation situation)
    {
        var hand = devidedHand.SelectMany(y => y);
        yakuList.AddRange(Enumerable.Repeat(Yaku.Dora, doraIndicators.Select(TileKind.ToRealDora).Sum(x => hand.Count(y => x == y))));
        yakuList.AddRange(Enumerable.Repeat(Yaku.Uradora, uradoraIndicators.Select(TileKind.ToRealDora).Sum(x => hand.Count(y => x == y))));
        yakuList.AddRange(Enumerable.Repeat(Yaku.Akadora, situation.Akadora));
    }
}