using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlibTest
{
    internal static class TestsMixin
    {
        public static TileKindList StringToOpenTiles34(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var openSet = TileList.Parse(man: man, pin: pin, sou: sou, honor: honors);
            return new TileKindList(openSet.Select(t => t.Value / 4));
        }

        public static TileKind StringToTileKind(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var item = TileList.Parse(man: man, pin: pin, sou: sou, honor: honors);
            return new TileKind(item[0].Value / 4);
        }

        public static Meld MakeMeld(MeldType meldType,
            string man = "",
            string pin = "",
            string sou = "",
            string honors = "",
            bool isOpen = true)
        {
            var tiles = TileList.Parse(man: man, pin: pin, sou: sou, honor: honors);
            return new Meld(meldType, tiles, isOpen, tiles[0], who: 0);
        }

        public static IList<TileKindList> Hand(Tiles34 tiles, int handIndex = 0)
        {
            return HandDivider.DivideHand(tiles)[handIndex];
        }
    }
}