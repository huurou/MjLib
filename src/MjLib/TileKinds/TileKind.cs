namespace MjLib.TileKinds;

/// <summary>
/// 牌種別
/// </summary>
internal class TileKind : ValueObject<TileKind>, IComparable<TileKind>
{
    public static TileKind Man1 { get; } = new(MAN1);
    public static TileKind Man2 { get; } = new(MAN2);
    public static TileKind Man3 { get; } = new(MAN3);
    public static TileKind Man4 { get; } = new(MAN4);
    public static TileKind Man5 { get; } = new(MAN5);
    public static TileKind Man6 { get; } = new(MAN6);
    public static TileKind Man7 { get; } = new(MAN7);
    public static TileKind Man8 { get; } = new(MAN8);
    public static TileKind Man9 { get; } = new(MAN9);
    public static TileKind Pin1 { get; } = new(PIN1);
    public static TileKind Pin2 { get; } = new(PIN2);
    public static TileKind Pin3 { get; } = new(PIN3);
    public static TileKind Pin4 { get; } = new(PIN4);
    public static TileKind Pin5 { get; } = new(PIN5);
    public static TileKind Pin6 { get; } = new(PIN6);
    public static TileKind Pin7 { get; } = new(PIN7);
    public static TileKind Pin8 { get; } = new(PIN8);
    public static TileKind Pin9 { get; } = new(PIN9);
    public static TileKind Sou1 { get; } = new(SOU1);
    public static TileKind Sou2 { get; } = new(SOU2);
    public static TileKind Sou3 { get; } = new(SOU3);
    public static TileKind Sou4 { get; } = new(SOU4);
    public static TileKind Sou5 { get; } = new(SOU5);
    public static TileKind Sou6 { get; } = new(SOU6);
    public static TileKind Sou7 { get; } = new(SOU7);
    public static TileKind Sou8 { get; } = new(SOU8);
    public static TileKind Sou9 { get; } = new(SOU9);
    public static TileKind Ton { get; } = new(TON);
    public static TileKind Nan { get; } = new(NAN);
    public static TileKind Sha { get; } = new(SHA);
    public static TileKind Pei { get; } = new(PEI);
    public static TileKind Haku { get; } = new(HAKU);
    public static TileKind Hatsu { get; } = new(HATSU);
    public static TileKind Chun { get; } = new(CHUN);

    public const int ID_MIN = 0;
    public const int ID_MAX = 33;
    public const int KIND_COUNT = 34;

    private const int MAN1 = 0;
    private const int MAN2 = 1;
    private const int MAN3 = 2;
    private const int MAN4 = 3;
    private const int MAN5 = 4;
    private const int MAN6 = 5;
    private const int MAN7 = 6;
    private const int MAN8 = 7;
    private const int MAN9 = 8;
    private const int PIN1 = 9;
    private const int PIN2 = 10;
    private const int PIN3 = 11;
    private const int PIN4 = 12;
    private const int PIN5 = 13;
    private const int PIN6 = 14;
    private const int PIN7 = 15;
    private const int PIN8 = 16;
    private const int PIN9 = 17;
    private const int SOU1 = 18;
    private const int SOU2 = 19;
    private const int SOU3 = 20;
    private const int SOU4 = 21;
    private const int SOU5 = 22;
    private const int SOU6 = 23;
    private const int SOU7 = 24;
    private const int SOU8 = 25;
    private const int SOU9 = 26;
    private const int TON = 27;
    private const int NAN = 28;
    private const int SHA = 29;
    private const int PEI = 30;
    private const int HAKU = 31;
    private const int HATSU = 32;
    private const int CHUN = 33;

    /// <summary>
    /// 牌種別ID 0～33
    /// </summary>
    public int Id { get; }
    /// <summary>
    /// 牌に書かれている番号 萬子・筒子・索子は1～9 字牌は東南西北白發中の順に1～7
    /// </summary>
    public int Number => IsMan ? Id + 1 : IsPin ? Id - 8 : IsSou ? Id - 17 : IsHonor ? Id - 26 : throw new InvalidOperationException();
    public bool IsMan => Id is MAN1 or MAN2 or MAN3 or MAN4 or MAN5 or MAN6 or MAN7 or MAN8 or MAN9;
    public bool IsPin => Id is PIN1 or PIN2 or PIN3 or PIN4 or PIN5 or PIN6 or PIN7 or PIN8 or PIN9;
    public bool IsSou => Id is SOU1 or SOU2 or SOU3 or SOU4 or SOU5 or SOU6 or SOU7 or SOU8 or SOU9;
    public bool IsWind => Id is TON or NAN or SHA or PEI;
    public bool IsDragon => Id is HAKU or HATSU or CHUN;
    public bool IsHonor => IsWind || IsDragon;
    public bool IsChuuchan => !IsYaochuu;
    public bool IsYaochuu => IsRoutou || IsHonor;
    public bool IsRoutou => Id is MAN1 or MAN9 or PIN1 or PIN9 or SOU1 or SOU9;
    public static IEnumerable<TileKind> AllKind => Enumerable.Range(ID_MIN, KIND_COUNT).Select(x => new TileKind(x));

    public TileKind(int id)
    {
        Id = ID_MIN <= id && id <= ID_MAX
            ? id
            : throw new ArgumentException($"牌種別IDは{ID_MIN}～{ID_MAX}です。given:{id}", nameof(id));
    }

    public override string ToString()
    {
        return Id switch
        {
            MAN1 => "一",
            MAN2 => "二",
            MAN3 => "三",
            MAN4 => "四",
            MAN5 => "五",
            MAN6 => "六",
            MAN7 => "七",
            MAN8 => "八",
            MAN9 => "九",
            PIN1 => "(1)",
            PIN2 => "(2)",
            PIN3 => "(3)",
            PIN4 => "(4)",
            PIN5 => "(5)",
            PIN6 => "(6)",
            PIN7 => "(7)",
            PIN8 => "(8)",
            PIN9 => "(9)",
            SOU1 => "1",
            SOU2 => "2",
            SOU3 => "3",
            SOU4 => "4",
            SOU5 => "5",
            SOU6 => "6",
            SOU7 => "7",
            SOU8 => "8",
            SOU9 => "9",
            TON => "東",
            NAN => "南",
            SHA => "西",
            PEI => "北",
            HAKU => "白",
            HATSU => "發",
            CHUN => "中",
            _ => throw new InvalidOperationException()
        };
    }

    public static bool operator <(TileKind x, TileKind y) => x.Id < y.Id;

    public static bool operator >(TileKind x, TileKind y) => x.Id > y.Id;

    public int CompareTo(TileKind? other)
    {
        return other is TileKind x ? -x.Id.CompareTo(Id) : 1;
    }

    #region ValueObject<T>の実装

    public override bool EqualsCore(ValueObject<TileKind>? other)
    {
        return other is TileKind x && x.Id == Id;
    }

    public override int GetHashCodeCore()
    {
        return new { Id }.GetHashCode();
    }

    #endregion ValueObject<T>の実装
}