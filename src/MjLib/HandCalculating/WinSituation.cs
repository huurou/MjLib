namespace MjLib.HandCalculating;

/// <summary>
/// 和了した時の状況
/// </summary>
internal class WinSituation
{
    /// <summary>
    /// ツモアガリかどうか
    /// </summary>
    public bool Tsumo { get; init; } = false;

    /// <summary>
    /// リーチ
    /// </summary>
    public bool Riichi { get; init; } = false;

    /// <summary>
    /// 一発
    /// </summary>
    public bool Ippatsu { get; init; } = false;

    /// <summary>
    /// 嶺上開花
    /// </summary>
    public bool Rinshan { get; init; } = false;

    /// <summary>
    /// 槍槓
    /// </summary>
    public bool Chankan { get; init; } = false;

    /// <summary>
    /// 海底撈月
    /// </summary>
    public bool Haitei { get; init; } = false;

    /// <summary>
    /// 河底撈魚
    /// </summary>
    public bool Houtei { get; init; } = false;

    /// <summary>
    /// ダブル立直
    /// </summary>
    public bool DaburuRiichi { get; init; } = false;

    /// <summary>
    /// 流し満貫
    /// </summary>
    public bool Nagashimangan { get; init; } = false;

    /// <summary>
    /// 天和
    /// </summary>
    public bool Tenhou { get; init; } = false;

    /// <summary>
    /// 地和
    /// </summary>
    public bool Chiihou { get; init; } = false;

    /// <summary>
    /// 人和
    /// </summary>
    public bool Renhou { get; init; } = false;

    /// <summary>
    /// 自風
    /// </summary>
    public Wind Player { get; init; } = Wind.East;

    /// <summary>
    /// 場風
    /// </summary>
    public Wind Round { get; init; } = Wind.East;

    /// <summary>
    /// 赤ドラの枚数
    /// </summary>
    public int Akadora { get; init; } = 0;

    /// <summary>
    /// プレイヤーが親かどうか
    /// </summary>
    public bool IsDealer => Player == Wind.East;
}

internal enum Wind
{
    East,
    South,
    West,
    North,
}