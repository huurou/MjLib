using System;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    /// <summary>
    /// 牌の種類ごとに番号を付けたもの 0～33
    /// </summary>
    public class TileKind : IEquatable<TileKind>, IComparable<TileKind>
    {
        public int Value { get; }

        public bool IsMan => Value <= 8;
        public bool IsPin => Value is > 8 and <= 17;
        public bool IsSou => Value is > 17 and <= 26;
        public bool IsHonor => Value >= 27;
        public bool IsTerminal => TERMINAL_INDICES.Contains(Value);
        public bool IsYaochu => IsHonor || IsTerminal;
        public bool IsChuchan => !IsYaochu;

        public int Simplify => Value - 9 * (Value / 9);

        public TileKind(int value) => Value = value;

        public override bool Equals(object? obj) =>  obj is TileKind other && Equals(other);

        public bool Equals(TileKind? other) => other is not null && Value.Equals(other.Value);

        public override int GetHashCode() => base.GetHashCode();

        public int CompareTo(TileKind? other) => other is null ? 1 : Value > other.Value ? 1 : Value < other.Value ? -1 : 0;
    }
}