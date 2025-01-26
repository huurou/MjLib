using MjLib.TileKinds;
using System.Diagnostics;

namespace MjLib.HandCalculating.Dividings;

internal class TileListList : List<TileList>, IEquatable<TileListList>
{
    public TileListList()
    {
    }

    public TileListList(IEnumerable<TileList> kindLists)
    {
        AddRange(kindLists);
    }

    public TileListList(params TileList[] kindLists)
    {
        AddRange(kindLists);
    }

    public TileListList(params string[] sets)
    {
        AddRange(sets.Select(x => new TileList(x)));
    }

    public override string ToString()
    {
        return string.Join("", this.Select(x => $"[{x}]"));
    }

    public bool Equals(TileListList? other)
    {
        return other is TileListList x && x.SequenceEqual(this);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TileListList);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}