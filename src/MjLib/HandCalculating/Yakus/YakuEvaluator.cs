using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal static class YakuEvaluator
{
    public static List<YakuList> Evaluate(
        TileKindList hand,
        TileKind winTile,
        FuuroList? fuuroList = null,
        TileKindList? doraIndicators = null,
        TileKindList? uradoraIndicators = null,
        HandConfig? config = null)
    {
        fuuroList ??= new();
        doraIndicators ??= new();
        uradoraIndicators ??= new();
        config ??= new();
        var countArray = hand.ToTileCountArray();
        var devidedHands = HandDevider.Devide(hand);
        // 国士無双と判定された場合天和・地和・人和以外の役は付かない
        if (Kokushimusou.Valid(countArray))
        {
            var yakuList = new YakuList
            {
                Kokushimusou13.Valid(countArray, winTile, config)
                    ? Yaku.Kokushimusou13
                    : Yaku.Kokushimusou
            };
            EvaluateCommon(yakuList, config);
            return new() { yakuList };
        }
        var yakuLists = new List<YakuList>();
        foreach (var devidedHand in devidedHands)
        {
            var winGroups = devidedHand.Where(x => x.Contains(winTile)).Distinct();
            foreach (var winGroup in winGroups)
            {
                var yakuList = new YakuList();
                EvaluateCommon(yakuList, config);
                EvaluateYaku(yakuList, devidedHand, winTile, winGroup, fuuroList, config);
                var yakumanList = new YakuList(yakuList.Where(x => x.IsYakuman));
                if (yakumanList.Any())
                {
                    yakuList = yakumanList;
                }
                else if (yakuList.Any())
                {
                    AddDora(yakuList, doraIndicators, uradoraIndicators, config);
                }
                yakuLists.Add(yakuList);
            }
        }
        return yakuLists;
    }

    private static void EvaluateCommon(YakuList yakuList, HandConfig config)
    {
        if (RenhouYakuman.Valid(config, config.Rurles))
        {
            yakuList.Add(Yaku.RenhouYakuman);
        }
        if (Tenhou.Valid(config))
        {
            yakuList.Add(Yaku.Tenhou);
        }
        if (Chiihou.Valid(config))
        {
            yakuList.Add(Yaku.Chiihou);
        }
    }

    private static YakuList EvaluateYaku(YakuList yakuList, TileKindListList hand, TileKind winTile, TileKindList winGroup, FuuroList fuuroList, HandConfig config)
    {
        EvaluateFormless(yakuList, hand, fuuroList, config);
        if (hand.Count == 7)
        {
            EvaluateChiitoisu(yakuList, hand, config);
        }
        if (hand.Concat(fuuroList.KindLists).Any(x => x.IsShuntsu))
        {
            EvaluateShuntsu(yakuList, hand, winTile, winGroup, fuuroList, config);
        }
        if (hand.Concat(fuuroList.KindLists).Any(x => x.IsKoutsu || x.IsKantsu))
        {
            EvaluateKoutsu(yakuList, hand, winTile, winGroup, fuuroList, config);
        }
        return yakuList;
    }

    // 形を問わない役を判定する
    private static void EvaluateFormless(YakuList yakuList, TileKindListList hand, FuuroList fuuroList, HandConfig config)
    {
        if (Tsumo.Valid(config, fuuroList))
        {
            yakuList.Add(Yaku.Tsumo);
        }
        if (Riichi.Valid(config))
        {
            yakuList.Add(Yaku.Riichi);
        }
        if (DaburuRiichi.Valid(config))
        {
            yakuList.Add(Yaku.DaburuRiichi);
        }
        if (Tanyao.Valid(hand, fuuroList, config.Rurles))
        {
            yakuList.Add(Yaku.Tanyao);
        }
        if (Ippatsu.Valid(config))
        {
            yakuList.Add(Yaku.Ippatsu);
        }
        if (Rinshan.Valid(config))
        {
            yakuList.Add(Yaku.Rinshan);
        }
        if (Chankan.Valid(config))
        {
            yakuList.Add(Yaku.Chankan);
        }
        if (Haitei.Valid(config))
        {
            yakuList.Add(Yaku.Haitei);
        }
        if (Houtei.Valid(config))
        {
            yakuList.Add(Yaku.Houtei);
        }
        if (Renhou.Valid(config, config.Rurles))
        {
            yakuList.Add(Yaku.Renhou);
        }
        if (Honitsu.Valid(hand))
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
    private static void EvaluateChiitoisu(YakuList yakuList, TileKindListList hand, HandConfig config)
    {
        if (Chiitoitsu.Valid(hand))
        {
            yakuList.Add(Yaku.Chiitoitsu);
        }
        if (Daisharin.Valid(hand, config.Rurles))
        {
            yakuList.Add(Yaku.Daisharin);
        }
    }

    // 順子が必要な役を判定する
    private static void EvaluateShuntsu(YakuList yakuList, TileKindListList hand, TileKind winTile, TileKindList winGroup, FuuroList fuuroList, HandConfig config)
    {
        var fuList = FuCalculator.Calculate(hand, winTile, winGroup, config, fuuroList);
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
    private static void EvaluateKoutsu(YakuList yakuList, TileKindListList hand, TileKind winTile, TileKindList winGroup, FuuroList fuuroList, HandConfig config)
    {
        if (Toitoihou.Valid(hand, fuuroList))
        {
            yakuList.Add(Yaku.Toitoihou);
        }
        if (Sanankou.Valid(hand, winGroup, fuuroList, config))
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
        if (PlayerWind.Valid(hand, fuuroList, config))
        {
            yakuList.Add(Yaku.PlayerWind);
        }
        if (RoundWind.Valid(hand, fuuroList, config))
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
            yakuList.Add(config.Rurles.HasDaburuYakuman ? Yaku.DaisuushiiDaburu : Yaku.Daisuushii);
        }
        if (Chuurenpoutou.Valid(hand))
        {
            yakuList.Add(JunseiChuurenpoutou.Valid(hand, winTile, config)
                ? Yaku.JunseiChuurenpoutou
                : Yaku.Chuurenpoutou);
        }
        if (Suuankou.Valid(hand, winGroup, fuuroList, config))
        {
            yakuList.Add(SuuankouTanki.Valid(hand, winGroup, winTile, fuuroList, config)
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

    private static void AddDora(YakuList yakuList, TileKindList doraIndicators, TileKindList uradoraIndicators, HandConfig config)
    {
        yakuList.AddRange(Enumerable.Repeat(Yaku.Dora, doraIndicators.Select(TileKind.ToRealDora).Count()));
        yakuList.AddRange(Enumerable.Repeat(Yaku.Uradora, uradoraIndicators.Select(TileKind.ToRealDora).Count()));
        yakuList.AddRange(Enumerable.Repeat(Yaku.Akadora, config.Akadora));
    }
}