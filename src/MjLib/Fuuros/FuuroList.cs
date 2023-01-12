using System.Diagnostics;

namespace MjLib.Fuuros;

[DebuggerDisplay("{ToString()}")]
internal class FuuroList : List<Fuuro>
{
    public IEnumerable<Fuuro> Chis => this.Where(x => x.IsChi);
    public IEnumerable<Fuuro> Pons => this.Where(x => x.IsPon);
    public IEnumerable<Fuuro> Ankans => this.Where(x => x.IsAnkan);
    public IEnumerable<Fuuro> Minkans => this.Where(x => x.IsMinkan);
    public bool HasOpen => this.Any(x => x.IsOpen);

    public override string ToString()
    {
        return string.Join(",", this);
    }
}