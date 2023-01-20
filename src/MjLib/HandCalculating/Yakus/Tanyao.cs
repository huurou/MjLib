﻿using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal class Tanyao : Yaku
{
    public Tanyao(int id)
        : base(id) { }

    public override string Name => "断么九";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList, OptionalRules rules)
    {
        return hand.Concat(fuuroList.KindLists).SelectMany(x => x).Distinct().All(x => x.IsChuuchan) &&
            (!fuuroList.HasOpen || rules.Kuitan);
    }
}