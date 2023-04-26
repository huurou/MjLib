using MjLib.HandCalculating.Fus;
using MjLib.HandCalculating.Scores;
using MjLib.HandCalculating.Yakus;

namespace MjLib.HandCalculating.Hands;

/// <summary>
/// 手の評価結果
/// </summary>
internal class HandResult
{
    public int Fu { get; }
    public int Han { get; }
    public Score Score { get; } = new();
    public YakuList YakuList { get; } = new();
    public FuList FuList { get; } = new();
    public string? Error { get; }

    public HandResult(int fu, int han, Score score, YakuList yakuList, FuList fuList)
    {
        Fu = fu;
        Han = han;
        Score = score;
        YakuList = yakuList;
        FuList = fuList;
    }

    public HandResult(string? error)
    {
        Error = error;
    }
}