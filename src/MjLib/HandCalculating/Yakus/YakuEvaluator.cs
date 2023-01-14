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
                if (Yaku.Chiitoitsu.Valid(h) && fuuroList_.HasOpen) continue;
                yakuList = F(h, winGroup);
            }
        }

        return yakuList;
    }

    private static YakuList F(TileKindListList hand, TileKindList winGroup)
    {
        var yakuList = new YakuList();
        if (Yaku.Tsumo.Valid(config_, fuuroList_))
        {
            yakuList.Add(Yaku.Tsumo);
        }
        var fuList = FuCalculator.Calculate(hand, winTile_, winGroup, config_, fuuroList_);
        if (Yaku.Pinfu.Valid(fuList, fuuroList_))
        {
            yakuList.Add(Yaku.Pinfu);
        }
        if (Yaku.Chiitoitsu.Valid(hand))
        {
            yakuList.Add(Yaku.Chiitoitsu);
        }
        if (Yaku.Daisharin.Valid(hand, config_.Rurles))
        {
            yakuList.Add(Yaku.Daisharin);
        }
        if (config_.IsRiichi && !config_.IsDaburuRiichi)
        {
            yakuList.Add(Yaku.Riichi);
        }
        if (config_.IsDaburuRiichi)
        {
            yakuList.Add(Yaku.DaburuRiichi);
        }
        if (Yaku.Tanyao.Valid(hand, fuuroList_, config_.Rurles))
        {
            yakuList.Add(Yaku.Tanyao);
        }
        if (Yaku.Ippatsu.Valid(config_))
        {
            yakuList.Add(Yaku.Ippatsu);
        }
        if (Yaku.Rinshan.Valid(config_))
        {
            yakuList.Add(Yaku.Rinshan);
        }
        if (Yaku.Chankan.Valid(config_))
        {
            yakuList.Add(Yaku.Chankan);
        }
        if (Yaku.Haitei.Valid(config_))
        {
            yakuList.Add(Yaku.Haitei);
        }
        if (Yaku.Houtei.Valid(config_))
        {
            yakuList.Add(Yaku.Houtei);
        }
        if (Yaku.Renhou.Valid(config_, config_.Rurles))
        {
            yakuList.Add(Yaku.Renhou);
        }
        if (Yaku.RenhouYakuman.Valid(config_, config_.Rurles))
        {
            yakuList.Add(Yaku.RenhouYakuman);
        }
        if (Yaku.Tenhou.Valid(config_))
        {
            yakuList.Add(Yaku.Tenhou);
        }
        if (Yaku.Chiihou.Valid(config_))
        {
            yakuList.Add(Yaku.Chiihou);
        }
        if (Yaku.Honitsu.Valid()){
            yakuList.Add(Yaku.Honitsu);
        }

        return yakuList;
    }
}