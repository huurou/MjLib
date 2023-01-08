using MjLib.TileCountArrays;
using System.Diagnostics;
using static MjLib.TileKinds.TileKind;

namespace MjLib.TileKinds;

[DebuggerDisplay("{ToString()}")]
internal class TileKindList : List<TileKind>
{
    public TileKindList()
        : base() { }

    public TileKindList(IEnumerable<TileKind> kinds)
        : base(kinds) { }

    public TileKindList(IEnumerable<int> ids)
        : this(ids.Select(x => new TileKind(x))) { }

    public TileKindList(TileCountArray countArray)
    {
        for (var id = ID_MIN; id <= ID_MAX; id++)
        {
            AddRange(Enumerable.Repeat(new TileKind(id), countArray[id]));
        }
    }

    /// <summary>
    /// 自身及び隣り合った牌を除いたTileKindListを取得する
    /// </summary>
    /// <returns></returns>
    public TileKindList GetIsolated()
    {
        var countArray = ToTileCountArray();
        var isolated = new TileKindList();
        foreach (var kind in AllKinds)
        {
            if (kind.IsHonor && countArray[kind] == 0 ||
                !kind.IsHonor && kind.Number == 1 && countArray[kind] == 0 && countArray[kind + 1] == 0 ||
                !kind.IsHonor && kind.Number is >= 2 and <= 8 && countArray[kind - 1] == 0 && countArray[kind] == 0 && countArray[kind + 1] == 0 ||
                !kind.IsHonor && kind.Number == 9 && countArray[kind - 1] == 0 && countArray[kind] == 0)
            {
                isolated.Add(kind);
            }
        }
        return isolated;
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

    public override string ToString()
    {
        return string.Join("", this);
    }
}