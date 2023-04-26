using MjLib.HandCalculating.Fus;

namespace MjLib;

public class MjFu
{
    public int Id { get; init; }
    public int Value { get; init; }
    public string Reason { get; init; }

    internal MjFu(Fu fu)
    {
        Id = fu.Id;
        Value = fu.Value;
        Reason = fu.Reason;
    }
}