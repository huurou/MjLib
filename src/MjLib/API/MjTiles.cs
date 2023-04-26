using MjLib.TileKinds;

namespace MjLib.API;

/// <summary>
/// 牌の集合
/// </summary>
public class MjTiles
{
    internal TileKindList KindList { get; }

    public MjTiles()
    {
        KindList = new();
    }

    public MjTiles(int tile)
    {
        KindList = new(new[] { tile });
    }

    public MjTiles(IEnumerable<int> tiles)
    {
        KindList = new TileKindList(tiles);
    }

    public MjTiles(string? man = null, string? pin = null, string? sou = null, string? honor = null)
    {
        KindList = TileKindList.Parse(man, pin, sou, honor);
    }
}