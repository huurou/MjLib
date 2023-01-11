using MjLib.TileKinds;
using System.Collections;
using static MjLib.TileKinds.TileKind;

namespace MjLib.TileCountArrays;

/// <summary>
/// 牌の種類id0 ~33をインデックスとして, それぞれの牌の個数を値として持つ配列
/// </summary>
internal class TileCountArray : IEnumerable<int>, IEnumerable
{
    private readonly int[] array_ = new int[KIND_COUNT];

    public int this[int index]
    {
        get => array_[index];
        set
        {
            if (value is < 0 or > 4) throw new ArgumentException($"牌は0～4枚です。 given:{value}", nameof(value));
            array_[index] = value;
        }
    }
    public int this[TileKind kind] { get => this[kind.Id]; set => this[kind.Id] = value; }
    public int[] this[Range range] => array_[range];

    public TileCountArray()
    { }

    public TileCountArray(IEnumerable<int> counts)
    {
        if (array_.Length != counts.Count()) throw new ArgumentException($"牌種類数と配列の長さが一致しません。given:{counts}", nameof(counts));
        array_ = counts.ToArray();
    }

    public TileCountArray(IEnumerable<TileKind> kinds)
    {
        foreach (var kind in kinds)
        {
            this[kind]++;
        }
    }

    public TileKindList GetIsolations()
    {
        var isolatations = new TileKindList();
        foreach (var kind in AllKind)
        {
            if (kind.IsHonor && this[kind] == 0 ||
                !kind.IsHonor && kind.Number == 1 && this[kind] == 0 && this[kind.Id + 1] == 0 ||
                !kind.IsHonor && kind.Number is >= 2 and <= 8 && this[kind.Id - 1] == 0 && this[kind] == 0 && this[kind.Id + 1] == 0 ||
                !kind.IsHonor && kind.Number == 9 && this[kind.Id - 1] == 0 && this[kind] == 0)
            {
                isolatations.Add(kind);
            }
        }
        return isolatations;
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