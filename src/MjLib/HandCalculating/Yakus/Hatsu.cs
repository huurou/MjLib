﻿using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal record Hatsu : Yaku
{
    public Hatsu(int id)
        : base(id) { }

    public override string Name => "發";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileListList hand, FuuroList fuuroList_)
    {
        return hand.Concat(fuuroList_.TileLists)
                   .Where(x => x.IsKoutsu || x.IsKantsu)
                   .Any(x => x[0] == Tile.Hatsu);
    }
}