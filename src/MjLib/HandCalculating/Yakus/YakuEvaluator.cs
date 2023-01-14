using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.TileCountArrays;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal static class YakuEvaluator
{
    private static TileKindList hand_ = new();
    private static TileCountArray countArray_ = new();
    private static TileKind winTile_ = new(0);
    private static FuuroList fuuroList_ = new();
    private static HandConfig config_ = new();
    private static TileKindList doraIndicators_ = new();

    public static YakuList Evaluate(
        TileKindList hand,
        TileKind winTile,
        FuuroList? fuuroList = null,
        TileKindList? doraIndicators = null,
        HandConfig? config = null)
    {
        hand_ = hand;
        winTile_ = winTile;
        countArray_ = hand.ToTileCountArray();
        fuuroList_ = fuuroList ?? new();
        doraIndicators_ = doraIndicators ?? new();
        config_ = config ?? new();

        var countArray = hand.ToTileCountArray();
        var devidedHand = HandDevider.Devide(hand);
        var yakuList = new YakuList();
        foreach (var h in devidedHand)
        {
            var winGroups = h.Where(x => x.Contains(winTile)).Distinct();
            foreach (var winGroup in winGroups)
            {
                yakuList = EstimateYaku(h, winGroup);
            }
        }

        return yakuList;
    }

    private static YakuList EstimateYaku(TileKindListList hand, TileKindList winGroup)
    {
        var yakuList = new YakuList();
        EvaluateFormless(yakuList, hand, winGroup);
        if (hand.Count == 7)
        {
            EvaluateChiitoisu(yakuList, hand, winGroup);
        }
        if (hand.Any(x => x.IsShuntsu))
        {
            EvaluateShuntsu(yakuList, hand, winGroup);
        }
        if (hand.Any(x => x.IsKoutsu))
        {
            EvaluateKoutsu(yakuList, hand, winGroup);
        }
        return yakuList;
    }

    // 形を問わない役を判定する
    private static void EvaluateFormless(YakuList yakuList, TileKindListList hand, TileKindList winGroup)
    {
        if (Tsumo.Valid(config_, fuuroList_))
        {
            yakuList.Add(Yaku.Tsumo);
        }
        if (config_.IsRiichi && !config_.IsDaburuRiichi)
        {
            yakuList.Add(Yaku.Riichi);
        }
        if (config_.IsDaburuRiichi)
        {
            yakuList.Add(Yaku.DaburuRiichi);
        }
        if (Tanyao.Valid(hand, fuuroList_, config_.Rurles))
        {
            yakuList.Add(Yaku.Tanyao);
        }
        if (Ippatsu.Valid(config_))
        {
            yakuList.Add(Yaku.Ippatsu);
        }
        if (Rinshan.Valid(config_))
        {
            yakuList.Add(Yaku.Rinshan);
        }
        if (Chankan.Valid(config_))
        {
            yakuList.Add(Yaku.Chankan);
        }
        if (Haitei.Valid(config_))
        {
            yakuList.Add(Yaku.Haitei);
        }
        if (Houtei.Valid(config_))
        {
            yakuList.Add(Yaku.Houtei);
        }
        if (Renhou.Valid(config_, config_.Rurles))
        {
            yakuList.Add(Yaku.Renhou);
        }
        if (RenhouYakuman.Valid(config_, config_.Rurles))
        {
            yakuList.Add(Yaku.RenhouYakuman);
        }
        if (Tenhou.Valid(config_))
        {
            yakuList.Add(Yaku.Tenhou);
        }
        if (Chiihou.Valid(config_))
        {
            yakuList.Add(Yaku.Chiihou);
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
    private static void EvaluateChiitoisu(YakuList yakuList, TileKindListList hand, TileKindList winGroup)
    {
        if (Chiitoitsu.Valid(hand))
        {
            yakuList.Add(Yaku.Chiitoitsu);
        }
        if (Daisharin.Valid(hand, config_.Rurles))
        {
            yakuList.Add(Yaku.Daisharin);
        }
    }

    // 順子が必要な役を判定する
    private static void EvaluateShuntsu(YakuList yakuList, TileKindListList hand, TileKindList winGroup)
    {
        var fuList = FuCalculator.Calculate(hand, winTile_, winGroup, config_, fuuroList_);
        if (Pinfu.Valid(fuList, fuuroList_))
        {
            yakuList.Add(Yaku.Pinfu);
        }
        if (Chanta.Valid(hand))
        {
            yakuList.Add(Yaku.Chanta);
        }
        if (Junchan.Valid(hand))
        {
            yakuList.Add(Yaku.Junchan);
        }
        if (Ittsu.Valid(hand))
        {
            yakuList.Add(Yaku.Ittsu);
        }
        // 一盃口と二盃口は重複しない
        if (Ryanpeikou.Valid(hand, fuuroList_))
        {
            yakuList.Add(Yaku.Ryanpeikou);
        }
        else if (Iipeikou.Valid(hand, fuuroList_))
        {
            yakuList.Add(Yaku.Iipeikou);
        }
        if (Sanshoku.Valid(hand))
        {
            yakuList.Add(Yaku.Sanshoku);
        }
    }

    // 刻子が必要な役を判定する
    private static void EvaluateKoutsu(YakuList yakuList, TileKindListList hand, TileKindList winGroup)
    {
        if (Chinroutou.Valid(hand))
        {
            yakuList.Add(Yaku.Chinroutou);
        }
    }
}