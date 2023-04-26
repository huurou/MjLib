using MjLib.HandCalculating.Fus;

namespace MjLib.API;

public record MjFu(int Id, int Value, string Reason)
{
    internal MjFu(Fu fu)
        : this(fu.Id, fu.Value, fu.Reason) { }
}