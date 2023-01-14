﻿using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.HandCalculating.Yakus;

internal class Ryuuiisou : Yaku
{
    public Ryuuiisou(int id)
        : base(id) { }

    public override string Name => "緑一色";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand)
    {
        var greens = new[] { Sou2, Sou3, Sou4, Sou6, Sou8, TileKind.Hatsu };
        return hand.SelectMany(x => x).Distinct().All(x => greens.Contains(x));
    }
}