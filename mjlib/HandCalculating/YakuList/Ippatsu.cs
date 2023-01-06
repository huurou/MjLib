using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Ippatsu : YakuBase
    {
        public override int YakuId => 2;
        public override int TenhouId => 2;
        public override string Name => "Ippatsu";
        public override string Japanese => "一発";
        public override string English => "One Shot";
        public override int HanOpen { get; set; } = 0;
        public override int HanClosed { get; set; } = 1;
        public override bool IsYakuman => false;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            return true;
        }
    }
}