using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class YakuhaiSouth : Yaku
    {
        public override int YakuId => 17;

        public override int TenhouId => 10;

        public override string Name => "Yakuhai (south)";

        public override string Japanese => "役牌(南)";

        public override string English => "South Round/Seat";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 1;

        public override bool IsYakuman => false;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            if (hand is null) return false;
            if (args is null) return false;
            var playerWind = (int)args[0];
            var roundWind = (int)args[1];

            return hand.Count(
                x => x.IsPon && x[0].Value == playerWind) == 1
                    && playerWind == Constants.SOUTH
                || hand.Count(
                x => x.IsPon && x[0].Value == roundWind) == 1
                    && roundWind == Constants.SOUTH;
        }
    }
}