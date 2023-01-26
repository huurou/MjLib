using MjLib.HandCalculating.Scores;

namespace MjLib;

/// <summary>
/// 点数 <para/>
/// <see cref="Main"/>: ツモ: 親の点数 ロン:手の点数
/// <see cref="Sub"/>: ツモ: 子の点数 ロン:なし
/// 満貫ツモアガリ→ { 4000, 0 } or { 4000, 2000 }
/// 満貫ロンアガリ-> { 12000, 0 } or { 8000, 0 }
/// </summary>
public class MjScore
{
    /// <summary>
    /// ツモ時:親の点数 ロン:手の点数
    /// </summary>
    public int Main { get; init; }
    /// <summary>
    /// ツモ:子の点数 ロン:なし
    /// </summary>
    public int Sub { get; init; }

    public MjScore()
    {
    }

    internal MjScore(Score score)
    {
        Main = score.Main;
        Sub = score.Sub;
    }
}