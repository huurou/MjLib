namespace MjLib.HandCalculating.Scores;

/// <summary>
/// 点数 <para/>
/// <see cref="Main"/>: ツモ: 親の点数 ロン:手の点数
/// <see cref="Sub"/>: ツモ: 子の点数 ロン:なし
/// 満貫ツモアガリ→ { 4000, 0 } or { 4000, 2000 }
/// 満貫ロンアガリ-> { 12000, 0 } or { 8000, 0 }
/// </summary>
internal class Score : ValueObject<Score>
{
    public int Fu { get; }
    public int Han { get; }

    /// <summary>
    /// ツモ時:親の点数 ロン:手の点数
    /// </summary>
    public int Main { get; }
    /// <summary>
    /// ツモ:子の点数 ロン:なし
    /// </summary>
    public int Sub { get; }

    public Score(int fu, int han, int main, int sub)
    {
        Fu = fu;
        Han = han;
        Main = main;
        Sub = sub;
    }

    public override bool EqualsCore(ValueObject<Score>? other)
    {
        return other is Score x &&
            x.Fu == Fu &&
            x.Han == Han &&
            x.Main == Main &&
            x.Sub == Sub;
    }

    public override int GetHashCodeCore()
    {
        return new { Fu, Han, Main, Sub }.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Fu}符{Han}翻{Main}-{Sub}";
    }
}