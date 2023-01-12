namespace MjLib.HandCalculating.Fus;

internal abstract class Fu : ValueObject<Fu>
{
    public static Base Base { get; } = new();
    public static ChuuchanMinko ChuuchanMinko { get; } = new();
    public static YaochuuMinko YaochuuMinko { get; } = new();
    public static ChuuchanAnko ChuuchanAnko { get; } = new();
    public static YaochuuAnko YaochuuAnko { get; } = new();
    public static ChuuchanMinkan ChuuchanMinkan { get; } = new();
    public static YaochuuMinkan YaochuuMinkan { get; } = new();
    public static ChuuchanAnkan ChuuchanAnkan { get; } = new();
    public static YaochuuAnkan YaochuuAnkan { get; } = new();
    public static PlayerWindToitsu PlayerWindToitsu { get; } = new();
    public static RoundWindToitsu RoundWindToitsu { get; } = new();
    public static DragonToitsu DragonToitsu { get; } = new();
    public static Kanchan Kanchan { get; } = new();
    public static Penchan Penchan { get; } = new();
    public static Tanki Tanki { get; } = new();
    public static Menzen Menzen { get; } = new();
    public static Tsumo Tsumo { get; } = new();
    public static Chiitoitsu Chiitoitsu { get; } = new();
    public static OpenPinfuBase OpenPinfuBase { get; } = new();

    public abstract int Value { get; }
    public abstract string Reason { get; }

    public override string ToString()
    {
        return $"{Reason}:{Value}符";
    }

    #region ValueObject<T>の実装

    public override bool EqualsCore(ValueObject<Fu>? other)
    {
        return other is Fu x && x.Reason == Reason;
    }

    public override int GetHashCodeCore()
    {
        return new { Reason, Value }.GetHashCode();
    }

    #endregion ValueObject<T>の実装
}

internal class Base : Fu
{
    public override int Value => 20;

    public override string Reason => "副底";
}

internal class ChuuchanMinko : Fu
{
    public override int Value => 2;

    public override string Reason => "中張明刻";
}

internal class YaochuuMinko : Fu
{
    public override int Value => 4;

    public override string Reason => "么九明刻";
}

internal class ChuuchanAnko : Fu
{
    public override int Value => 4;

    public override string Reason => "中張暗刻";
}

internal class YaochuuAnko : Fu
{
    public override int Value => 8;

    public override string Reason => "么九暗刻";
}

internal class ChuuchanMinkan : Fu
{
    public override int Value => 8;

    public override string Reason => "中張明槓";
}

internal class YaochuuMinkan : Fu
{
    public override int Value => 16;

    public override string Reason => "么九明槓";
}

internal class ChuuchanAnkan : Fu
{
    public override int Value => 16;

    public override string Reason => "中張暗槓";
}

internal class YaochuuAnkan : Fu
{
    public override int Value => 32;

    public override string Reason => "么九暗槓";
}

internal class PlayerWindToitsu : Fu
{
    public override int Value => 2;

    public override string Reason => "自風雀頭";
}

internal class RoundWindToitsu : Fu
{
    public override int Value => 2;

    public override string Reason => "場風雀頭";
}

internal class DragonToitsu : Fu
{
    public override int Value => 2;

    public override string Reason => "三元牌雀頭";
}

internal class Kanchan : Fu
{
    public override int Value => 2;

    public override string Reason => "嵌張待ち";
}

internal class Penchan : Fu
{
    public override int Value => 2;

    public override string Reason => "辺張待ち";
}

internal class Tanki : Fu
{
    public override int Value => 2;

    public override string Reason => "単騎待ち";
}

internal class Menzen : Fu
{
    public override int Value => 10;

    public override string Reason => "面前加符";
}

internal class Tsumo : Fu
{
    public override int Value => 2;

    public override string Reason => "自摸";
}

internal class Chiitoitsu : Fu
{
    public override int Value => 25;

    public override string Reason => "七対子";
}

internal class OpenPinfuBase : Fu
{
    public override int Value => 30;

    public override string Reason => "副底(食い平和)";
}