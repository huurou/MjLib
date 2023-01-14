using MjLib.TileKinds;
using System.Diagnostics;

namespace MjLib.Fuuros;

[DebuggerDisplay("{ToString()}")]
internal class FuuroList : List<Fuuro>
{
    public bool HasOpen => this.Any(x => x.IsOpen);
    public IEnumerable<TileKindList> KindLists => this.Select(x => x.KindList);

    public override string ToString()
    {
        return string.Join(",", this);
    }
}