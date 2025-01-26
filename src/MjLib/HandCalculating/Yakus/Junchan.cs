using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Junchan : Yaku
{
    public Junchan(int id)
        : base(id) { }

    public override string Name => "純全帯么九";
    public override int HanOpen => 2;
    public override int HanClosed => 3;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand, FuuroList fuuroList)
    {
        var kindLists = hand.Concat(fuuroList.TileLists);
        var shuntsu = kindLists.Count(x => x.IsShuntsu);
        var routou = kindLists.Count(x => x.Any(x => x.IsRoutou));
        return shuntsu != 0 && routou == 5;
    }
}