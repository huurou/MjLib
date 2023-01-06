using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class SanKantsu : YakuBase
    {
        public override int YakuId => 28;

        public override int TenhouId => 27;

        public override string Name => "SanKantsu";

        public override string Japanese => "三槓子";

        public override string English => "Three Kans";

        public override int HanOpen { get; set; } = 2;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            if (args is null) return false;
            var melds = (List<Meld>)args[0];
            return melds.Where(x => x.Type is MeldType.Kan or MeldType.Chankan)
                        .Count() == 3;
        }
    }
}