namespace MjLib.Huuros;

internal class HuuroList : List<Huuro>
{
    public HuuroList()
        : base() { }

    public HuuroList(IEnumerable<Huuro> huuros)
        : base(huuros) { }
}