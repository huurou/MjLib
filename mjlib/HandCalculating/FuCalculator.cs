using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.HandCalculating
{
    public class FuDetail : IEquatable<FuDetail>
    {
        public int Fu { get; }
        public string Reason { get; }

        public FuDetail(int fu, string reason)
        {
            Fu = fu;
            Reason = reason;
        }

        public override bool Equals(object? obj)
        {
            return obj is FuDetail other && Equals(other);
        }

        public bool Equals(FuDetail? other)
        {
            return other is not null && Fu == other.Fu && Reason == other.Reason;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    internal static class FuCalculator
    {
        public const string BASE = "base";
        public const string PENCHAN = "penchan";
        public const string KANCHAN = "kanchan";
        public const string VALUED_PAIR = "values_pair";
        public const string PAIR_WAIT = "pair_wait";
        public const string TSUMO = "tsumo";
        public const string HAND_WITHOUT_FU = "hand_without_fu";

        public const string CLOSED_PON = "closed_pon";
        public const string OPEN_PON = "open_pon";

        public const string CLOSED_TERMINAL_PON = "closed_terminal_pon";
        public const string OPEN_TERMINAL_PON = "open_terminal_pon";

        public const string CLOSED_KAN = "closed_kan";
        public const string OPEN_KAN = "open_kan";

        public const string CLOSED_TERMINAL_KAN = "closed_terminal_kan";
        public const string OPEN_TERMINAL_KAN = "open_terminal_kan";

        public static (List<FuDetail>, int) CalculateFu(IList<TileKindList> devidedHand,
            Tile winTile,
            TileKindList winGroup,
            HandConfig config,
            IEnumerable<int>? valuedTiles = null,
            IEnumerable<Meld>? melds = null)
        {
            var winTileKind = winTile.ToTileKind();
            if (valuedTiles is null)
            {
                valuedTiles = new List<int>();
            }
            if (melds is null)
            {
                melds = new List<Meld>();
            }
            var fuDetails = new List<FuDetail>();
            if (devidedHand.Count == 7)
            {
                fuDetails = new List<FuDetail>
                {
                    new FuDetail(25, BASE)
                };
                return (fuDetails, 25);
            }
            var pair = devidedHand.Where(x => x.IsPair).ElementAt(0);
            var ponSets = devidedHand.Where(x => x.IsPon);

            var openMeldsCopy = melds.Where(x => x.Type == MeldType.Chi)
                                         .Select(x => x.KindList)
                                         .ToList();
            var closedChiSets = new List<TileKindList>();
            foreach (var x in devidedHand)
            {
                if (!openMeldsCopy.Contains(x))
                {
                    closedChiSets.Add(x);
                }
                else
                {
                    openMeldsCopy.Remove(x);
                }
            }

            if (closedChiSets.Contains(winGroup))
            {
                var tileIndex = winTileKind.Simplify;
                //ペンチャン
                if (winGroup.ContainsTerminal())
                {
                    //ペン12
                    if (tileIndex == 2 && winGroup.IndexOf(winTileKind) == 2)
                    {
                        fuDetails.Add(new FuDetail(2, PENCHAN));
                    }
                    //ペン89
                    else if (tileIndex == 6 && winGroup.IndexOf(winTileKind) == 0)
                    {
                        fuDetails.Add(new FuDetail(2, PENCHAN));
                    }
                }
                //カン57
                if (winGroup.IndexOf(winTileKind) == 1)
                {
                    fuDetails.Add(new FuDetail(2, KANCHAN));
                }
            }

            //符あり雀頭
            var countOfValuedPairs = valuedTiles.Count(x => x == pair[0].Value);
            if (countOfValuedPairs == 1)
            {
                fuDetails.Add(new FuDetail(2, VALUED_PAIR));
            }

            //雀頭ダブ東南4符
            if (countOfValuedPairs == 2)
            {
                fuDetails.Add(new FuDetail(2, VALUED_PAIR));
                fuDetails.Add(new FuDetail(2, VALUED_PAIR));
            }

            //シャンポン待ち
            if (winGroup is not null && winGroup.IsPair)
            {
                fuDetails.Add(new FuDetail(2, PAIR_WAIT));
            }

            foreach (var ponSet in ponSets)
            {
                var openMelds = melds.Where(x => ponSet.Equals(x.KindList))
                                    .ToList();
                var openMeld = openMelds.Count == 0 ? null : openMelds[0];
                var setWasOpen = !(openMeld is null) && openMeld.IsOpen;
                var isKan = !(openMeld is null) &&
                    (openMeld.Type == MeldType.Kan || openMeld.Type == MeldType.Chankan);
                var isYaochu = YAOCHU_INDICES.Contains(ponSet[0].Value);

                if (!config.IsTsumo && ponSet.Equals(winGroup))
                {
                    setWasOpen = true;
                }
                if (isYaochu)
                {
                    if (isKan)
                    {
                        if (setWasOpen)
                            fuDetails.Add(new FuDetail(16, OPEN_TERMINAL_KAN));
                        else
                            fuDetails.Add(new FuDetail(32, CLOSED_TERMINAL_KAN));
                    }
                    else
                    {
                        if (setWasOpen)
                            fuDetails.Add(new FuDetail(4, OPEN_TERMINAL_PON));
                        else
                            fuDetails.Add(new FuDetail(8, CLOSED_TERMINAL_PON));
                    }
                }
                else
                {
                    if (isKan)
                    {
                        if (setWasOpen)
                            fuDetails.Add(new FuDetail(8, OPEN_KAN));
                        else
                            fuDetails.Add(new FuDetail(16, CLOSED_KAN));
                    }
                    else
                    {
                        if (setWasOpen)
                            fuDetails.Add(new FuDetail(2, OPEN_PON));
                        else
                            fuDetails.Add(new FuDetail(4, CLOSED_PON));
                    }
                }
            }
            var addTsumoFu = fuDetails.Count > 0 || !config.Rurles.FuForPinfuTsumo;

            if (config.IsTsumo && addTsumoFu)
                fuDetails.Add(new FuDetail(2, TSUMO));
            if (melds.Any(x => x.IsOpen) && fuDetails.Count == 0 && config.Rurles.FuForOpenPinfu)
                fuDetails.Add(new FuDetail(2, HAND_WITHOUT_FU));
            if (melds.Any(x => x.IsOpen) || config.IsTsumo)
                fuDetails.Add(new FuDetail(20, BASE));
            else
                fuDetails.Add(new FuDetail(30, BASE));
            return (fuDetails, RoundFu(fuDetails));
        }

        //点パネ
        private static int RoundFu(IList<FuDetail> fuDetails)
        {
            var fu = fuDetails.Select(x => x.Fu).Sum();
            return (fu + 9) / 10 * 10;
        }
    }
}