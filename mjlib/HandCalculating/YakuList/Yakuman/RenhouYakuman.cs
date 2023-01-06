using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class RenhouYakuman : YakuBase
    {
        public override int YakuId => 52;

        public override int TenhouId => -1;

        public override string Name => "Renhou";

        public override string Japanese => "人和";

        public override string English => "Hand of Man";

        public override int HanOpen { get; set; } = 13;

        public override int HanClosed { get; set; } = 13;

        public override bool IsYakuman => true;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            return true;
        }
    }
}