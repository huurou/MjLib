using mjlib.Tiles;
using System.Linq;

namespace mjlib
{
    public class Meld
    {
        public MeldType Type { get; }
        public TileList Tiles { get; }
        public bool IsOpen { get; }
        public Tile? CalledTile { get; }
        public int? Who { get; }
        public int? FromWho { get; }

        public TileKindList KindList => new(Tiles.Take(3).Select(t => t.Value / 4));

        public Meld(MeldType meldType = MeldType.None,
            TileList? tiles = null,
            bool opened = true,
            Tile? calledTile = null,
            int? who = null,
            int? fromWho = null)
        {
            Type = meldType;
            Tiles = tiles ?? new();
            IsOpen = opened;
            CalledTile = calledTile;
            Who = who;
            FromWho = fromWho;
        }

        public override string ToString()
        {
            return $"Type: {Type}\tTiles: {Tiles.ToOneLineString()}";
        }
    }

    public enum MeldType
    {
        None,
        Chi,
        Pon,
        Kan,
        Chankan,
        Nuki,
    }
}