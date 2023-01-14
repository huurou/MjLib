namespace MjLib.HandCalculating;

internal class HandConfig
{
    public OptionalRules Rurles { get; init; } = new();

    /// <summary>
    /// ツモアガリかどうか
    /// </summary>
    public bool IsTsumo { get; init; } = false;

    /// <summary>
    /// リーチ
    /// </summary>
    public bool IsRiichi { get; init; } = false;

    /// <summary>
    /// 一発
    /// </summary>
    public bool IsIppatsu { get; init; } = false;

    /// <summary>
    /// 嶺上開花
    /// </summary>
    public bool IsRinshan { get; init; } = false;

    /// <summary>
    /// 槍槓
    /// </summary>
    public bool IsChankan { get; init; } = false;

    /// <summary>
    /// 海底撈月
    /// </summary>
    public bool IsHaitei { get; init; } = false;

    /// <summary>
    /// 河底撈魚
    /// </summary>
    public bool IsHoutei { get; init; } = false;

    /// <summary>
    /// ダブル立直
    /// </summary>
    public bool IsDaburuRiichi { get; init; } = false;

    /// <summary>
    /// 流し満貫
    /// </summary>
    public bool IsNagashimangan { get; init; } = false;

    /// <summary>
    /// 天和
    /// </summary>
    public bool IsTenhou { get; init; } = false;

    /// <summary>
    /// 地和
    /// </summary>
    public bool IsChiihou { get; init; } = false;

    /// <summary>
    /// 人和
    /// </summary>
    public bool IsRenhou { get; init; } = false;

    /// <summary>
    /// 自風
    /// </summary>
    public Wind PlayerWind { get; init; } = Wind.East;

    /// <summary>
    /// 場風
    /// </summary>
    public Wind RoundWind { get; init; } = Wind.East;

    /// <summary>
    /// プレイヤーが親かどうか
    /// </summary>
    public bool IsDealer => PlayerWind == Wind.East;
}

internal enum Wind
{
    East,
    South,
    West,
    North,
}