namespace MjLib.HandCalculating;

/// <summary>
/// 点数に関わるルール
/// </summary>
internal class OptionalRules
{
    /// <summary>
    /// 喰いタンあり/なし
    /// </summary>
    public bool HasOpenTanyao { get; init; } = true;

    /// <summary>
    /// ダブル役満あり/なし
    /// </summary>
    public bool HasDoubleYakuman { get; init; } = true;

    /// <summary>
    /// 数え役満あり/なし
    /// </summary>
    public Kazoe KazoeLimit { get; init; } = Kazoe.Limited;

    /// <summary>
    /// 切り上げ満貫あり/なし
    /// </summary>
    public bool Kiriage { get; init; } = false;

    /// <summary>
    /// ピンヅモあり/なし
    /// </summary>
    public bool Pinzumo { get; init; } = true;

    /// <summary>
    /// 人和役満あり/なし
    /// </summary>
    public bool RenhouAsYakuman { get; init; } = false;

    /// <summary>
    /// 大車輪あり/なし
    /// </summary>
    public bool HasDaisharin { get; init; } = false;
}

internal enum Kazoe
{
    /// <summary>
    /// 13翻以上は全て数え役満
    /// </summary>
    Limited = 0,
    /// <summary>
    /// 13翻以上は全て三倍満
    /// </summary>
    Sanbaiman = 1,
    /// <summary>
    /// 13翻以上は13翻ごとに数え役満が重なる
    /// </summary>
    Nolimit = 2
}