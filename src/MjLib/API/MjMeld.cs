using MjLib.Fuuros;

namespace MjLib.API;

/// <summary>
/// 副露
/// </summary>
/// <param name="Type">副露の種類></param>
/// <param name="Tiles">副露された牌</param>
public record MjMeld(MjMeldType Type, MjTiles Tiles)
{
    internal Fuuro ToInternalModel()
    {
        return new Fuuro((FuuroType)Type, Tiles.KindList);
    }
}

/// <summary>
/// 副露の種類
/// </summary>
public enum MjMeldType
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
    /// 明槓
    /// </summary>
    Minkan,
    /// <summary>
    /// 抜き
    /// </summary>
    Nuki,
}