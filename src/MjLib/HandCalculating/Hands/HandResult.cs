using MjLib.HandCalculating.Scores;
using MjLib.HandCalculating.Yakus;

namespace MjLib.HandCalculating.Hands;

/// <summary>
/// 手の評価結果
/// </summary>
internal class HandResult
{
    public Score Score { get; } = new(0, 0, 0, 0);
    public YakuList YakuList { get; } = new();
    public FuList FuList { get; } = new();
    public string? Error { get; }

    public HandResult(Score score, int fu, int han, YakuList yakuList, FuList fuList)
    {
        Score = score;
        YakuList = yakuList;
        FuList = fuList;
    }

    public HandResult(string? error)
    {
        Error = error;
    }
}
