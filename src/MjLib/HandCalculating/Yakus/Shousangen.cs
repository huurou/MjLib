using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Shousangen : Yaku
{
    public Shousangen(int id)
        : base(id) { }

    public override string Name => "小三元";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand, FuuroList fuuroList)
    {
        return hand.Where(x => (x.IsToitsu || x.IsKoutsu) && x[0].IsDragon)
            .Concat(fuuroList.Where(x => (x.IsPon || x.IsKan) && x.Tiles[0].IsDragon).Select(x => x.Tiles))
            .Count() == 3;
    }
}