using MjLib.TileKinds;

namespace MjLib.Fuuros;

/// <summary>
/// 副露
/// </summary>
internal class Fuuro
{
    /// <summary>
    /// 種別
    /// </summary>
    public FuuroType Type { get; }
    /// <summary>
    /// 副露
    /// </summary>
    public TileKindList KindList { get; }

    /// <summary>
    /// チーかどうか
    /// </summary>
    public bool IsChi => Type == FuuroType.Chi;
    /// <summary>
    /// ポンかどうか
    /// </summary>
    public bool IsPon => Type == FuuroType.Pon;
    /// <summary>
    /// 槓かどうか
    /// </summary>
    public bool IsKan => Type is FuuroType.Ankan or FuuroType.Minkan;
    /// <summary>
    /// 暗槓かどうか
    /// </summary>
    public bool IsAnkan => Type is FuuroType.Ankan;
    /// <summary>
    /// 明槓かどうか
    /// </summary>
    public bool IsMinkan => Type is FuuroType.Minkan;
    /// <summary>
    /// 抜きかどうか
    /// </summary>
    public bool IsNuki => Type is FuuroType.Nuki;
    /// <summary>
    /// 面前が崩れる副露かどうか
    /// </summary>
    public bool IsOpen => Type is FuuroType.Chi or FuuroType.Pon or FuuroType.Minkan;

    public Fuuro(FuuroType type, TileKindList kindList)
    {
        Type = type;
        KindList = kindList;
    }

    public override string ToString()
    {
        var typeStr = Type switch
        {
            FuuroType.Chi => "チー",
            FuuroType.Pon => "ポン",
            FuuroType.Ankan => "暗槓",
            FuuroType.Minkan => "明槓",
            FuuroType.Nuki => "抜き",
            _ => "",
        };
        return $"{typeStr} {KindList}";
    }
}

/// <summary>
/// 副露種別
/// </summary>
internal enum FuuroType
{
    /// <summary>
    /// チー
    /// </summary>
    Chi,
    /// <summary>
    /// ポン
    /// </summary>
    Pon,
    /// <summary>
    /// 暗槓
    /// </summary>
    Ankan,
    /// <summary>
    /// 明槓 大明槓または小明槓
    /// </summary>
    Minkan,
    /// <summary>
    /// <summary>
    /// 抜き
    /// </summary>
    Nuki,
}