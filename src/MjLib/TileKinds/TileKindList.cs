using MjLib.TileCountArrays;
using System.Diagnostics;
using static MjLib.TileKinds.TileKind;

namespace MjLib.TileKinds;

[DebuggerDisplay("{ToString()}")]
internal class TileKindList : List<TileKind>, IEquatable<TileKindList>, IComparable<TileKindList>
{
    /// <summary>
    /// 対子かどうか
    /// </summary>
    public bool IsToitsu => Count == 2 && this[0] == this[1];
    /// <summary>
    /// 順子かどうか
    /// </summary>
    public bool IsShuntsu => Count == 3 && this[0].Id == this[1].Id - 1 && this[1].Id == this[2].Id - 1;
    /// <summary>
    /// 刻子かどうか
    /// </summary>
    public bool IsKoutsu => Count == 3 && this[0] == this[1] && this[1] == this[2];
    /// <summary>
    /// 槓子かどうか
    /// </summary>
    public bool IsKantsu => Count == 4 && this[0] == this[1] && this[1] == this[2] && this[2] == this[3];

    public TileKindList()
        : base() { }

    public TileKindList(IEnumerable<TileKind> kinds)
        : base(kinds) { }

    public TileKindList(IEnumerable<int> ids)
        : this(ids.Select(x => new TileKind(x))) { }

    public TileKindList(TileCountArray countArray)
    {
        for (var id = ID_MIN; id < KIND_COUNT; id++)
        {
            AddRange(Enumerable.Repeat(new TileKind(id), countArray[id]));
        }
    }

    /// <summary>
    /// 自身及び隣り合った牌を除いたTileKindListを取得する
    /// </summary>
    /// <returns></returns>
    public TileKindList GetIsolations()
    {
        return ToTileCountArray().GetIsolations();
    }

    public TileCountArray ToTileCountArray()
    {
        return new(this);
    }

    public static TileKindList Parse(string oneLine = "")
    {
        var man = "";
        var pin = "";
        var sou = "";
        var honor = "";
        var splitStart = 0;
        for (var i = splitStart; i < oneLine.Length; i++)
        {
            if (oneLine[i] == 'm')
            {
                for (var j = splitStart; j < i; j++)
                {
                    man += oneLine[j];
                }
                splitStart = i + 1;
            }
            else if (oneLine[i] == 'p')
            {
                for (var j = splitStart; j < i; j++)
                {
                    pin += oneLine[j];
                }
                splitStart = i + 1;
            }
            else if (oneLine[i] == 's')
            {
                for (var j = splitStart; j < i; j++)
                {
                    sou += oneLine[j];
                }
                splitStart = i + 1;
            }
            else if (oneLine[i] == 'z')
            {
                for (var j = splitStart; j < i; j++)
                {
                    honor += oneLine[j];
                }
                splitStart = i + 1;
            }
        }
        return Parse(man, pin, sou, honor);
    }

    public static TileKindList Parse(string man = "", string pin = "", string sou = "", string honor = "")
    {
        var mans = man.Select(x => new TileKind(int.Parse(x.ToString()) - 1));
        var pins = pin.Select(x => new TileKind(int.Parse(x.ToString()) + 8));
        var sous = sou.Select(x => new TileKind(int.Parse(x.ToString()) + 17));
        var honors = honor.Select(x => new TileKind(int.Parse(x.ToString()) + 26));
        return new(mans.Concat(pins).Concat(sous).Concat(honors));
    }

    public string ToOneLine()
    {
        var sorted = this.OrderBy(x => x.Id);
        var mans = $"{string.Join("", sorted.Where(x => x.IsMan).Select(x => x.Id + 1))}m";
        var pins = $"{string.Join("", sorted.Where(x => x.IsPin).Select(x => x.Id - 8))}p";
        var sous = $"{string.Join("", sorted.Where(x => x.IsSou).Select(x => x.Id - 17))}s";
        var honors = $"{string.Join("", sorted.Where(x => x.IsHonor).Select(x => x.Id - 26))}z";
        return $"{mans}{pins}{sous}{honors}";
    }

    public static bool operator ==(TileKindList? x, TileKindList? y) => x?.Equals(y) ?? y is null;

    public static bool operator !=(TileKindList? x, TileKindList? y) => !(x == y);

    public static bool operator >(TileKindList x, TileKindList y) => x.CompareTo(y) > 0;

    public static bool operator <(TileKindList x, TileKindList y) => x.CompareTo(y) < 0;

    public bool Equals(TileKindList? other)
    {
        return other is TileKindList x && x.SequenceEqual(this);
    }

    public int CompareTo(TileKindList? other)
    {
        if (other is null) return 1;
        var min = Math.Min(Count, other.Count);
        for (var i = 0; i < min; i++)
        {
            if (this[i] > other[i]) return 1;
            if (this[i] < other[i]) return -1;
        }
        return Count > other.Count ? 1 : Count < other.Count ? -1 : 0;
    }

    public override string ToString()
    {
        return string.Join("", this);
    }

    public override bool Equals(object? obj)
    {
        return obj is TileKindList x && Equals(x);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}