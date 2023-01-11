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

    public TileKindListList(IEnumerable<TileKindList> kindList)
        : base(kindList) { }

    public override string ToString()
    {
        return string.Join("", this.Select(x => $"[{x}]"));
    }

    public bool Equals(TileKindListList? other)
    {
        return other is TileKindListList x &&x.SequenceEqual(this);
    }
}