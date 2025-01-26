using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;
using static MjLib.TileKinds.Tile;

namespace MjLib.HandCalculating.Fus;

internal static class FuCalculator
{
    private static FuList fuList_ = [];
    private static TileListList hand_ = [];
    private static Tile winTile_ = Man1;
    private static TileList winGroup_ = [];
    private static FuuroList fuuroList_ = [];
    private static WinSituation situation_ = new();
    private static GameRules rules_ = new();

    /// <summary>
    /// 符を計算します。
    /// </summary>
    /// <param name="hand">分割された手牌 アガリ牌含む</param>
    /// <param name="winTile">アガリ牌</param>
    /// <param name="winGroup">アガリ牌を含む面子</param>
    /// <param name="fuuroList">副露のリスト</param>
    /// <param name="situation">和了したときの状況</param>
    /// <returns></returns>
    internal static FuList Calculate(
        TileListList hand,
        Tile winTile,
        TileList winGroup,
        FuuroList? fuuroList = null,
        WinSituation? situation = null,
        GameRules? rules = null)
    {
        fuList_ = [];
        hand_ = hand;
        winTile_ = winTile;
        winGroup_ = winGroup;
        fuuroList_ = fuuroList ?? [];
        situation_ = situation ?? new();
        rules_ = rules ?? new();

        if (hand_.Count == 7) return [Fu.Chiitoitsu];
        CalcJantou();
        CalcWait();
        CalcMentsu();
        CalcBase();
        fuList_.Sort((x, y) => x.Id.CompareTo(y.Id));
        return fuList_;
    }

    private static void CalcJantou()
    {
        var toitsuTile = hand_.First(x => x.IsToitsu).ElementAt(0);
        // 役牌雀頭符
        // 三元牌符
        if (toitsuTile == Haku || toitsuTile == Hatsu || toitsuTile == Chun)
        {
            fuList_.Add(Fu.DragonToitsu);
        }
        // 自風牌符
        if (WindToTileKind(situation_.Player) == toitsuTile)
        {
            fuList_.Add(Fu.PlayerWindToitsu);
        }
        // 場風牌符
        if (WindToTileKind(situation_.Round) == toitsuTile)
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
            if (winTile_.Number == 3 && winGroup_.IndexOf(winTile_) == 2 ||
                winTile_.Number == 7 && winGroup_.IndexOf(winTile_) == 0)
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
        //副露の明刻
        foreach (var minko in fuuroList_.Where(x => x.IsPon).Select(x => x.Tiles))
        {
            fuList_.Add(minko[0].IsChuuchan ? Fu.ChuuchanMinko : Fu.YaochuuMinko);
        }
        // シャンポン待ちロンアガリのとき明刻扱いになる
        if (!situation_.Tsumo && winGroup_.IsKoutsu)
        {
            fuList_.Add(winGroup_[0].IsChuuchan ? Fu.ChuuchanMinko : Fu.YaochuuMinko);
        }
        // 手牌の暗刻
        foreach (var anko in hand_.Where(x => x.IsKoutsu && x != winGroup_))
        {
            fuList_.Add(anko[0].IsChuuchan ? Fu.ChuuchanAnko : Fu.YaochuuAnko);
        }
        // シャンポン待ちツモアガリのとき暗刻扱いになる
        if (situation_.Tsumo && winGroup_.IsKoutsu)
        {
            fuList_.Add(winGroup_[0].IsChuuchan ? Fu.ChuuchanAnko : Fu.YaochuuAnko);
        }
        // 明槓
        foreach (var minkan in fuuroList_.Where(x => x.IsMinkan).Select(x => x.Tiles))
        {
            fuList_.Add(minkan[0].IsChuuchan ? Fu.ChuuchanMinkan : Fu.YaochuuMinkan);
        }
        // 暗槓
        foreach (var ankan in fuuroList_.Where(x => x.IsAnkan).Select(x => x.Tiles))
        {
            fuList_.Add(ankan[0].IsChuuchan ? Fu.ChuuchanAnkan : Fu.YaochuuAnkan);
        }
    }

    private static void CalcBase()
    {
        // ピンヅモありで符が無くて面前でツモのときツモの2符を加えない
        if (situation_.Tsumo)
        {
            if (rules_.Pinzumo && fuList_.Total == 0 && !fuuroList_.HasOpen) { }
            else fuList_.Add(Fu.Tsumo);
        }
        // 食い平和のロンアガリは副底を30符にする
        if (!situation_.Tsumo && fuuroList_.HasOpen && fuList_.Total == 0)
        {
            fuList_.Add(Fu.OpenPinfuBase);
        }
        else
        {
            fuList_.Add(Fu.Base);
            // メンゼンロン
            if (!situation_.Tsumo && !fuuroList_.HasOpen)
            {
                fuList_.Add(Fu.Menzen);
            }
        }
    }

    private static Tile WindToTileKind(Wind wind)
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