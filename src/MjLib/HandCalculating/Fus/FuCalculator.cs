using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.HandCalculating.Fus;

internal static class FuCalculator
{
    private static FuList fuList_ = new();
    private static DevidedHand hand_ = new();
    private static TileKind winTile_ = Man1;
    private static TileKindList winGroup_ = new();
    private static HandConfig config_ = new();
    private static FuuroList fuuroList_ = new();

    /// <summary>
    /// 符を計算します。
    /// </summary>
    /// <param name="hand">分割された手牌 アガリ牌含む</param>
    /// <param name="winTile">アガリ牌</param>
    /// <param name="winGroup">アガリ牌を含む面子</param>
    /// <param name="config">HandConfig</param>
    /// <param name="fuuroList">副露のリスト</param>
    /// <returns></returns>
    internal static FuList Calc(
        DevidedHand hand,
        TileKind winTile,
        TileKindList winGroup,
        HandConfig config,
        FuuroList? fuuroList = null)
    {
        fuList_ = new();
        hand_ = hand;
        winTile_ = winTile;
        winGroup_ = winGroup;
        config_ = config;
        fuuroList_ = fuuroList ?? new();

        if (hand_.Count == 7) return new() { Fu.Chiitoitsu };
        CalcAtama();
        CalcWait();
        CalcMentsu();
        CalcBase();
        return fuList_;
    }

    private static void CalcAtama()
    {
        var toitsuTile = hand_.Toitsu[0];
        // 役牌雀頭符
        // 三元牌符
        if (toitsuTile == Haku || toitsuTile == Hatsu || toitsuTile == Chun)
        {
            fuList_.Add(Fu.DragonToitsu);
        }
        // 自風牌符
        if (WindToTileKind(config_.PlayerWind) == toitsuTile)
        {
            fuList_.Add(Fu.PlayerWindToitsu);
        }
        // 場風牌符
        if (WindToTileKind(config_.RoundWind) == toitsuTile)
        {
            fuList_.Add(Fu.RoundWindToitsu);
        }
    }

    private static void CalcWait()
    {
        // ペンチャンorカンチャン待ち
        if (winGroup_.IsShuntsu)
        {
            // ペンチャン待ち[12(3)] [(7)89]
            if (winGroup_.Any(x => x.IsRoutou) &&
                (winTile_.Number == 3 && winGroup_.IndexOf(winTile_) == 2 ||
                winTile_.Number == 7 && winGroup_.IndexOf(winTile_) == 0))
            {
                fuList_.Add(Fu.Penchan);
            }
            // カンチャン待ち[1(2)3] [2(3)4] [3(4)5] [4(5)6] [5(6)7] [6(7)8] [7(8)9]
            if (winGroup_.IndexOf(winTile_) == 1)
            {
                fuList_.Add(Fu.Kanchan);
            }
        }
        // 単騎待ち
        if (winGroup_.IsToitsu)
        {
            fuList_.Add(Fu.Tanki);
        }
    }

    private static void CalcMentsu()
    {
        // 明刻扱いになるのはロンのときのアガリ面子と副露
        foreach (var minko in config_.IsTsumo ? fuuroList_.Pons.Select(x => x.Tiles) : fuuroList_.Pons.Select(x => x.Tiles).Append(winGroup_))
        {
            fuList_.Add(minko[0].IsChuchan ? Fu.ChuchanMinko : Fu.YaochuMinko);
        }
        // 暗刻扱いになるのはツモのときのアガリ面子とそれ以外の手牌
        foreach (var anko in config_.IsTsumo ? hand_.Koutsus : hand_.Koutsus.Where(x => x != winGroup_))
        {
            fuList_.Add(anko[0].IsChuchan ? Fu.ChuchanAnko : Fu.YaochuAnko);
        }
        // 明槓
        foreach (var minkan in fuuroList_.Minkans.Select(x => x.Tiles))
        {
            fuList_.Add(minkan[0].IsChuchan ? Fu.ChuchanMinkan : Fu.YaochuMinkan);
        }
        // 暗槓
        foreach (var ankan in fuuroList_.Ankans.Select(x => x.Tiles))
        {
            fuList_.Add(ankan[0].IsChuchan ? Fu.ChuchanAnkan : Fu.YaochuAnkan);
        }
    }

    private static void CalcBase()
    {
        // ピンヅモありルールなら符が無いときにツモの2符を加えない
        if (config_.IsTsumo && !fuuroList_.HasOpen && !(fuList_.Total == 0 && config_.Rurles.Pinzumo))
        {
            fuList_.Add(Fu.Tsumo);
        }
        // 食い平和のロンアガリは副底を30符にする
        if (!config_.IsTsumo && fuuroList_.HasOpen && fuList_.Total == 0)
        {
            fuList_.Add(Fu.OpenPinfuBase);
        }
        else
        {
            fuList_.Add(Fu.Base);
            // メンゼンロン
            if (!config_.IsTsumo && !fuuroList_.HasOpen)
            {
                fuList_.Add(Fu.Menzen);
            }
        }
    }

    private static TileKind WindToTileKind(Wind wind)
    {
        return wind switch
        {
            Wind.East => Ton,
            Wind.South => Nan,
            Wind.West => Sha,
            Wind.North => Pei,

            _ => throw new NotSupportedException()
        };
    }
}