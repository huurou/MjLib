using MjLib.TileKinds;
using System.Diagnostics;

namespace MjLib.HandCalculating.Dividings;

internal class TileKindListList : List<TileKindList>, IEquatable<TileKindListList>
{
    public TileKindListList()
    {
    }

    public TileKindListList(IEnumerable<TileKindList> kindLists)
    {
        AddRange(kindLists);
    }

    public TileKindListList(params TileKindList[] kindLists)
    {
        AddRange(kindLists);
    }

    public TileKindListList(params string[] sets)
    {
        AddRange(sets.Select(x => new TileKindList(x)));
    }

    public override string ToString()
    {
        return string.Join("", this.Select(x => $"[{x}]"));
    }

    public bool Equals(TileKindListList? other)
    {
        return other is TileKindListList x && x.SequenceEqual(this);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TileKindListList);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}