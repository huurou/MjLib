using MjLib.HandCalculating.Yakus;

namespace MjLib;

public class MjYaku
{
    public int Id { get; init; }
    public int Han { get; init; }
    public string Name { get; init; }

    internal MjYaku(Yaku yaku, bool open)
    {
        Id = yaku.Id;
        Han = open ? yaku.HanOpen : yaku.HanClosed;
        Name = yaku.Name;
    }
}