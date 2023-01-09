using MjLib.TileKinds;

namespace MjLib.Huuros;

/// <summary>
/// 副露
/// </summary>
internal class Huuro
{
    /// <summary>
    /// 種別
    /// </summary>
    public HuuroType Type { get; }
    /// <summary>
    /// 副露牌
    /// </summary>
    public TileKindList Tiles { get; }
    /// <summary>
    /// 鳴かれた牌
    /// </summary>
    public TileKind? CalledTile { get; }
    /// <summary>
    /// 誰から鳴いたか
    /// </summary>
    public HuuroFromWho FromWho { get; }

    /// <summary>
    /// チーかどうか
    /// </summary>
    public bool IsChi => Type == HuuroType.Chi;
    /// <summary>
    /// ポンかどうか
    /// </summary>
    public bool IsPon => Type == HuuroType.Pon;
    /// <summary>
    /// 槓かどうか
    /// </summary>
    public bool IsKan => Type is HuuroType.Ankan or HuuroType.Daiminkan or HuuroType.Shouminkan;
    /// <summary>
    /// 抜きかどうか
    /// </summary>
    public bool IsNuki => Type is HuuroType.Nuki;
    /// <summary>
    /// 面前が崩れる副露かどうか
    /// </summary>
    public bool IsOpen => Type is HuuroType.Chi or HuuroType.Pon or HuuroType.Shouminkan or HuuroType.Daiminkan;

    public Huuro(HuuroType type, TileKindList tiles, TileKind? calledTile, HuuroFromWho fromWho, bool open = true)
    {
        Type = type;
        Tiles = tiles;
        CalledTile = calledTile;
        FromWho = fromWho;
    }

    public override string ToString()
    {
        var typeStr = Type switch
        {
            HuuroType.Chi => "チー",
            HuuroType.Pon => "ポン",
            HuuroType.Ankan => "暗槓",
            HuuroType.Daiminkan => "大明槓",
            HuuroType.Shouminkan => "小明槓",
            HuuroType.Nuki => "抜き",
            _ => "",
        };
        var fromWhoStr = FromWho switch
        {
            HuuroFromWho.None => "なし",
            HuuroFromWho.Right => "下家",
            HuuroFromWho.Front => "対面",
            HuuroFromWho.Left => "上家",
            _ => ""
        };
        return $"{typeStr} {Tiles} {CalledTile} {fromWhoStr}";
    }
}

/// <summary>
/// 副露種別
/// </summary>
internal enum HuuroType
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

/// <summary>
/// 誰から鳴いたか
/// </summary>
internal enum HuuroFromWho
{
    /// <summary>
    /// なし　抜き、暗槓
    /// </summary>
    None,
    /// <summary>
    /// 下家
    /// </summary>
    Right,
    /// <summary>
    /// 対面
    /// </summary>
    Front,
    /// <summary>
    /// 上家
    /// </summary>
    Left,
}