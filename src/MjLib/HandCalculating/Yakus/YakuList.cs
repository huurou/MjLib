using System.Diagnostics;

namespace MjLib.HandCalculating.Yakus;

[DebuggerDisplay("{ToString()}")]
internal class YakuList : List<Yaku>
{
    public int HanOpen => this.Sum(x => x.HanOpen);
    public int HanClosed => this.Sum(x => x.HanClosed);

    public YakuList()
        : base() { }

    public YakuList(IEnumerable<Yaku> yakus)
        : base(yakus) { }

    public override string ToString()
    {
        return string.Join(",", this);
    }
}