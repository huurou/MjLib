using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace mjlib
{
    public static class Shanten
    {
        public const int AGARI_STATE = -1;

        private static Tiles34 array = new();
        private static int meldsCount = 0;
        private static int tatsuCount = 0;
        private static int pairsCount = 0;
        private static int jidahaiCount = 0;
        private static int honorBits = 0;
        private static int isolatedBits = 0;
        private static int shanten = 0;

        /// <summary>
        /// シャンテン数を計算する 0:テンパイ, -1: あがり
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="openSets"></param>
        /// <param name="chiitoitsu"></param>
        /// <param name="kokushimusou"></param>
        /// <returns></returns>
        public static int CalculateShanten(TileList hand, IList<TileKindList>? openSets = null,
            bool chiitoitsu = true, bool kokushimusou = true)
        {
            array = hand.ToTiles34();
            Init(array);
            var countOfTiles = array.Sum();
            if (countOfTiles > 14) throw new ArgumentException("牌の数が14個より多いです。", nameof(hand));

            if (!(openSets is null || openSets.Count == 0))
            {
                var isolatedTiles = Shanten.array.FindIsolatedTileIndices();
                foreach (var meld in openSets)
                {
                    if (isolatedTiles.Count == 0) break;

                    var lastIndex = isolatedTiles.Count - 1;
                    var isolatedTile = isolatedTiles[lastIndex];
                    isolatedTiles.RemoveAt(lastIndex);

                    array[meld[0].Value] -= 1;
                    array[meld[1].Value] -= 1;
                    array[meld[2].Value] -= 1;
                    array[isolatedTile.Value] = 3;
                }
            }

            if (openSets is null || openSets.Count == 0)
            {
                shanten = ScanChiitoitsuAndKokushi(chiitoitsu, kokushimusou);
            }

            RemoveCharacterTiles(countOfTiles);

            var initMentsu = (14 - countOfTiles) / 3;
            Scan(initMentsu);
            return shanten;
        }

        private static void Init(Tiles34 tiles)
        {
            array = tiles;
            meldsCount = 0;
            tatsuCount = 0;
            pairsCount = 0;
            jidahaiCount = 0;
            honorBits = 0;
            isolatedBits = 0;
            shanten = 8;
        }

        private static int ScanChiitoitsuAndKokushi(bool chiitoitsu, bool kokushi)
        {
            var shanten = Shanten.shanten;
            var yaochuIndices = new List<int>
            {
                0, 8, 9, 17, 18,
                26, 27, 28, 29, 30, 31, 32, 33
            };

            var completedTerminals = 0;
            foreach (var i in yaochuIndices)
            {
                completedTerminals += array[i] >= 2 ? 1 : 0;
            }

            var terminals = 0;
            foreach (var i in yaochuIndices)
            {
                terminals += array[i] != 0 ? 1 : 0;
            }

            var chuchanIndices = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7,
                10, 11, 12, 13, 14, 15, 16,
                19, 20, 21, 22, 23, 24, 25
            };

            var completedPairs = completedTerminals;
            foreach (var i in chuchanIndices)
            {
                completedPairs += array[i] >= 2 ? 1 : 0;
            }

            var pairs = terminals;
            foreach (var i in chuchanIndices)
            {
                pairs += array[i] != 0 ? 1 : 0;
            }

            //七対子のシャンテン数: 6-対子
            if (chiitoitsu)
            {
                var retShanten = 6 - completedPairs + (pairs < 7 ? 7 - pairs : 0);
                if (retShanten < shanten)
                {
                    shanten = retShanten;
                }
            }

            //国士無双のシャンテン数: 13-么九牌
            if (kokushi)
            {
                var retShanten = 13 - terminals - (completedTerminals != 0 ? 1 : 0);
                if (retShanten < shanten)
                {
                    shanten = retShanten;
                }
            }
            return shanten;
        }

        private static void RemoveCharacterTiles(int countOfTiles)
        {
            var number = 0;
            var isoleted = 0;
            for (var i = 27; i < 34; i++)
            {
                if (array[i] == 4)
                {
                    meldsCount += 1;
                    jidahaiCount += 1;
                    number |= 1 << (i - 27);
                    isoleted |= 1 << (i - 27);
                }
                if (array[i] == 3)
                {
                    meldsCount += 1;
                }
                if (array[i] == 2)
                {
                    pairsCount += 1;
                }
                if (array[i] == 1)
                {
                    isoleted |= i << (i - 27);
                }
            }
            if (jidahaiCount != 0 && (countOfTiles % 3) == 2)
            {
                jidahaiCount -= 1;
            }
            if (isoleted != 0)
            {
                isolatedBits |= 1 << 27;
                if ((number | isoleted) == number)
                {
                    honorBits |= 1 << 27;
                }
            }
        }

        private static void Scan(int initMentsu)
        {
            honorBits = 0;
            for (var i = 0; i < 27; i++)
            {
                honorBits |= array[i] == 4 ? 1 << i : 0;
            }
            meldsCount += initMentsu;
            Run(0);
        }

        private static void Run(int depth)
        {
            if (shanten == AGARI_STATE) return;

            //牌が存在するインデックスまで移動
            while (array[depth] == 0)
            {
                depth += 1;
                if (depth >= 27) break;
            }
            if (depth >= 27)
            {
                UpdateResult();
                return;
            }
            var i = depth;
            if (i > 8)
            {
                i -= 9;
            }
            if (i > 8)
            {
                i -= 9;
            }
            if (array[depth] == 4)
            {
                IncreaseSet(depth);
                if (i < 7 && array[depth + 2] != 0)
                {
                    if (array[depth + 1] != 0)
                    {
                        IncreaseSyuntsu(depth);
                        Run(depth + 1);
                        DecreaseSyuntsu(depth);
                    }
                    IncreaseTatsuSecond(depth);
                    Run(depth + 1);
                    DecreaseTatsuSecond(depth);
                }
                if (i < 8 && array[depth + 1] != 0)
                {
                    IncreaseTatsuFirst(depth);
                    Run(depth + 1);
                    DecreaseTatsuFirst(depth);
                }
                IncreaseIsolatedTile(depth);
                Run(depth + 1);
                DecreaseIsolatedTile(depth);
                DecreaseSet(depth);
                IncreasePair(depth);

                if (i < 7 && array[depth + 2] != 0)
                {
                    if (array[depth + 1] != 0)
                    {
                        IncreaseSyuntsu(depth);
                        Run(depth);
                        DecreaseSyuntsu(depth);
                    }
                    IncreaseTatsuSecond(depth);
                    Run(depth + 1);
                    DecreaseTatsuSecond(depth);
                }
                if (i < 8 && array[depth + 1] != 0)
                {
                    IncreaseTatsuFirst(depth);
                    Run(depth + 1);
                    DecreaseTatsuFirst(depth);
                }
                DecreasePair(depth);
            }
            if (array[depth] == 3)
            {
                IncreaseSet(depth);
                Run(depth + 1);
                DecreaseSet(depth);
                IncreasePair(depth);
                if (i < 7 && array[depth + 1] != 0 && array[depth + 2] != 0)
                {
                    IncreaseSyuntsu(depth);
                    Run(depth + 1);
                    DecreaseSyuntsu(depth);
                }
                else
                {
                    if (i < 7 && array[depth + 2] != 0)
                    {
                        IncreaseTatsuSecond(depth);
                        Run(depth + 1);
                        DecreaseTatsuSecond(depth);
                    }
                    if (i < 8 && array[depth + 1] != 0)
                    {
                        IncreaseTatsuFirst(depth);
                        Run(depth + 1);
                        DecreaseTatsuFirst(depth);
                    }
                }
                DecreasePair(depth);

                if (i < 7 && array[depth + 2] >= 2 && array[depth + 1] >= 2)
                {
                    IncreaseSyuntsu(depth);
                    IncreaseSyuntsu(depth);
                    Run(depth);
                    DecreaseSyuntsu(depth);
                    DecreaseSyuntsu(depth);
                }
            }
            if (array[depth] == 2)
            {
                IncreasePair(depth);
                Run(depth + 1);
                DecreasePair(depth);
                if (1 < 7 && array[depth + 2] != 0 && array[depth + 1] != 0)
                {
                    IncreaseSyuntsu(depth);
                    Run(depth);
                    DecreaseSyuntsu(depth);
                }
            }
            if (array[depth] == 1)
            {
                if (i < 6 && array[depth + 1] == 1 && array[depth + 2] != 0 && array[depth + 3] != 4)
                {
                    IncreaseSyuntsu(depth);
                    Run(depth + 2);
                    DecreaseSyuntsu(depth);
                }
                else
                {
                    IncreaseIsolatedTile(depth);
                    Run(depth + 1);
                    DecreaseIsolatedTile(depth);
                    if (i < 7 && array[depth + 2] != 0)
                    {
                        if (array[depth + 1] != 0)
                        {
                            IncreaseSyuntsu(depth);
                            Run(depth + 1);
                            DecreaseSyuntsu(depth);
                        }
                        IncreaseTatsuSecond(depth);
                        Run(depth + 1);
                        DecreaseTatsuSecond(depth);
                    }
                    if (i < 8 && array[depth + 1] != 0)
                    {
                        IncreaseTatsuFirst(depth);
                        Run(depth + 1);
                        DecreaseTatsuFirst(depth);
                    }
                }
            }
        }

        private static void UpdateResult()
        {
            var retShanten = 8 - meldsCount * 2 - tatsuCount - pairsCount;
            var nMentsuKouho = meldsCount + tatsuCount;
            if (pairsCount != 0)
            {
                nMentsuKouho += pairsCount - 1;
            }
            else if (honorBits != 0 && isolatedBits != 0)
            {
                if ((honorBits | isolatedBits) == honorBits)
                {
                    retShanten += 1;
                }
            }
            if (nMentsuKouho > 4)
            {
                retShanten += nMentsuKouho - 4;
            }
            if (retShanten != AGARI_STATE && retShanten < jidahaiCount)
            {
                retShanten = jidahaiCount;
            }
            if (retShanten < shanten)
            {
                shanten = retShanten;
            }
        }

        private static void IncreaseSet(int k)
        {
            array[k] -= 3;
            meldsCount += 1;
        }

        private static void DecreaseSet(int k)
        {
            array[k] += 3;
            meldsCount -= 1;
        }

        private static void IncreasePair(int k)
        {
            array[k] -= 2;
            pairsCount += 1;
        }

        private static void DecreasePair(int k)
        {
            array[k] += 2;
            pairsCount -= 1;
        }

        private static void IncreaseSyuntsu(int k)
        {
            array[k] -= 1;
            array[k + 1] -= 1;
            array[k + 2] -= 1;
            meldsCount += 1;
        }

        private static void DecreaseSyuntsu(int k)
        {
            array[k] += 1;
            array[k + 1] += 1;
            array[k + 2] += 1;
            meldsCount -= 1;
        }

        private static void IncreaseTatsuFirst(int k)
        {
            array[k] -= 1;
            array[k + 1] -= 1;
            tatsuCount += 1;
        }

        private static void DecreaseTatsuFirst(int k)
        {
            array[k] += 1;
            array[k + 1] += 1;
            tatsuCount -= 1;
        }

        private static void IncreaseTatsuSecond(int k)
        {
            array[k] -= 1;
            array[k + 2] -= 1;
            tatsuCount += 1;
        }

        private static void DecreaseTatsuSecond(int k)
        {
            array[k] += 1;
            array[k + 2] += 1;
            tatsuCount -= 1;
        }

        private static void IncreaseIsolatedTile(int k)
        {
            array[k] -= 1;
            isolatedBits |= 1 << k;
        }

        private static void DecreaseIsolatedTile(int k)
        {
            array[k] += 1;
            isolatedBits |= 1 << k;
        }
    }
}