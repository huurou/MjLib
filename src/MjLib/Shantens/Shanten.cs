using MjLib.TileCountArrays;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Shantens;

internal static class Shanten
{
    public const int AGARI_STATE = -1;

    private static TileCountArray countArray_ = new();
    private static int tripletsCount_;
    private static int tatsuCount_;
    private static int pairsCount_;
    private static int jidahaiCount_;
    private static readonly bool[] numberCharacters_ = new bool[ID_MAX + 1];
    private static readonly bool[] isolations_ = new bool[ID_MAX + 1];
    private static int minShanten_;

    /// <summary>
    /// シャンテン数を計算する 0:テンパイ -1:あがり
    /// </summary>
    /// <param name="pureHand">純手牌 鳴かれた牌を含まない手牌</param>
    /// <returns>シャンテン数</returns>
    public static int Calculate(TileKindList pureHand, bool useChiitoitsu = true, bool useKokushi = true)
    {
        var shantens = new List<int> { CalculateForRegular(pureHand) };
        if (useChiitoitsu) shantens.Add(CalculateForChiitoitsu(pureHand));
        if (useKokushi) shantens.Add(CalculateForKokushi(pureHand));
        return shantens.Min();
    }

    public static int CalculateForRegular(TileKindList pureHand)
    {
        Init(pureHand);
        RemoveCharacterTiles();
        Scan();
        Run(0);
        return minShanten_;
    }

    public static int CalculateForChiitoitsu(TileKindList pureHand)
    {
        Init(pureHand);
        // 対子の数
        var pair = countArray_.Count(x => x >= 2);
        // 牌種類の数
        var count = countArray_.Count(x => x != 0);
        // 6-対子 7種類の牌が必要なので補正する
        return 6 - pair + Math.Max(0, 7 - count);
    }

    public static int CalculateForKokushi(TileKindList pureHand)
    {
        Init(pureHand);
        // 么九牌の対子の数
        var yaochuPair = AllKind.Where(x=>x.IsYaochu).Count(x => countArray_[x] >= 2);
        // 么九牌の種類数
        var yaochu = AllKind.Where(x => x.IsYaochu).Count(x => countArray_[x] != 0);
        // 13-么九牌 么九牌の対子があればシャンテン数一つ減る
        return 13 - yaochu - (yaochuPair != 0 ? 1 : 0);
    }

    private static void Init(TileKindList pureHand)
    {
        if (pureHand.Count > 14) throw new ArgumentException("手牌の数が14個より多いです。", nameof(pureHand));
        countArray_ = pureHand.ToTileCountArray();
        tripletsCount_ = 0;
        tatsuCount_ = 0;
        pairsCount_ = 0;
        jidahaiCount_ = 0;
        Array.Clear(numberCharacters_);
        Array.Clear(isolations_);
        minShanten_ = 8;
    }

    private static void RemoveCharacterTiles()
    {
        tripletsCount_ = 0;
        jidahaiCount_ = 0;
        pairsCount_ = 0;
        for (var id = Ton.Id; id <= Chun.Id; id++)
        {
            switch (countArray_[id])
            {
                case 1:
                    isolations_[id] = true;
                    break;

                case 2:
                    pairsCount_++;
                    break;

                case 3:
                    tripletsCount_++;
                    break;

                case 4:
                    tripletsCount_++;
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
            numberCharacters_[i] = countArray_[i] == 4;
        }
        tripletsCount_ += (14 - countArray_.Sum()) / 3;
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
            IncreasePair(id);
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
            DecreasePair(id);
        }
        if (countArray_[id] == 3)
        {
            IncreaseSet(id);
            Run(id);
            DecreaseSet(id);
            IncreasePair(id);
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
            DecreasePair(id);
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
            IncreasePair(id);
            Run(id);
            DecreasePair(id);
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
        var shanten = 8 - tripletsCount_ * 2 - tatsuCount_ - pairsCount_;
        var mentsuKouho = tripletsCount_ + tatsuCount_;
        if (pairsCount_ != 0)
        {
            mentsuKouho += pairsCount_ - 1;
        }
        // 同種の数牌を4枚持っているときに刻子&単騎待ちとみなされないよう修正
        else if (numberCharacters_.Any(x => x) && isolations_.Any(x => x) &&
            isolations_.Select((x, i) => (x, i)).Where(x => x.x).All(x => numberCharacters_[x.i]))
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
        tripletsCount_++;
    }

    private static void DecreaseSet(int id)
    {
        countArray_[id] += 3;
        tripletsCount_--;
    }

    private static void IncreasePair(int id)
    {
        countArray_[id] -= 2;
        pairsCount_++;
    }

    private static void DecreasePair(int id)
    {
        countArray_[id] += 2;
        pairsCount_--;
    }

    private static void IncreaseShuntsu(int id)
    {
        countArray_[id]--;
        countArray_[id + 1]--;
        countArray_[id + 2]--;
        tripletsCount_++;
    }

    private static void DecreaseShuntsu(int id)
    {
        countArray_[id]++;
        countArray_[id + 1]++;
        countArray_[id + 2]++;
        tripletsCount_--;
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