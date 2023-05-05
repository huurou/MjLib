using MjLib.HandCalculating.Yakus;

namespace MjLib;

public record MjYaku(int Id, int Han, string Name)
{
    internal MjYaku(Yaku yaku, bool open)
        : this(yaku.Id, open ? yaku.HanOpen : yaku.HanClosed, yaku.Name) { }
}