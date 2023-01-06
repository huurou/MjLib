using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;
using static System.Linq.Enumerable;

namespace mjlib.Tiles
{
    public class TileList : IList<Tile>, IEquatable<TileList>
    {
        private readonly List<Tile> tiles_;

        public int Count => tiles_.Count;

        public bool IsReadOnly => ((ICollection<Tile>)tiles_).IsReadOnly;

        public Tile this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public TileList()
        {
            tiles_ = new List<Tile>();
        }

        public TileList(IEnumerable<Tile> tiles)
        {
            tiles_ = tiles.ToList();
        }

        public TileList(IEnumerable<int> tiles)
        {
            tiles_ = tiles.Select(t => new Tile(t)).ToList();
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            return tiles_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tiles_).GetEnumerator();
        }

        public TileKindList ToTileKinds()
        {
            return new(this.Select(x => x.Value / 4));
        }

        public Tiles34 ToTiles34()
        {
            var result = new Tiles34();
            foreach (var tile in this)
            {
                result[tile.Value / 4] += 1;
            }
            return result;
        }

        public string ToOneLineString(bool printAkaDora = false)
        {
            var tiles = this.OrderBy(t => t.Value)
                            .Select(t => t.Value);

            var man = tiles.Where(t => t < 36);
            var pin = tiles.Where(t => t is >= 36 and < 72)
                           .Select(t => t - 36);
            var sou = tiles.Where(t => t is >= 72 and < 108)
                           .Select(t => t - 72);
            var honors = tiles.Where(t => t >= 108)
                              .Select(t => t - 108);

            string Words(IEnumerable<int> suits, int redFive, string suffix) =>
                !suits.Any()
                    ? ""
                    : string.Join("", suits.Select(t => t == redFive && printAkaDora
                            ? "0"
                            : (t / 4 + 1).ToString())) + suffix;
            var manStr = Words(man, FIVE_RED_MAN, "m");
            var pinStr = Words(pin, FIVE_RED_PIN, "p");
            var souStr = Words(sou, FIVE_RED_SOU, "s");
            var honorsStr = Words(honors, -1, "z");

            return manStr + pinStr + souStr + honorsStr;
        }

        public Tile? FindTileKind(TileKind tileKind)
        {
            if (tileKind is null || tileKind.Value > 33)
            {
                return null;
            }
            var tile = tileKind.Value * 4;
            var possibleTiles = Range(0, 4).Select(i => tile + i);
            Tile? foundTile = null;
            foreach (var possibleTile in possibleTiles)
            {
                if (this.Select(t => t.Value).Contains(possibleTile))
                {
                    foundTile = new Tile(possibleTile);
                    break;
                }
            }
            return foundTile;
        }

        public static TileList Parse(string str, bool hasAkaDora = false)
        {
            var man = "";
            var pin = "";
            var sou = "";
            var honors = "";

            var splitStart = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == 'm')
                {
                    for (var j = splitStart; j < i; j++)
                    {
                        man += str[j].ToString();
                    }
                    splitStart = i + 1;
                }
                if (str[i] == 'p')
                {
                    for (var j = splitStart; j < i; j++)
                    {
                        pin += str[j].ToString();
                    }
                    splitStart = i + 1;
                }
                if (str[i] == 's')
                {
                    for (var j = splitStart; j < i; j++)
                    {
                        sou += str[j].ToString();
                    }
                    splitStart = i + 1;
                }
                if (str[i] == 'z')
                {
                    for (var j = splitStart; j < i; j++)
                    {
                        honors += str[j].ToString();
                    }
                    splitStart = i + 1;
                }
            }
            return Parse(man: man, pin: pin, sou: sou, honor: honors, hasAkaDora);
        }

        public static TileList Parse(string man = "", string pin = "", string sou = "",
            string honor = "", bool hasAkaDora = false)
        {
            IList<int> SplitString(string str, int offset, int red)
            {
                var temp = new List<int>();
                var data = new List<int>();
                if (string.IsNullOrEmpty(str))
                {
                    return temp;
                }
                foreach (var i in str)
                {
                    if ((i == 'r' || i == '0') && hasAkaDora)
                    {
                        temp.Add(red);
                        data.Add(red);
                    }
                    else
                    {
                        var tile = offset + (int.Parse(i.ToString()) - 1) * 4;
                        if (tile == red && hasAkaDora)
                        {
                            tile += 1;
                        }
                        if (data.Contains(tile))
                        {
                            var count = temp.Count(x => x == tile);
                            var newTile = tile + count;
                            data.Add(newTile);
                            temp.Add(tile);
                        }
                        else
                        {
                            data.Add(tile);
                            temp.Add(tile);
                        }
                    }
                }
                return data;
            }
            var result = SplitString(man, 0, FIVE_RED_MAN).ToList();
            result.AddRange(SplitString(pin, 36, FIVE_RED_PIN));
            result.AddRange(SplitString(sou, 72, FIVE_RED_SOU));
            result.AddRange(SplitString(honor, 108, -1));

            return new TileList(result);
        }

        public override bool Equals(object? obj)
        {
            return obj is TileList other && Equals(other);
        }

        public bool Equals(TileList? other)
        {
            if (other is null) return false;
            if (Count != other.Count) return false;
            for (var i = 0; i < tiles_.Count; i++)
            {
                if (!this[i].Equals(other[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Add(int item)
        {
            tiles_.Add(new Tile(item));
        }

        public void Add(Tile item)
        {
            tiles_.Add(item);
        }

        public int IndexOf(Tile item)
        {
            return ((IList<Tile>)tiles_).IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Tile>)tiles_).RemoveAt(index);
        }

        public bool Contains(Tile? item)
        {
            return item is not null && ((ICollection<Tile>)tiles_).Contains(item);
        }

        public void Insert(int index, Tile item)
        {
            ((IList<Tile>)tiles_).Insert(index, item);
        }

        public void Clear()
        {
            ((ICollection<Tile>)tiles_).Clear();
        }

        public void CopyTo(Tile[] array, int arrayIndex)
        {
            ((ICollection<Tile>)tiles_).CopyTo(array, arrayIndex);
        }

        public bool Remove(Tile item)
        {
            return ((ICollection<Tile>)tiles_).Remove(item);
        }
    }
}