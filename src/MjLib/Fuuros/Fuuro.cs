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
    public TileKindList Tiles { get; }

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
    public bool IsKan => Type is FuuroType.Ankan or FuuroType.Daiminkan or FuuroType.Shouminkan;
    /// <summary>
    /// 暗槓かどうか
    /// </summary>
    public bool IsAnkan => Type is FuuroType.Ankan;
    /// <summary>
    /// 明槓かどうか
    /// </summary>
    public bool IsMinkan => Type is FuuroType.Daiminkan or FuuroType.Shouminkan;
    /// <summary>
    /// 抜きかどうか
    /// </summary>
    public bool IsNuki => Type is FuuroType.Nuki;
    /// <summary>
    /// 面前が崩れる副露かどうか
    /// </summary>
    public bool IsOpen => Type is FuuroType.Chi or FuuroType.Pon or FuuroType.Shouminkan or FuuroType.Daiminkan;

    public Fuuro(FuuroType type, TileKindList tiles)
    {
        Type = type;
        Tiles = tiles;
    }

    public override string ToString()
    {
        var typeStr = Type switch
        {
            FuuroType.Chi => "チー",
            FuuroType.Pon => "ポン",
            FuuroType.Ankan => "暗槓",
            FuuroType.Daiminkan => "大明槓",
            FuuroType.Shouminkan => "小明槓",
            FuuroType.Nuki => "抜き",
            _ => "",
        };
        return $"{typeStr} {Tiles}";
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
    /// 大明槓
    /// </summary>
    Daiminkan,
    /// <summary>
    /// 小明槓
    /// </summary>
    Shouminkan,
    /// <summary>
    /// 抜き
    /// </summary>
    Nuki,
}