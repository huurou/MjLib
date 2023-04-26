using MjLib.HandCalculating;

namespace MjLib.API;

public record MjGameRules
{
    /// <summary>
    /// 喰いタンあり/なし
    /// </summary>
    public bool Kuitan { get; init; } = true;

    /// <summary>
    /// ダブル役満あり/なし
    /// </summary>
    public bool DaburuYakuman { get; init; } = true;

    /// <summary>
    /// 数え役満
    /// </summary>
    public MjKazoe KazoeLimit { get; init; } = MjKazoe.Limited;

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
    public bool Daisharin { get; init; } = false;

    internal GameRules ToInternalModel()
    {
        return new()
        {
            Kuitan = Kuitan,
            DaburuYakuman = DaburuYakuman,
            KazoeLimit = (Kazoe)KazoeLimit,
            Kiriage = Kiriage,
            Pinzumo = Pinzumo,
            RenhouAsYakuman = RenhouAsYakuman,
            Daisharin = Daisharin,
        };
    }
}

public enum MjKazoe
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