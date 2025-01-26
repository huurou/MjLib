using MjLib.TileKinds;

namespace MjLib.Fuuros;

internal class FuuroList : List<Fuuro>
{
    public bool HasOpen => this.Any(x => x.IsOpen);
    public IEnumerable<TileList> TileLists => this.Select(x => x.Tiles);

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