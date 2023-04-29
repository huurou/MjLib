using MjLib.TileKinds;

namespace MjLib.Fuuros;

internal class FuuroList : List<Fuuro>
{
    public bool HasOpen => this.Any(x => x.IsOpen);
    public IEnumerable<TileKindList> KindLists => this.Select(x => x.TileKindList);

    public FuuroList()
    {
    }

    public FuuroList(IEnumerable<Fuuro> fuuros)
    {
        AddRange(fuuros);
    }

    public override string ToString()
    {
        return string.Join(",", this);
    }
}