using MjLib.HandCalculating.Fus;
using System.Diagnostics;

namespace MjLib.HandCalculating;

[DebuggerDisplay("{ToString()}")]
internal class FuList : List<Fu>
{
    public int Total => Contains(Fu.Chiitoitsu)
        ? 25
        : (this.Sum(x => x.Value) + 9) / 10 * 10;

    public override string ToString()
    {
        return $"{Total}符 {string.Join(',', this)}";
    }
}