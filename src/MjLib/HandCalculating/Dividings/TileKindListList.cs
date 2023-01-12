using MjLib.TileKinds;
using System.Diagnostics;

namespace MjLib.HandCalculating.Dividings;

[DebuggerDisplay("{ToString()}")]
internal class TileKindListList : List<TileKindList>, IEquatable<TileKindListList>
{
    public TileKindList Toitsu => this.Where(x => x.IsToitsu).First();
    public IEnumerable<TileKindList> Shuntsus => this.Where(x => x.IsShuntsu);
    public IEnumerable<TileKindList> Koutsus => this.Where(x => x.IsKoutsu);

    public TileKindListList()
        : base() { }

    public TileKindListList(IEnumerable<TileKindList> kindLists)
        : base(kindLists) { }

    public TileKindListList(params TileKindList[] kindLists)
        : this(kindLists.AsEnumerable()) { }

    public TileKindListList(params string[] sets)
        : this(sets.Select(TileKindList.Parse)) { }

    public override string ToString()
    {
        return string.Join("", this.Select(x => $"[{x}]"));
    }

    public bool Equals(TileKindListList? other)
    {
        return other is TileKindListList x && x.SequenceEqual(this);
    }
}