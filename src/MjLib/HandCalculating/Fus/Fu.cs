namespace MjLib.HandCalculating.Fus;

internal abstract class Fu
{
    public static Base Base { get; } = new();
    public static ChuchanMinko ChuchanMinko { get; } = new();
    public static YaochuMinko YaochuMinko { get; } = new();
    public static ChuchanAnko ChuchanAnko { get; } = new();
    public static YaochuAnko YaochuAnko { get; } = new();
    public static ChuchanMinkan ChuchanMinkan { get; } = new();
    public static YaochuMinkan YaochuMinkan { get; } = new();
    public static ChuchanAnkan ChuchanAnkan { get; } = new();
    public static YaochuAnkan YaochuAnkan { get; } = new();
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
}

internal class Base : Fu
{
    public override int Value => 20;

    public override string Reason => "副底";
}

internal class ChuchanMinko : Fu
{
    public override int Value => 2;

    public override string Reason => "面子::中張明刻";
}

internal class YaochuMinko : Fu
{
    public override int Value => 4;

    public override string Reason => "面子::么九明刻";
}

internal class ChuchanAnko : Fu
{
    public override int Value => 4;

    public override string Reason => "面子::中張暗刻";
}

internal class YaochuAnko : Fu
{
    public override int Value => 8;

    public override string Reason => "面子::么九暗刻";
}

internal class ChuchanMinkan : Fu
{
    public override int Value => 8;

    public override string Reason => "面子::中張明槓";
}

internal class YaochuMinkan : Fu
{
    public override int Value => 16;

    public override string Reason => "面子::么九明槓";
}

internal class ChuchanAnkan : Fu
{
    public override int Value => 16;

    public override string Reason => "面子::中張暗槓";
}

internal class YaochuAnkan : Fu
{
    public override int Value => 32;

    public override string Reason => "面子::么九暗槓";
}

internal class PlayerWindToitsu : Fu
{
    public override int Value => 2;

    public override string Reason => "雀頭::自風";
}

internal class RoundWindToitsu : Fu
{
    public override int Value => 2;

    public override string Reason => "雀頭::場風";
}

internal class DragonToitsu : Fu
{
    public override int Value => 2;

    public override string Reason => "雀頭::三元牌";
}

internal class Kanchan : Fu
{
    public override int Value => 2;

    public override string Reason => "待ち::嵌張";
}

internal class Penchan : Fu
{
    public override int Value => 2;

    public override string Reason => "待ち::辺張";
}

internal class Tanki : Fu
{
    public override int Value => 2;

    public override string Reason => "待ち::単騎";
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