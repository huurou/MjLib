﻿using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating
{
    public abstract class YakuBase
    {
        public abstract int YakuId { get; }
        public abstract int TenhouId { get; }
        public abstract string Name { get; }
        public abstract string Japanese { get; }
        public abstract string English { get; }
        public abstract int HanOpen { get; set; }
        public abstract int HanClosed { get; set; }
        public abstract bool IsYakuman { get; }

        public abstract bool Valid(IEnumerable<TileKindList>? hand, params object[] args);
    }
}