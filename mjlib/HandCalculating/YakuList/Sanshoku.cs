using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Sanshoku : YakuBase
    {
        public override int YakuId => 22;

        public override int TenhouId => 25;

        public override string Name => "Sanshoku Doujun";

        public override string Japanese => "三色同順";

        public override string English => "Three Colored Triplets";

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
            foreach (var manItem in manChi)
            {
                foreach (var pinItem in pinChi)
                {
                    foreach (var souItem in souChi)
                    {
                        var manNum = new TileKindList(manItem.Select(x => x.Simplify));
                        var pinNum = new TileKindList(pinItem.Select(x => x.Simplify));
                        var souNum = new TileKindList(souItem.Select(x => x.Simplify));
                        if (manNum.Equals(pinNum) && pinNum.Equals(souNum)) return true;
                    }
                }
            }
            return false;
        }
    }
}