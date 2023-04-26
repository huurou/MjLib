namespace MjLib.HandCalculating.Scores;

/// <summary>
/// 点数 <para/>
/// 満貫ツモアガリ→ { 4000, 0 } or { 4000, 2000 } <br/>
/// 満貫ロンアガリ-> { 12000, 0 } or { 8000, 0 }
/// </summary>
/// <param name="Main">ツモ: 親の点数 ロン:手の点数</param>
/// <param name="Sub">ツモ: 子の点数 ロン:なし</param>
internal record Score(int Main, int Sub)
{
    public Score()
        : this(0, 0) { }
    public override string ToString()
    {
        return $"{Main}-{Sub}";
    }
}