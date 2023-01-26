using MjLib.HandCalculating.Hands;
using MjLib.HandCalculating.Yakus;

namespace MjLib;

public record MjHandResult
{
    public int Fu { get; }
    public int Han { get; }
    public MjScore Score { get; }
    public List<MjYaku> Yakus { get; }
    public List<MjFu> Fus { get; }

    internal MjHandResult(HandResult result, bool open)
    {
        Fu = result.Fu;
        Han = result.Han;
        Score = new(result.Score);
        var yakus = result.YakuList.Select(x => new MjYaku(x, open));
        var dora = result.YakuList.Count(x => x.Id == Yaku.Dora.Id);
        var uradora = result.YakuList.Count(x => x.Id == Yaku.Uradora.Id);
        var akadora = result.YakuList.Count(x => x.Id == Yaku.Akadora.Id);
        yakus = yakus.SkipWhile(x => x.Id == Yaku.Dora.Id || x.Id == Yaku.Uradora.Id || x.Id == Yaku.Akadora.Id);
        yakus = yakus.Append(new(Yaku.Dora, open) { Han = dora }).Append(new(Yaku.Uradora, open) { Han = dora }).Append(new(Yaku.Akadora, open) { Han = akadora });
        Yakus = yakus.ToList();
        Fus = result.FuList.Select(x => new MjFu(x)).ToList();
    }
}