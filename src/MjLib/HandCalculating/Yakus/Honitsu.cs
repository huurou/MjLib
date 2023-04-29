using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Honitsu : Yaku
{
    public Honitsu(int id)
        : base(id) { }

    public override string Name => "混一色";
    public override int HanOpen => 2;
    public override int HanClosed => 3;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand,FuuroList fuuroList)
    {
        var tileLists = hand.Concat(fuuroList.Select(x => x.TileKindList));
        var man = tileLists.Count(x => x[0].IsMan);
        var pin = tileLists.Count(x => x[0].IsPin);
        var sou = tileLists.Count(x => x[0].IsSou);
        var honor = tileLists.Count(x => x[0].IsHonor);
        return new[] { man, pin, sou }.Count(x => x != 0) == 1 && honor != 0;
    }
}