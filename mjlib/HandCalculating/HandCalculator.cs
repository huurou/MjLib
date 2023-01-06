using mjlib.HandCalculating.YakuList;
using mjlib.HandCalculating.YakuList.Yakuman;
using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Agari;
using static mjlib.Constants;
using static mjlib.HandCalculating.FuCalculator;
using static mjlib.HandCalculating.HandDivider;
using static mjlib.HandCalculating.ScoresCalcurator;

namespace mjlib.HandCalculating
{
    public static class HandCalculator
    {
        public static HandResult EstimateHandValue(TileList tiles,
            Tile? winTile,
            List<Meld>? melds = null,
            TileList? doraIndicators = null,
            HandConfig? config = null)
        {
            if (melds is null)
            {
                melds = new();
            }
            if (doraIndicators is null)
            {
                doraIndicators = new();
            }
            config ??= new();

            var yakus = new List<YakuBase>();
            var tileArray = tiles.ToTiles34();
            var openMelds = melds.Where(x => x.IsOpen).Select(x => x.KindList);
            var allMelds = melds.Select(x => x.KindList);
            var isOpenHand = openMelds.Any();

            if (config.IsNagashiMangan)
            {
                yakus.Add(new NagashiMangan());
                var fu = 30;
                var han = new NagashiMangan().HanClosed;
                var cost = CalculateScore(han, fu, config, false);
                return new HandResult(cost, han, fu, yakus);
            }
            if (winTile is null) throw new ArgumentException(null, nameof(winTile));

            if (!IsAgari(tiles, allMelds))
            {
                return new HandResult(error: "Hand is not winning");
            }

            if (!tiles.Contains(winTile))
            {
                return new HandResult(error: "Win tile not in the hand");
            }

            if (config.IsRiichi && isOpenHand)
            {
                return new HandResult(error: "Riichi can't be declared with open hand");
            }
            if (config.IsDaburuRiichi && isOpenHand)
            {
                return new HandResult(error: "Daburu Riichi can't be declared with open hand");
            }
            if (config.IsIppatsu && isOpenHand)
            {
                return new HandResult(error: "Ippatsu can't be declared with open hand");
            }

            if (config.IsIppatsu && !config.IsRiichi && !config.IsDaburuRiichi)
            {
                return new HandResult(error: "Ippatsu can't be declared without riichi");
            }

            var devidedHand = DivideHand(tileArray, melds);
            var results = new List<HandResult>();
            foreach (var hand in devidedHand)
            {
                var isChiitoi = new Chiitoitsu().Valid(hand);
                var valuedTiles = new List<int>
                {
                    HAKU, HATSU, CHUN,
                    config.PlayerWind, config.RoundWind,
                };
                var winGroups = FindWinGroups(winTile, hand, openMelds.ToList());
                foreach (var winGroup in winGroups)
                {
                    Score? score = null;
                    string? error = null;
                    yakus = new List<YakuBase>();
                    var han = 0;
                    var (fuDetails, fu) = CalculateFu(hand, winTile, winGroup, config, valuedTiles, melds);
                    var isPinfu = fuDetails.Count == 1 && !isChiitoi && !isOpenHand;
                    var ponSets = hand.Where(x => x.IsPon);
                    var chiSets = hand.Where(x => x.IsChi);
                    if (config.IsTsumo)
                    {
                        if (!isOpenHand)
                        {
                            yakus.Add(new Tsumo());
                        }
                    }
                    if (isPinfu)
                    {
                        yakus.Add(new Pinfu());
                    }
                    if (isChiitoi && isOpenHand) continue;
                    if (isChiitoi)
                    {
                        yakus.Add(new Chiitoitsu());
                    }
                    var isDaisharin = new Daisharin().Valid(hand);
                    if (config.Rurles.HasDaisharin && isDaisharin)
                    {
                        yakus.Add(new Daisharin());
                    }
                    if (config.IsRiichi && !config.IsDaburuRiichi)
                    {
                        yakus.Add(new Riichi());
                    }
                    if (config.IsDaburuRiichi)
                    {
                        yakus.Add(new DaburuRiichi());
                    }
                    var isTanyao = new Tanyao().Valid(hand);
                    if (isOpenHand && !config.Rurles.HasOpenTanyao)
                    {
                        isTanyao = false;
                    }
                    if (isTanyao)
                    {
                        yakus.Add(new Tanyao());
                    }
                    if (config.IsIppatsu)
                    {
                        yakus.Add(new Ippatsu());
                    }
                    if (config.IsRinshan)
                    {
                        yakus.Add(new Rinshan());
                    }
                    if (config.IsChankan)
                    {
                        yakus.Add(new Chankan());
                    }
                    if (config.IsHaitei)
                    {
                        yakus.Add(new Haitei());
                    }
                    if (config.IsHoutei)
                    {
                        yakus.Add(new Houtei());
                    }
                    if (config.IsRenhou)
                    {
                        if (config.Rurles.RenhouAsYakuman)
                        {
                            yakus.Add(new RenhouYakuman());
                        }
                        else
                        {
                            yakus.Add(new Renhou());
                        }
                    }
                    if (config.IsTenhou)
                    {
                        yakus.Add(new Tenhou());
                    }
                    if (config.IsChiihou)
                    {
                        yakus.Add(new Chiihou());
                    }
                    if (new Honitsu().Valid(hand))
                    {
                        yakus.Add(new Honitsu());
                    }
                    if (new Chinitsu().Valid(hand))
                    {
                        yakus.Add(new Chinitsu());
                    }
                    if (new Tsuuiisou().Valid(hand))
                    {
                        yakus.Add(new Tsuuiisou());
                    }
                    if (new Honroto().Valid(hand))
                    {
                        yakus.Add(new Honroto());
                    }
                    if (new Chinroutou().Valid(hand))
                    {
                        yakus.Add(new Chinroutou());
                    }
                    if (new Ryuuiisou().Valid(hand))
                    {
                        yakus.Add(new Ryuuiisou());
                    }

                    //順子が必要な役
                    if (chiSets.Any())
                    {
                        if (new Chanta().Valid(hand))
                        {
                            yakus.Add(new Chanta());
                        }
                        if (new Junchan().Valid(hand))
                        {
                            yakus.Add(new Junchan());
                        }
                        if (new Ittsu().Valid(hand))
                        {
                            yakus.Add(new Ittsu());
                        }
                        if (!isOpenHand)
                        {
                            if (new Ryanpeikou().Valid(hand))
                            {
                                yakus.Add(new Ryanpeikou());
                            }
                            else if (new Iipeiko().Valid(hand))
                            {
                                yakus.Add(new Iipeiko());
                            }
                        }
                        if (new Sanshoku().Valid(hand))
                        {
                            yakus.Add(new Sanshoku());
                        }
                    }

                    //刻子が必要な役
                    if (ponSets.Any())
                    {
                        if (new Toitoi().Valid(hand))
                        {
                            yakus.Add(new Toitoi());
                        }
                        if (new Sanankou().Valid(hand, new object[]
                        {
                            winTile, melds, config.IsTsumo
                        }))
                        {
                            yakus.Add(new Sanankou());
                        }
                        if (new SanshokuDoukou().Valid(hand))
                        {
                            yakus.Add(new SanshokuDoukou());
                        }
                        if (new Shosangen().Valid(hand))
                        {
                            yakus.Add(new Shosangen());
                        }
                        if (new Haku().Valid(hand))
                        {
                            yakus.Add(new Haku());
                        }
                        if (new Hatsu().Valid(hand))
                        {
                            yakus.Add(new Hatsu());
                        }
                        if (new Chun().Valid(hand))
                        {
                            yakus.Add(new Chun());
                        }
                        if (new YakuhaiEast().Valid(hand, new object[]
                        {
                            config.PlayerWind, config.RoundWind
                        }))
                        {
                            if (config.PlayerWind == EAST)
                            {
                                yakus.Add(new YakuhaiOfPlace());
                            }
                            if (config.RoundWind == EAST)
                            {
                                yakus.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new YakuhaiSouth().Valid(hand, new object[]
                        {
                            config.PlayerWind, config.RoundWind
                        }))
                        {
                            if (config.PlayerWind == SOUTH)
                            {
                                yakus.Add(new YakuhaiOfPlace());
                            }
                            if (config.RoundWind == SOUTH)
                            {
                                yakus.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new YakuhaiWest().Valid(hand, new object[]
                        {
                            config.PlayerWind, config.RoundWind
                        }))
                        {
                            if (config.PlayerWind == WEST)
                            {
                                yakus.Add(new YakuhaiOfPlace());
                            }
                            if (config.RoundWind == WEST)
                            {
                                yakus.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new YakuhaiNorth().Valid(hand, new object[]
                        {
                            config.PlayerWind, config.RoundWind
                        }))
                        {
                            if (config.PlayerWind == NORTH)
                            {
                                yakus.Add(new YakuhaiOfPlace());
                            }
                            if (config.RoundWind == NORTH)
                            {
                                yakus.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new Daisangen().Valid(hand))
                        {
                            yakus.Add(new Daisangen());
                        }
                        if (new Shousuushii().Valid(hand))
                        {
                            yakus.Add(new Shousuushii());
                        }
                        if (new DaiSuushi().Valid(hand))
                        {
                            if (config.Rurles.HasDoubleYakuman)
                            {
                                yakus.Add(new DaiSuushi());
                            }
                            else
                            {
                                yakus.Add(new DaiSuushi { HanOpen = 13, HanClosed = 13 });
                            }
                        }
                        if (melds.Count == 0 && new ChuurenPoutou().Valid(hand))
                        {
                            if (tileArray[winTile.Value / 4] is 2
                                or 4)
                            {
                                if (config.Rurles.HasDoubleYakuman)
                                {
                                    yakus.Add(new DaburuChuurenPoutou());
                                }
                                else
                                {
                                    yakus.Add(new DaburuChuurenPoutou { HanClosed = 13 });
                                }
                            }
                            else
                            {
                                yakus.Add(new ChuurenPoutou());
                            }
                        }
                        if (!isOpenHand && new Suuankou().Valid(hand, new object[]
                        {
                            winTile, config.IsTsumo
                        }))
                        {
                            if (tileArray[winTile.Value / 4] == 2)
                            {
                                if (config.Rurles.HasDoubleYakuman)
                                {
                                    yakus.Add(new SuuankouTanki());
                                }
                                else
                                {
                                    yakus.Add(new SuuankouTanki { HanClosed = 13 });
                                }
                            }
                            else
                            {
                                yakus.Add(new Suuankou());
                            }
                        }
                        if (new SanKantsu().Valid(hand, new object[]
                        {
                            melds
                        }))
                        {
                            yakus.Add(new SanKantsu());
                        }
                        if (new Suukantsu().Valid(hand, new object[]
                        {
                            melds
                        }))
                        {
                            yakus.Add(new Suukantsu());
                        }
                    }

                    //役満に役満以外の役は付かない
                    var yakumanList = yakus.Where(x => x.IsYakuman)
                                              .ToList();
                    if (yakumanList.Count != 0)
                    {
                        yakus = yakumanList;
                    }

                    //翻を計算する
                    foreach (var item in yakus)
                    {
                        if (isOpenHand && item.HanOpen != 0)
                        {
                            han += item.HanOpen;
                        }
                        else
                        {
                            han += item.HanClosed;
                        }
                    }
                    if (han == 0)
                    {
                        error = "There are no yaku in the hand.";
                        score = null;
                    }

                    //役満にドラは付かない
                    if (yakumanList.Count == 0)
                    {
                        var tilesForDora = tiles.ToList();
                        foreach (var meld in melds)
                        {
                            if (meld.Type is MeldType.Kan or MeldType.Chankan)
                            {
                                tilesForDora.Add(meld.Tiles[3]);
                            }
                        }
                        var countOfDora = 0;
                        var countOfAkaDora = 0;
                        foreach (var tile in tilesForDora)
                        {
                            countOfDora += PlusDora(tile, doraIndicators);
                        }
                        foreach (var tile in tilesForDora)
                        {
                            if (IsAkaDora(tile, config.Rurles.HasAkaDora))
                            {
                                countOfAkaDora++;
                            }
                        }
                        if (countOfDora != 0)
                        {
                            yakus.Add(new Dora { HanOpen = countOfDora, HanClosed = countOfDora });
                            han += countOfDora;
                        }
                        if (countOfAkaDora != 0)
                        {
                            yakus.Add(new Akadora { HanOpen = countOfAkaDora, HanClosed = countOfAkaDora });
                            han += countOfAkaDora;
                        }
                    }
                    if (string.IsNullOrEmpty(error))
                    {
                        score = CalculateScore(han, fu, config, yakumanList.Count > 0);
                    }
                    results.Add(new HandResult(score, han, fu, yakus, error, fuDetails));
                }
            }
            if (!isOpenHand && new KokushiMusou().Valid(null, new object[] { tileArray }))
            {
                if (tileArray[winTile.Value / 4] == 2)
                {
                    if (config.Rurles.HasDoubleYakuman)
                    {
                        yakus.Add(new DaburuKokushiMusou());
                    }
                    else
                    {
                        yakus.Add(new DaburuKokushiMusou { HanClosed = 13 });
                    }
                }
                else
                {
                    yakus.Add(new KokushiMusou());
                }
                if (config.IsRenhou && config.Rurles.RenhouAsYakuman)
                {
                    yakus.Add(new RenhouYakuman());
                }
                if (config.IsTenhou)
                {
                    yakus.Add(new Tenhou());
                }
                if (config.IsChiihou)
                {
                    yakus.Add(new Chiihou());
                }
                var han = 0;
                foreach (var item in yakus)
                {
                    if (isOpenHand && item.HanOpen != 0)
                    {
                        han += item.HanOpen;
                    }
                    else
                    {
                        han += item.HanClosed;
                    }
                }
                var fu = 0;
                var cost = CalculateScore(han, fu, config, yakus.Count > 0);
                yakus.Sort((x, y) => x.YakuId.CompareTo(y.YakuId));
                results.Add(new HandResult(
                    cost, han, fu, yakus, null, new List<FuDetail>()));
            }
            results.Sort((x, y) =>
                x.Han < y.Han ? 1 : x.Han > y.Han ? -1
                : x.Fu < y.Fu ? 1 : x.Fu > y.Fu ? -1 : 0);
            var resultHand = results[0];
            resultHand.Yakus?.Sort((x, y) => x.YakuId.CompareTo(y.YakuId));
            return resultHand;
        }

        private static IEnumerable<TileKindList> FindWinGroups(Tile winTile, IEnumerable<TileKindList> hand, List<TileKindList> openMelds)
        {
            var winTile34 = winTile.ToTileKind();
            var closedSetItems = new List<TileKindList>();
            foreach (var x in hand)
            {
                if (!openMelds.Contains(x))
                {
                    closedSetItems.Add(x);
                }
                else
                {
                    openMelds.Remove(x);
                }
            }
            var winGroups = closedSetItems.Where(x => x.Contains(winTile34));
            return winGroups.Distinct();
        }

        private static int PlusDora(Tile tile, TileList doraIndicators)
        {
            var tileIndex = tile.Value / 4;
            var doraCount = 0;
            foreach (var _dora in doraIndicators)
            {
                var dora = _dora.Value / 4;
                if (tileIndex < EAST)
                {
                    if (dora == 8) dora = -1;
                    if (dora == 17) dora = 8;
                    if (dora == 26) dora = 17;
                    if (tileIndex == dora + 1) doraCount++;
                }
                else
                {
                    if (dora < EAST) continue;
                    dora -= 9 * 3;
                    var tileIndexTemp = tileIndex - 9 * 3;
                    if (dora == 3) dora = -1;
                    if (dora == 6) dora = 3;
                    if (tileIndexTemp == dora + 1) doraCount++;
                }
            }
            return doraCount;
        }

        private static bool IsAkaDora(Tile tile, bool akaEnabled)
        {
            return akaEnabled
                && (tile.Value == FIVE_RED_MAN
                || tile.Value == FIVE_RED_PIN
                || tile.Value == FIVE_RED_SOU);
        }
    }
}