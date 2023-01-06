﻿using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Honroto : YakuBase
    {
        public override int YakuId => 25;

        public override int TenhouId => 31;

        public override string Name => "Honroto";

        public override string Japanese => "混老頭";

        public override string English => "Terminals and Honors";

        public override int HanOpen { get; set; } = 2;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            if (hand is null) return false;
            var indices = hand.Aggregate((x, y) => x.AddRange(y));
            return indices.All(x => Constants.YAOCHU_INDICES.Contains(x.Value));
        }
    }
}