using System;

namespace mjlib.HandCalculating
{
    /// <summary>
    /// Main: ツモ: 親の点数 ロン:手の点数
    /// Additional: ツモ: 子の点数 ロン:なし
    /// 満貫ツモアガリ->4000オール or (4000, 2000)
    /// 満貫出アガリ->12000 or 8000
    /// </summary>
    public class Cost
    {
        public int Main { get; }
        public int Additional { get; }

        public Cost(int main, int additional)
        {
            Main = main;
            Additional = additional;
        }
    }

    internal static class ScoresCalcurator
    {
        public static Cost CalculateScores(int han, int fu, HandConfig config, bool isYakuman = false)
        {
            //数え役満
            if (han >= 13 && !isYakuman)
            {
                if (config.Options.KazoeLimit == Kazoe.Limited)
                {
                    han = 13;
                }
                else if (config.Options.KazoeLimit == Kazoe.Sanbaiman)
                {
                    han = 12;
                }
            }

            //rounded: 一家あたりの点数 跳満(子)-12000なら3000点
            int rounded, doubleRounded, fourRounded, sixRounded;
            if (han >= 5)
            {
                rounded = han switch
                {
                    >= 78 => 48000,
                    >= 65 => 40000,
                    >= 52 => 32000,
                    >= 39 => 24000,
                    >= 26 => 16000,
                    >= 13 => 8000,
                    >= 11 => 6000,
                    >= 8 => 4000,
                    >= 6 => 3000,
                    _ => 2000,
                };
                doubleRounded = rounded * 2;
                fourRounded = doubleRounded * 2;
                sixRounded = doubleRounded * 3;
            }
            else
            {
                var basePoint = fu * Math.Pow(2, 2 + han);
                rounded = (int)((basePoint + 99) / 100) * 100;
                doubleRounded = (int)((2 * basePoint + 99) / 100) * 100;
                fourRounded = (int)((4 * basePoint + 99) / 100) * 100;
                sixRounded = (int)((6 * basePoint + 99) / 100) * 100;

                var IsKiriage = false;
                if (config.Options.Kiriage)
                {
                    if (han == 4 && fu == 30)
                    {
                        IsKiriage = true;
                    }
                    if (han == 3 && fu == 60)
                    {
                        IsKiriage = true;
                    }
                }

                //満貫
                if (rounded > 2000 || IsKiriage)
                {
                    rounded = 2000;
                    doubleRounded = rounded * 2;
                    fourRounded = doubleRounded * 2;
                    sixRounded = doubleRounded * 3;
                }
            }

            return config.IsTsumo
                ? new Cost(doubleRounded, config.IsDealer ? doubleRounded : rounded)
                : new Cost(config.IsDealer ? sixRounded : fourRounded, 0);
        }
    }
}