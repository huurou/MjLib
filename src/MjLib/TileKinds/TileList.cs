using MjLib.TileCountArrays;
using static MjLib.TileKinds.Tile;

namespace MjLib.TileKinds;

internal class TileList : List<Tile>, IEquatable<TileList>, IComparable<TileList>
{
    /// <summary>
    /// 対子かどうか
    /// </summary>
    public bool IsToitsu => Count == 2 && this[0] == this[1];
    /// <summary>
    /// 順子かどうか
    /// </summary>
    public bool IsShuntsu => Count == 3 && this[0].Id == this[1].Id - 1 && this[1].Id == this[2].Id - 1 &&
        (this.All(x => x.IsMan) || this.All(x => x.IsPin) || this.All(x => x.IsSou));
    /// <summary>
    /// 刻子かどうか
    /// </summary>
    public bool IsKoutsu => Count == 3 && this[0] == this[1] && this[1] == this[2];
    /// <summary>
    /// 槓子かどうか
    /// </summary>
    public bool IsKantsu => Count == 4 && this[0] == this[1] && this[1] == this[2] && this[2] == this[3];

    public TileList()
    {
    }

    public TileList(IEnumerable<Tile> kinds)
    {
        AddRange(kinds);
    }

    public TileList(params Tile[] kinds)
    {
        if (kinds is not null)
        {
            AddRange(kinds);
        }
    }

    public TileList(IEnumerable<int> ids)
    {
        AddRange(ids.Select(x => new Tile(x)));
    }

    public TileList(CountArray countArray)
    {
        for (var id = ID_MIN; id < KIND_COUNT; id++)
        {
            AddRange(Enumerable.Repeat(new Tile(id), countArray[id]));
        }
    }

    public TileList(string? man = null, string? pin = null, string? sou = null, string? honor = null)
    {
        AddRange(Parse(man, pin, sou, honor));
    }

    public TileList(string oneLine = "")
    {
        AddRange(Parse(oneLine));
    }

    private static IEnumerable<Tile> Parse(string oneLine = "")
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

    private static IEnumerable<Tile> Parse(string? man = null, string? pin = null, string? sou = null, string? honor = null)
    {
        var mans = (man ?? "").Select(x => new Tile(int.Parse(x.ToString()) - 1));
        var pins = (pin ?? "").Select(x => new Tile(int.Parse(x.ToString()) + 8));
        var sous = (sou ?? "").Select(x => new Tile(int.Parse(x.ToString()) + 17));
        var honors = (honor ?? "").Select(x => new Tile(int.Parse(x.ToString()) + 26));
        return mans.Concat(pins).Concat(sous).Concat(honors);
    }

    /// <summary>
    /// 自身及び隣り合った牌を除いたTileKindListを取得する
    /// </summary>
    /// <returns></returns>
    public TileList GetIsolations()
    {
        return ToTileCountArray().GetIsolations();
    }

    public CountArray ToTileCountArray()
    {
        return new(this);
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

    public static bool operator ==(TileList? x, TileList? y) => x?.Equals(y) ?? y is null;

    public static bool operator !=(TileList? x, TileList? y) => !(x == y);

    public static bool operator >(TileList x, TileList y) => x.CompareTo(y) > 0;

    public static bool operator <(TileList x, TileList y) => x.CompareTo(y) < 0;

    public bool Equals(TileList? other)
    {
        return other is TileList x && x.SequenceEqual(this);
    }

    public int CompareTo(TileList? other)
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
        return obj is TileList x && Equals(x);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var tile in this)
        {
            hashCode.Add(tile);
        }
        return hashCode.ToHashCode();
    }

}