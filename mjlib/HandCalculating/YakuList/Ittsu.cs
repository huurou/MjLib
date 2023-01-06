using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Ittsu : YakuBase
    {
        public override int YakuId => 23;

        public override int TenhouId => 24;

        public override string Name => "Ittsu";

        public override string Japanese => "一気通貫";

        public override string English => "Straight";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool Valid(IEnumerable<TileKindList>? hand, params object[] args)
        {
            if (hand is null) return false;
            var chiSets = hand.Where(x => x.IsChi);
            if (chiSets.Count() < 3) return false;

            var manChi = new List<TileKindList>();
            var pinChi = new List<TileKindList>();
            var souChi = new List<TileKindList>();
            foreach (var item in chiSets)
            {
                if (item[0].IsMan)
                {
                    manChi.Add(item);
                }
                if (item[0].IsPin)
                {
                    pinChi.Add(item);
                }
                if (item[0].IsSou)
                {
                    souChi.Add(item);
                }
            }
            var sets = new List<List<TileKindList>>
            {
                manChi, pinChi, souChi
            };
            foreach (var suitItem in sets)
            {
                if (suitItem.Count < 3) continue;
                var castedSets = new List<TileKindList>();
                foreach (var setItem in suitItem)
                {
                    castedSets.Add(new TileKindList(new List<TileKind>
                    {
                         new TileKind(setItem[0].Simplify),
                         new TileKind(setItem[1].Simplify),
                         new TileKind(setItem[2].Simplify),
                    }));
                }
                return castedSets.Contains(new TileKindList(new List<int> { 0, 1, 2 }))
                    && castedSets.Contains(new TileKindList(new List<int> { 3, 4, 5 }))
                    && castedSets.Contains(new TileKindList(new List<int> { 6, 7, 8 }));
            }
            return false;
        }
    }
}