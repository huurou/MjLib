﻿using MjLib.TileCountArrays;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Shantens;

internal static class Shanten
{
    public const int AGARI_STATE = -1;

    private static TileCountArray countArray_ = new();
    private static int mentsuCount_;
    private static int tatsuCount_;
    private static int toitsusCount_;
    private static int jidahaiCount_;
    private static readonly bool[] suits_ = new bool[KIND_COUNT];
    private static readonly bool[] isolations_ = new bool[KIND_COUNT];
    private static int minShanten_;

    /// <summary>
    /// シャンテン数を計算する 0:テンパイ -1:あがり
    /// </summary>
    /// <param name="hand">手牌 鳴かれた牌を含まない手牌</param>
    /// <returns>シャンテン数</returns>
    public static int Calculate(TileKindList hand, bool useChiitoitsu = true, bool useKokushi = true)
    {
        var shantens = new List<int> { CalculateForRegular(hand) };
        if (useChiitoitsu) shantens.Add(CalculateForChiitoitsu(hand));
        if (useKokushi) shantens.Add(CalculateForKokushi(hand));
        return shantens.Min();
    }

    public static int CalculateForRegular(TileKindList hand)
    {
        Init(hand);
        RemoveCharacterTiles();
        Scan();
        Run(0);
        return minShanten_;
    }

    public static int CalculateForChiitoitsu(TileKindList hand)
    {
        Init(hand);
        // 対子の数
        var toitsu = countArray_.Count(x => x >= 2);
        // 牌種類の数
        var count = countArray_.Count(x => x != 0);
        // 6-対子 7種類の牌が必要なので補正する
        return 6 - toitsu + Math.Max(0, 7 - count);
    }

    public static int CalculateForKokushi(TileKindList hand)
    {
        Init(hand);
        // 么九牌の対子の数
        var yaochuToitsu = AllKind.Where(x => x.IsYaochu).Count(x => countArray_[x] >= 2);
        // 么九牌の種類数
        var yaochu = AllKind.Where(x => x.IsYaochu).Count(x => countArray_[x] != 0);
        // 13-么九牌 么九牌の対子があればシャンテン数一つ減る
        return 13 - yaochu - (yaochuToitsu != 0 ? 1 : 0);
    }

    private static void Init(TileKindList hand)
    {
        if (hand.Count > 14) throw new ArgumentException($"手牌の数が14個より多いです。given:{hand}", nameof(hand));
        countArray_ = hand.ToTileCountArray();
        mentsuCount_ = 0;
        tatsuCount_ = 0;
        toitsusCount_ = 0;
        jidahaiCount_ = 0;
        Array.Clear(suits_);
        Array.Clear(isolations_);
        minShanten_ = 8;
    }

    private static void RemoveCharacterTiles()
    {
        mentsuCount_ = 0;
        jidahaiCount_ = 0;
        toitsusCount_ = 0;
        for (var id = Ton.Id; id <= Chun.Id; id++)
        {
            switch (countArray_[id])
            {
                case 1:
                    isolations_[id] = true;
                    break;

                case 2:
                    toitsusCount_++;
                    break;

                case 3:
                    mentsuCount_++;
                    break;

                case 4:
                    mentsuCount_++;
                    jidahaiCount_++;
                    isolations_[id] = true;
                    break;

                default: break;
            }
        }
        if (jidahaiCount_ != 0 && (countArray_.Sum() % 3) == 2)
        {
            jidahaiCount_--;
        }
    }

    private static void Scan()
    {
        for (var i = 0; i <= Sou9.Id; i++)
        {
            suits_[i] = countArray_[i] == 4;
        }
        mentsuCount_ += (14 - countArray_.Sum()) / 3;
    }

    private static void Run(int id)
    {
        if (minShanten_ == AGARI_STATE) return;
        // 牌が存在するインデックスまで移動する
        while (countArray_[id] == 0)
        {
            id++;
            if (id >= 27) break;
        }
        if (id >= 27)
        {
            UpdateResult();
            return;
        }
        var i = id;
        if (i > 8) i -= 9;
        if (i > 8) i -= 9;
        if (countArray_[id] == 4)
        {
            IncreaseSet(id);
            if (i < 7 && countArray_[id + 2] != 0)
            {
                if (countArray_[id + 1] != 0)
                {
                    IncreaseShuntsu(id);
                    Run(id);
                    DecreaseShuntsu(id);
                }
                IncreaseTatsu2(id);
                Run(id);
                DecreaseTatsu2(id);
            }
            if (i < 8 && countArray_[id + 1] != 0)
            {
                IncreaseTatsu1(id);
                Run(id);
                DecreaseTatsu1(id);
            }
            IncreaseIsolation(id);
            Run(id);
            DecreaseIsolation(id);
            DecreaseSet(id);
            IncreaseToitsu(id);
            if (i < 7 && countArray_[id + 2] != 0)
            {
                if (countArray_[id + 1] != 0)
                {
                    IncreaseShuntsu(id);
                    Run(id);
                    DecreaseShuntsu(id);
                }
                IncreaseTatsu2(id);
                Run(id);
                DecreaseTatsu2(id);
            }
            if (i < 8 && countArray_[id + 1] != 0)
            {
                IncreaseTatsu1(id);
                Run(id);
                DecreaseTatsu1(id);
            }
            DecreaseToitsu(id);
        }
        if (countArray_[id] == 3)
        {
            IncreaseSet(id);
            Run(id);
            DecreaseSet(id);
            IncreaseToitsu(id);
            if (i < 7 && countArray_[id + 1] != 0 && countArray_[id + 2] != 0)
            {
                IncreaseShuntsu(id);
                Run(id);
                DecreaseShuntsu(id);
            }
            else
            {
                if (i < 7 && countArray_[id + 2] != 0)
                {
                    IncreaseTatsu2(id);
                    Run(id);
                    DecreaseTatsu2(id);
                }
                if (i < 8 && countArray_[id + 1] != 0)
                {
                    IncreaseTatsu1(id);
                    Run(id);
                    DecreaseTatsu1(id);
                }
            }
            DecreaseToitsu(id);
            if (i < 7 && countArray_[id + 2] >= 2 && countArray_[id + 1] >= 2)
            {
                IncreaseShuntsu(id);
                IncreaseShuntsu(id);
                Run(id);
                DecreaseShuntsu(id);
                DecreaseShuntsu(id);
            }
        }
        if (countArray_[id] == 2)
        {
            IncreaseToitsu(id);
            Run(id);
            DecreaseToitsu(id);
            if (i < 7 && countArray_[id + 1] != 0 && countArray_[id + 2] != 0)
            {
                IncreaseShuntsu(id);
                Run(id);
                DecreaseShuntsu(id);
            }
        }
        if (countArray_[id] == 1)
        {
            if (i < 6 && countArray_[id + 1] == 1 && countArray_[id + 2] != 0 && countArray_[id + 3] != 4)
            {
                IncreaseShuntsu(id);
                Run(id);
                DecreaseShuntsu(id);
            }
            else
            {
                IncreaseIsolation(id);
                Run(id);
                DecreaseIsolation(id);
                if (i < 7 && countArray_[id + 2] != 0)
                {
                    if (countArray_[id + 1] != 0)
                    {
                        IncreaseShuntsu(id);
                        Run(id);
                        DecreaseShuntsu(id);
                    }
                    IncreaseTatsu2(id);
                    Run(id);
                    DecreaseTatsu2(id);
                }
                if (i < 8 && countArray_[id + 1] != 0)
                {
                    IncreaseTatsu1(id);
                    Run(id);
                    DecreaseTatsu1(id);
                }
            }
        }
    }

    private static void UpdateResult()
    {
        var shanten = 8 - mentsuCount_ * 2 - tatsuCount_ - toitsusCount_;
        var mentsuKouho = mentsuCount_ + tatsuCount_;
        if (toitsusCount_ != 0)
        {
            mentsuKouho += toitsusCount_ - 1;
        }
        // 同種の数牌を4枚持っているときに刻子&単騎待ちとみなされないよう修正
        else if (suits_.Any(x => x) && isolations_.Any(x => x) &&
            isolations_.Select((x, i) => (x, i)).Where(x => x.x).All(x => suits_[x.i]))
        {
            shanten++;
        }
        if (mentsuKouho > 4)
        {
            shanten += mentsuKouho - 4;
        }
        if (shanten != AGARI_STATE && shanten < jidahaiCount_)
        {
            shanten = jidahaiCount_;
        }
        if (shanten < minShanten_)
        {
            minShanten_ = shanten;
        }
    }

    private static void IncreaseSet(int id)
    {
        countArray_[id] -= 3;
        mentsuCount_++;
    }

    private static void DecreaseSet(int id)
    {
        countArray_[id] += 3;
        mentsuCount_--;
    }

    private static void IncreaseToitsu(int id)
    {
        countArray_[id] -= 2;
        toitsusCount_++;
    }

    private static void DecreaseToitsu(int id)
    {
        countArray_[id] += 2;
        toitsusCount_--;
    }

    private static void IncreaseShuntsu(int id)
    {
        countArray_[id]--;
        countArray_[id + 1]--;
        countArray_[id + 2]--;
        mentsuCount_++;
    }

    private static void DecreaseShuntsu(int id)
    {
        countArray_[id]++;
        countArray_[id + 1]++;
        countArray_[id + 2]++;
        mentsuCount_--;
    }

    private static void IncreaseTatsu1(int id)
    {
        countArray_[id]--;
        countArray_[id + 1]--;
        tatsuCount_++;
    }

    private static void DecreaseTatsu1(int id)
    {
        countArray_[id]++;
        countArray_[id + 1]++;
        tatsuCount_--;
    }

    private static void IncreaseTatsu2(int id)
    {
        countArray_[id]--;
        countArray_[id + 2]--;
        tatsuCount_++;
    }

    private static void DecreaseTatsu2(int id)
    {
        countArray_[id]++;
        countArray_[id + 2]++;
        tatsuCount_--;
    }

    private static void IncreaseIsolation(int id)
    {
        countArray_[id]--;
        isolations_[id] = true;
    }

    private static void DecreaseIsolation(int id)
    {
        countArray_[id]++;
        isolations_[id] = false;
    }
}