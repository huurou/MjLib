namespace MjLib.HandCalculating.Fus;

internal abstract class Fu : ValueObject<Fu>
{
    public static Base Base { get; }
    public static Menzen Menzen { get; }
    public static Chiitoitsu Chiitoitsu { get; }
    public static OpenPinfuBase OpenPinfuBase { get; }
    public static Tsumo Tsumo { get; }

    // 待ち
    public static Kanchan Kanchan { get; }
    public static Penchan Penchan { get; }
    public static Tanki Tanki { get; }

    // 雀頭
    public static PlayerWindToitsu PlayerWindToitsu { get; }
    public static RoundWindToitsu RoundWindToitsu { get; }
    public static DragonToitsu DragonToitsu { get; }

    // 面子
    public static ChuuchanMinko ChuuchanMinko { get; }
    public static YaochuuMinko YaochuuMinko { get; }
    public static ChuuchanAnko ChuuchanAnko { get; }
    public static YaochuuAnko YaochuuAnko { get; }
    public static ChuuchanMinkan ChuuchanMinkan { get; }
    public static YaochuuMinkan YaochuuMinkan { get; }
    public static ChuuchanAnkan ChuuchanAnkan { get; }
    public static YaochuuAnkan YaochuuAnkan { get; }

    static Fu()
    {
        var id = 0;
        Base = new(id++);
        Base = new(id++);
        Menzen = new(id++);
        Chiitoitsu = new(id++);
        OpenPinfuBase = new(id++);
        Tsumo = new(id++);
        Kanchan = new(id++);
        Penchan = new(id++);
        Tanki = new(id++);
        PlayerWindToitsu = new(id++);
        RoundWindToitsu = new(id++);
        DragonToitsu = new(id++);
        ChuuchanMinko = new(id++);
        YaochuuMinko = new(id++);
        ChuuchanAnko = new(id++);
        YaochuuAnko = new(id++);
        ChuuchanMinkan = new(id++);
        YaochuuMinkan = new(id++);
        ChuuchanAnkan = new(id++);
        YaochuuAnkan = new(id++);
    }

    public int Id { get; }
    public abstract int Value { get; }
    public abstract string Reason { get; }

    protected Fu(int id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"{Reason}:{Value}符";
    }

    #region ValueObject<T>の実装

    public override bool EqualsCore(ValueObject<Fu>? other)
    {
        return other is Fu x && x.Id == Id;
    }

    public override int GetHashCodeCore()
    {
        return new { Id, Reason, Value }.GetHashCode();
    }

    #endregion ValueObject<T>の実装
}

internal class Base : Fu
{
    public Base(int id)
        : base(id) { }

    public override int Value => 20;

    public override string Reason => "副底";
}

internal class Menzen : Fu
{
    public Menzen(int id)
        : base(id) { }

    public override int Value => 10;

    public override string Reason => "面前加符";
}

internal class Chiitoitsu : Fu
{
    public Chiitoitsu(int id)
        : base(id) { }

    public override int Value => 25;

    public override string Reason => "七対子";
}

internal class OpenPinfuBase : Fu
{
    public OpenPinfuBase(int id)
        : base(id) { }

    public override int Value => 30;

    public override string Reason => "副底(食い平和)";
}

internal class Tsumo : Fu
{
    public Tsumo(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "自摸";
}

internal class Kanchan : Fu
{
    public Kanchan(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "嵌張待ち";
}

internal class Penchan : Fu
{
    public Penchan(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "辺張待ち";
}

internal class Tanki : Fu
{
    public Tanki(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "単騎待ち";
}

internal class PlayerWindToitsu : Fu
{
    public PlayerWindToitsu(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "自風雀頭";
}

internal class RoundWindToitsu : Fu
{
    public RoundWindToitsu(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "場風雀頭";
}

internal class DragonToitsu : Fu
{
    public DragonToitsu(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "三元牌雀頭";
}

internal class ChuuchanMinko : Fu
{
    public ChuuchanMinko(int id)
        : base(id) { }

    public override int Value => 2;

    public override string Reason => "中張明刻";
}

internal class YaochuuMinko : Fu
{
    public YaochuuMinko(int id)
        : base(id) { }

    public override int Value => 4;

    public override string Reason => "么九明刻";
}

internal class ChuuchanAnko : Fu
{
    public ChuuchanAnko(int id)
        : base(id) { }

    public override int Value => 4;

    public override string Reason => "中張暗刻";
}

internal class YaochuuAnko : Fu
{
    public YaochuuAnko(int id)
        : base(id) { }

    public override int Value => 8;

    public override string Reason => "么九暗刻";
}

internal class ChuuchanMinkan : Fu
{
    public ChuuchanMinkan(int id)
        : base(id) { }

    public override int Value => 8;

    public override string Reason => "中張明槓";
}

internal class YaochuuMinkan : Fu
{
    public YaochuuMinkan(int id)
        : base(id) { }

    public override int Value => 16;

    public override string Reason => "么九明槓";
}

internal class ChuuchanAnkan : Fu
{
    public ChuuchanAnkan(int id)
        : base(id) { }

    public override int Value => 16;

    public override string Reason => "中張暗槓";
}

internal class YaochuuAnkan : Fu
{
    public YaochuuAnkan(int id)
        : base(id) { }

    public override int Value => 32;

    public override string Reason => "么九暗槓";
}