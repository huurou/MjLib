﻿using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Suuankou : YakuBase
    {
        public override int YakuId => 38;

        public override int TenhouId => 41;

        public override string Name => "Suuankou";

        public override string Japanese => "四暗刻";

        public override string English => "Four Concealed Triplets";

        public override int HanOpen { get; set; } = 0;

        public override int HanClosed { get; set; } = 13;

        public override bool IsYakuman => true;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            if (hand is null) return false;
            if (args is null) return false;
            var winTile = ((Tile)args[0]).ToTileKind();
            var isTsumo = (bool)args[1];
            var closedHand = new List<TileKindList>();
            foreach (var item in hand)
            {
                if (item.IsPon && item.Contains(winTile) && !isTsumo) continue;
                closedHand.Add(item);
            }
            return closedHand.Count(x => x.IsPon) == 4;
        }
    }
}