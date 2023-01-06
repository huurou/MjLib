using System;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    /// <summary>
    /// 牌一枚ずつに番号をつけたもの 0～135
    /// </summary>
    public class Tile : IEquatable<Tile>
    {
        public int Value { get; }

        public Tile(int value)
        {
            Value = value;
        }

        public TileKind ToTileKind()
        {
            return new(Value / 4);
        }

        public string ToOneLineString(bool printAkaDora = false)
        {
            if (Value < 36)
            {
                return printAkaDora && Value == FIVE_RED_MAN ? "0m" : $"{Value / 4 + 1}m";
            }
            else if (Value is >= 36 and < 72)
            {
                return printAkaDora && Value == FIVE_RED_PIN ? "0p" : $"{(Value - 36) / 4 + 1}p";
            }
            else if (Value is >= 72 and < 108)
            {
                return printAkaDora && Value == FIVE_RED_SOU ? "0s" : $"{(Value - 72) / 4 + 1}s";
            }
            return $"{(Value - 108) / 4 + 1}z";
        }

        public static Tile Parse(string man = "", string pin = "", string sou = "",
            string honors = "", bool hasAkaDora = false)
        {
            return TileList.Parse(man: man, pin: pin, sou: sou, honor: honors, hasAkaDora: hasAkaDora)[0];
        }

        public override bool Equals(object? obj)
        {
            return obj is Tile other && Equals(other);
        }

        public bool Equals(Tile? other)
        {
            return other is not null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}