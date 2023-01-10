using MjLib.TileKinds;
using System.Diagnostics;

namespace MjLib.HandCalculating.Dividings;

[DebuggerDisplay("{ToString()}")]
internal class DevidedHand : List<TileKindList>
{
    public TileKindList Toitsu => this.Where(x => x.IsToitsu).First();
    public IEnumerable<TileKindList> Shuntsus => this.Where(x => x.IsShuntsu);
    public IEnumerable<TileKindList> Koutsus => this.Where(x => x.IsKoutsu);

    public override string ToString()
    {
        return string.Join("", this.Select(x => $"[{x}]"));
    }
}