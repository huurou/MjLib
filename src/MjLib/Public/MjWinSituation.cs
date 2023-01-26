using MjLib.HandCalculating;

namespace MjLib;

public class MjWinSituation
{
    /// <summary>
    /// ツモアガリかどうか デフォルト:false
    /// </summary>
    public bool Tsumo { get; init; } = false;

    /// <summary>
    /// リーチ デフォルト:false
    /// </summary>
    public bool Riichi { get; init; } = false;

    /// <summary>
    /// 一発 デフォルト:false
    /// </summary>
    public bool Ippatsu { get; init; } = false;

    /// <summary>
    /// 嶺上開花 デフォルト:false
    /// </summary>
    public bool Rinshan { get; init; } = false;

    /// <summary>
    /// 槍槓 デフォルト:false
    /// </summary>
    public bool Chankan { get; init; } = false;

    /// <summary>
    /// 海底撈月 デフォルト:false
    /// </summary>
    public bool Haitei { get; init; } = false;

    /// <summary>
    /// 河底撈魚 デフォルト:false
    /// </summary>
    public bool Houtei { get; init; } = false;

    /// <summary>
    /// ダブル立直 デフォルト:false
    /// </summary>
    public bool DaburuRiichi { get; init; } = false;

    /// <summary>
    /// 流し満貫 デフォルト:false
    /// </summary>
    public bool Nagashimangan { get; init; } = false;

    /// <summary>
    /// 天和 デフォルト:false
    /// </summary>
    public bool Tenhou { get; init; } = false;

    /// <summary>
    /// 地和 デフォルト:false
    /// </summary>
    public bool Chiihou { get; init; } = false;

    /// <summary>
    /// 人和 デフォルト:false
    /// </summary>
    public bool Renhou { get; init; } = false;

    /// <summary>
    /// 自風 デフォルト:East
    /// </summary>
    public MjWind Player { get; init; } = MjWind.East;

    /// <summary>
    /// 場風 デフォルト:East
    /// </summary>
    public MjWind Round { get; init; } = MjWind.East;

    /// <summary>
    /// 赤ドラの枚数 デフォルト:0
    /// </summary>
    public int Akadora { get; init; } = 0;

    internal WinSituation ToInternalModel()
    {
        return new()
        {
            Tsumo = Tsumo,
            Riichi = Riichi,
            Ippatsu = Ippatsu,
            Rinshan = Rinshan,
            Chankan = Chankan,
            Haitei = Haitei,
            Houtei = Houtei,
            DaburuRiichi = DaburuRiichi,
            Nagashimangan = Nagashimangan,
            Tenhou = Tenhou,
            Chiihou = Chiihou,
            Renhou = Renhou,
            Player = (Wind)Player,
            Round = (Wind)Round,
            Akadora = Akadora,
        };
    }
}

public enum MjWind
{
    East,
    South,
    West,
    North,
}