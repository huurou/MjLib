using MjLib.TileKinds;
using System.Collections;
using static MjLib.TileKinds.TileKind;

namespace MjLib.TileCountArrays;

/// <summary>
/// 牌の種類id0 ~33をインデックスとして, それぞれの牌の個数を値として持つ配列
/// </summary>
internal class TileCountArray : IEnumerable<int>, IEnumerable
{
    private readonly int[] array_ = new int[ID_MAX + 1];

    public int this[int index] { get => array_[index]; set => array_[index] = value; }
    public int this[TileKind kind] { get => this[kind.Id]; set => this[kind.Id] = value; }

    public TileCountArray()
    { }

    public TileCountArray(int[] counts)
    {
        if (counts.Length != array_.Length) throw new ArgumentException("牌種類数と配列の長さが一致しません。", nameof(counts));
        array_ = counts;
    }

    public TileCountArray(IEnumerable<TileKind> kinds)
    {
        foreach (var kind in kinds)
        {
            array_[kind.Id]++;
        }
    }

    public TileKindList ToTileKindList()
    {
        return new(this);
    }

    public override string ToString()
    {
        var s = string.Join("", array_);
        return $"m[{s.Substring(Man1.Id, 9)}] p[{s.Substring(Pin1.Id, 9)}] s[{s.Substring(Sou1.Id, 9)}] z[{s.Substring(Ton.Id, 7)}]";
    }

    #region IEnumerable<T>、IEnumerableの実装

    public IEnumerator<int> GetEnumerator()
    {
        return ((IEnumerable<int>)array_).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return array_.GetEnumerator();
    }

    #endregion IEnumerable<T>、IEnumerableの実装
}