namespace MjLib.HandCalculating.Scores;

internal static class ScoreCalcurator
{
    public static Score Calculate(int fu, int han, HandConfig config, bool isYakuman = false)
    {
        var _han = han;
        // 数え役満
        if (_han >= 13 && !isYakuman)
        {
            if (config.Rurles.KazoeLimit == Kazoe.Limited)
            {
                _han = 13;
            }
            else if (config.Rurles.KazoeLimit == Kazoe.Sanbaiman)
            {
                _han = 12;
            }
        }
        // rounded: 一家あたりの点数 跳満(子)-12000なら3000点
        int rounded, doubleRounded, fourRounded, sixRounded;
        if (_han is >= 1 and <= 4)
        {
            var basePoint = fu * Math.Pow(2, 2 + _han);
            rounded = (int)((basePoint + 99) / 100) * 100;
            doubleRounded = (int)((2 * basePoint + 99) / 100) * 100;
            fourRounded = (int)((4 * basePoint + 99) / 100) * 100;
            sixRounded = (int)((6 * basePoint + 99) / 100) * 100;

            var IsKiriage = false;
            if (config.Rurles.Kiriage)
            {
                if (_han == 4 && fu == 30)
                {
                    IsKiriage = true;
                }
                if (_han == 3 && fu == 60)
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
        else
        {
            rounded = _han switch
            {
                5 => 2000,
                6 or 7 => 3000,
                8 or 9 or 10 => 4000,
                11 or 12 => 6000,
                >= 13 and < 26 => 8000,
                >= 26 and < 39 => 16000,
                >= 39 and < 52 => 24000,
                >= 52 and < 65 => 32000,
                >= 65 and < 78 => 40000,
                >= 78 => 48000,
                _ => throw new ArgumentException("翻に0以下を指定することはできません。", nameof(_han)),
            };
            doubleRounded = rounded * 2;
            fourRounded = doubleRounded * 2;
            sixRounded = doubleRounded * 3;
        }

        return new()
        {
            Main = config.IsTsumo ? doubleRounded : config.IsDealer ? sixRounded : fourRounded,
            Sub = config.IsTsumo ? config.IsDealer ? doubleRounded : rounded : 0,
        };
    }
}