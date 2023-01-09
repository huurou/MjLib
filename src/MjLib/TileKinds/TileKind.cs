namespace MjLib.TileKinds;

/// <summary>
/// 牌種別
/// </summary>
internal class TileKind : ValueObject<TileKind>
{
    public static TileKind Man1 => new(0);
    public static TileKind Man2 => new(1);
    public static TileKind Man3 => new(2);
    public static TileKind Man4 => new(3);
    public static TileKind Man5 => new(4);
    public static TileKind Man6 => new(5);
    public static TileKind Man7 => new(6);
    public static TileKind Man8 => new(7);
    public static TileKind Man9 => new(8);
    public static TileKind Pin1 => new(9);
    public static TileKind Pin2 => new(10);
    public static TileKind Pin3 => new(11);
    public static TileKind Pin4 => new(12);
    public static TileKind Pin5 => new(13);
    public static TileKind Pin6 => new(14);
    public static TileKind Pin7 => new(15);
    public static TileKind Pin8 => new(16);
    public static TileKind Pin9 => new(17);
    public static TileKind Sou1 => new(18);
    public static TileKind Sou2 => new(19);
    public static TileKind Sou3 => new(20);
    public static TileKind Sou4 => new(21);
    public static TileKind Sou5 => new(22);
    public static TileKind Sou6 => new(23);
    public static TileKind Sou7 => new(24);
    public static TileKind Sou8 => new(25);
    public static TileKind Sou9 => new(26);
    public static TileKind Ton => new(27);
    public static TileKind Nan => new(28);
    public static TileKind Sha => new(29);
    public static TileKind Pei => new(30);
    public static TileKind Haku => new(31);
    public static TileKind Hatsu => new(32);
    public static TileKind Chun => new(33);

    public const int ID_MIN = 0;
    public const int ID_MAX = 33;

    /// <summary>
    /// 牌種別ID 0～33
    /// </summary>
    public int Id { get; }
    /// <summary>
    /// 牌に書かれている番号 萬子・筒子・索子は1～9 字牌は東南西北白發中の順に1～7
    /// </summary>
    public int Number => IsMan ? Id + 1 : IsPin ? Id - 8 : IsSou ? Id - 17 : IsHonor ? Id - 26 : throw new InvalidOperationException();
    public bool IsMan => Id is >= 0 and <= 8;
    public bool IsPin => Id is >= 9 and <= 17;
    public bool IsSou => Id is >= 18 and <= 26;
    public bool IsHonor => Id is >= 27 and <= 33;
    public bool IsWind => Id is 27 or 28 or 29 or 30;
    public bool IsDragon => Id is 31 or 32 or 33;
    public bool IsChuchan => (IsMan || IsPin || IsSou) && Number is >= 2 and <= 8;
    public bool IsYaochu => !IsChuchan;
    public static IEnumerable<TileKind> AllKind => Enumerable.Range(ID_MIN, ID_MAX + 1).Select(x => new TileKind(x));

    public TileKind(int id)
    {
        Id = ID_MIN <= id && id <= ID_MAX
            ? id
            : throw new ArgumentException($"牌種別IDは{ID_MIN}～{ID_MAX}です。", nameof(id));
    }

    public override string ToString()
    {
        return Id switch
        {
            0 => "一",
            1 => "二",
            2 => "三",
            3 => "四",
            4 => "五",
            5 => "六",
            6 => "七",
            7 => "八",
            8 => "九",
            9 => "(1)",
            10 => "(2)",
            11 => "(3)",
            12 => "(4)",
            13 => "(5)",
            14 => "(6)",
            15 => "(7)",
            16 => "(8)",
            17 => "(9)",
            18 => "1",
            19 => "2",
            20 => "3",
            21 => "4",
            22 => "5",
            23 => "6",
            24 => "7",
            25 => "8",
            26 => "9",
            27 => "東",
            28 => "南",
            29 => "西",
            30 => "北",
            31 => "白",
            32 => "發",
            33 => "中",
            _ => throw new InvalidOperationException()
        };
    }

    public static TileKind operator +(TileKind x, int y)
    {
        return new(x.Id + y);
    }

    public static TileKind operator -(TileKind x, int y)
    {
        return new(x.Id - y);
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