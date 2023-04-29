namespace MjLib.HandCalculating.Yakus;

internal class YakuList : List<Yaku>
{
    public int HanOpen => this.Sum(x => x.HanOpen);
    public int HanClosed => this.Sum(x => x.HanClosed);

    public YakuList()
    {
    }

    public YakuList(IEnumerable<Yaku> yakus)
    {
        AddRange(yakus);
    }

    public override string ToString()
    {
        return string.Join(",", this);
    }
}