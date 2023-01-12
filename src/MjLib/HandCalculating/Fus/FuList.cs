using MjLib.HandCalculating.Fus;

namespace MjLib.HandCalculating;

internal class FuList : List<Fu>
{
    public int Total => Contains(Fu.Chitoitsu)
        ? 25
        : (this.Sum(x => x.Value) + 9) / 10 * 10;
}