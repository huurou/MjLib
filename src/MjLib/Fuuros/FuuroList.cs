using MjLib.TileKinds;
using System.Diagnostics;

namespace MjLib.Fuuros;

internal class FuuroList : List<Fuuro>
{
    public bool HasOpen => this.Any(x => x.IsOpen);
    public IEnumerable<TileKindList> KindLists => this.Select(x => x.KindList);

    public FuuroList()
        : base() { }

    public FuuroList(IEnumerable<Fuuro> fuuros)
        : base(fuuros) { }

    public override string ToString()
    {
        return string.Join(",", this);
    }
}