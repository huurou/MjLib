namespace MjLib.Fus;

internal abstract class Fu
{
    public static Base Base { get; } = new();
    public static OpenChuchanPon OpenChuchanPon { get; } = new();
    public static OpenYaochuPon OpenYaochuPon { get; } = new();
    public static ClosedChuchanPon ClosedChuchanPon { get; } = new();
    public static ClosedYaochuPon ClosedYaochuPon { get; } = new();
    public static OpenChuchanKan OpenChuchanKan { get; } = new();
    public static OpenYaochuKan OpenYaochuKan { get; } = new();
    public static ClosedChuchanKan ClosedChuchanKan { get; } = new();
    public static ClosedYaochuKan ClosedYaochuKan { get; } = new();
    public static PlaceWindPair PlaceWindPair { get; } = new();
    public static RoundWindPair RoundWindPair { get; } = new();
    public static DragonPair DragonPair { get; } = new();
    public static Kanchan Kanchan { get; } = new();
    public static Penchan Penchan { get; } = new();
    public static Tanki Tanki { get; } = new();
    public static Menzen Menzen { get; } = new();
    public static Tsumo Tsumo { get; } = new();
    public static TsumoPin TsumoPin { get; } = new();
    public static Chiitoitsu Chiitoitsu { get; } = new();
    public static OpenPinfu OpenPinfu { get; } = new();

    public abstract int Value { get; }
    public abstract string Reason { get; }
}

internal class Base : Fu
{
    public override int Value => 20;

    public override string Reason => "副底";
}

internal class OpenChuchanPon : Fu
{
    public override int Value => 2;

    public override string Reason => "面子::中張明刻";
}

internal class OpenYaochuPon : Fu
{
    public override int Value => 4;

    public override string Reason => "面子::么九明刻";
}

internal class ClosedChuchanPon : Fu
{
    public override int Value => 4;

    public override string Reason => "面子::中張暗刻";
}

internal class ClosedYaochuPon : Fu
{
    public override int Value => 8;

    public override string Reason => "面子::么九暗刻";
}

internal class OpenChuchanKan : Fu
{
    public override int Value => 8;

    public override string Reason => "面子::中張明槓";
}

internal class OpenYaochuKan : Fu
{
    public override int Value => 16;

    public override string Reason => "面子::么九明槓";
}

internal class ClosedChuchanKan : Fu
{
    public override int Value => 16;

    public override string Reason => "面子::中張暗槓";
}

internal class ClosedYaochuKan : Fu
{
    public override int Value => 32;

    public override string Reason => "面子::么九暗槓";
}

internal class PlaceWindPair : Fu
{
    public override int Value => 2;

    public override string Reason => "雀頭::自風";
}

internal class RoundWindPair : Fu
{
    public override int Value => 2;

    public override string Reason => "雀頭::場風";
}

internal class DragonPair : Fu
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

internal class TsumoPin : Fu
{
    public override int Value => 20;

    public override string Reason => "自摸平和";
}

internal class Chiitoitsu : Fu
{
    public override int Value => 25;

    public override string Reason => "七対子";
}

internal class OpenPinfu : Fu
{
    public override int Value => 30;

    public override string Reason => "食い平和";
}