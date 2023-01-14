using MjLib.Fuuros;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.TileKinds;
using System.Diagnostics.CodeAnalysis;
using static MjLib.TileKinds.TileKind;

namespace MjLib.HandCalculating.Yakus;

internal abstract class Yaku : ValueObject<Yaku>
{
    public static Akadora Akadora { get; } = new();
    public static Chankan Chankan { get; } = new();
    public static Chanta Chanta { get; } = new();
    public static Chiihou Chiihou { get; } = new();
    public static Chiitoitsu Chiitoitsu { get; } = new();
    public static Chinitsu Chinitsu { get; } = new();
    public static Chinroutou Chinroutou { get; } = new();
    public static Chun Chun { get; } = new();
    public static Chuurenpoutou Chuurenpoutou { get; } = new();
    public static DaburuRiichi DaburuRiichi { get; } = new();
    public static Daisangen Daisangen { get; } = new();
    public static Daisharin Daisharin { get; } = new();
    public static Daisuushi Daisuushi { get; } = new();
    public static Dora Dora { get; } = new();
    public static Haitei Haitei { get; } = new();
    public static Haku Haku { get; } = new();
    public static Hatsu Hatsu { get; } = new();
    public static Honitsu Honitsu { get; } = new();
    public static Honroto Honroto { get; } = new();
    public static Houtei Houtei { get; } = new();
    public static Iipeiko Iipeiko { get; } = new();
    public static Ippatsu Ippatsu { get; } = new();
    public static Ittsu Ittsu { get; } = new();
    public static Junchan Junchan { get; } = new();
    public static JunseiChuurenpoutou JunseiChuurenpoutou { get; } = new();
    public static Kokushimusou Kokushimusou { get; } = new();
    public static Kokushimusou13menmachi Kokushimusou13menmachi { get; } = new();
    public static Nagashimangan Nagashimangan { get; } = new();
    public static Pinfu Pinfu { get; } = new();
    public static Renhou Renhou { get; } = new();
    public static RenhouYakuman RenhouYakuman { get; } = new();
    public static Riichi Riichi { get; } = new();
    public static Rinshan Rinshan { get; } = new();
    public static Ryanpeikou Ryanpeikou { get; } = new();
    public static Ryuuiisou Ryuuiisou { get; } = new();
    public static Sanankou Sanankou { get; } = new();
    public static Sankantsu Sankantsu { get; } = new();
    public static Sanshoku Sanshoku { get; } = new();
    public static Sanshokudoukou Sanshokudoukou { get; } = new();
    public static Shousangen Shousangen { get; } = new();
    public static Shousuushii Shousuushii { get; } = new();
    public static Suuankou Suuankou { get; } = new();
    public static SuuankouTanki SuuankouTanki { get; } = new();
    public static Suukantsu Suukantsu { get; } = new();
    public static Tanyao Tanyao { get; } = new();
    public static Tenhou Tenhou { get; } = new();
    public static Toitoihou Toitoihou { get; } = new();
    public static Tsumo Tsumo { get; } = new();
    public static Tsuuiisou Tsuuiisou { get; } = new();
    public static YakuhaiOfPlayer YakuhaiOfPlayer { get; } = new();
    public static YakuhaiOfRound YakuhaiOfRound { get; } = new();

    public abstract int Id { get; }
    public abstract string Name { get; }
    public abstract int HanOpen { get; }
    public abstract int HanClosed { get; }
    public abstract bool IsYakuman { get; }

    public override string ToString()
    {
        return Name;
    }

    #region ValueObject<T>の実装

    public override bool EqualsCore(ValueObject<Yaku>? other)
    {
        return other is Yaku x &&
            x.Id == Id &&
            x.Name == Name &&
            x.HanOpen == HanOpen &&
            x.HanClosed == HanClosed &&
            x.IsYakuman == IsYakuman;
    }

    public override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }

    #endregion ValueObject<T>の実装
}

internal class Akadora : Yaku
{
    public override int Id => 54;
    public override string Name => "赤ドラ";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class Chankan : Yaku
{
    public override int Id => 3;
    public override string Name => "槍槓";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsChankan;
    }
}

internal class Chanta : Yaku
{
    public override int Id => 24;
    public override string Name => "混全帯幺九";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var shuntus = hand.Count(x => x.IsShuntsu);
        var routou = hand.Count(x => x.Any(y => y.IsRoutou));
        var honor = hand.Count(x => x[0].IsHonor);
        return shuntus != 0 &&
            routou + honor == 5 &&
            routou != 0 && honor != 0;
    }
}

internal class Chiihou : Yaku
{
    public override int Id => 51;
    public override string Name => "地和";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(HandConfig config)
    {
        return config.IsChiihou;
    }
}

internal class Chiitoitsu : Yaku
{
    public override int Id => 30;
    public override string Name => "七対子";
    public override int HanOpen => 0;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        return hand.Count == 7 && hand.All(x => x.IsToitsu);
    }
}

internal class Chinitsu : Yaku
{
    public override int Id => 35;
    public override string Name => "清一色";
    public override int HanOpen => 5;
    public override int HanClosed => 6;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var man = hand.Count(x => x[0].IsMan);
        var pin = hand.Count(x => x[0].IsPin);
        var sou = hand.Count(x => x[0].IsSou);
        var honor = hand.Count(x => x[0].IsHonor);
        return new[] { man, pin, sou }.Count(x => x != 0) == 1 && honor == 0;
    }
}

internal class Chinroutou : Yaku
{
    public override int Id => 44;
    public override string Name => "清老頭";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand)
    {
        return hand.SelectMany(x => x).All(x => x.IsRoutou);
    }
}

internal class Chun : Yaku
{
    public override int Id => 15;
    public override string Name => "中";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class Chuurenpoutou : Yaku
{
    public override int Id => 37;
    public override string Name => "九蓮宝燈";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}

internal class JunseiChuurenpoutou : Yaku
{
    public override int Id => 49;
    public override string Name => "純正九蓮宝燈";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}

internal class Kokushimusou13menmachi : Yaku
{
    public override int Id => 47;
    public override string Name => "国士無双十三面待ち";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}

internal class DaburuRiichi : Yaku
{
    public override int Id => 7;
    public override string Name => "ダブル立直";
    public override int HanOpen => 0;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}

internal class Daisangen : Yaku
{
    public override int Id => 39;
    public override string Name => "大三元";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}

internal class Daisharin : Yaku
{
    public override int Id => 45;
    public override string Name => "大車輪"; public override int HanOpen => 0; public override int HanClosed => 13; public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, OptionalRules rules)
    {
        return hand.Equals(new TileKindListList
            {
                new () { Pin2, Pin2 },
                new () { Pin3, Pin3 },
                new () { Pin4, Pin4 },
                new () { Pin5, Pin5 },
                new () { Pin6, Pin6 },
                new () { Pin7, Pin7 },
                new () { Pin8, Pin8 },
            }) && rules.HasDaisharin;
    }
}

internal class Daisuushi : Yaku
{
    public override int Id => 46;
    public override string Name => "大四喜";
    public override int HanOpen => 26;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}

internal class Dora : Yaku
{
    public override int Id => 53;
    public override string Name => "ドラ";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class Haitei : Yaku
{
    public override int Id => 5;
    public override string Name => "海底撈月";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsHaitei;
    }
}

internal class Haku : Yaku
{
    public override int Id => 13;
    public override string Name => "白";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class Hatsu : Yaku
{
    public override int Id => 14;
    public override string Name => "發";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class Honitsu : Yaku
{
    public override int Id => 32;
    public override string Name => "混一色";
    public override int HanOpen => 2;
    public override int HanClosed => 3;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var man = hand.Count(x => x[0].IsMan);
        var pin = hand.Count(x => x[0].IsPin);
        var sou = hand.Count(x => x[0].IsSou);
        var honor = hand.Count(x => x[0].IsHonor);
        return new[] { man, pin, sou }.Count(x => x != 0) == 1 && honor != 0;
    }
}

internal class Honroto : Yaku
{
    public override int Id => 25;
    public override string Name => "混老頭";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        return hand.SelectMany(x => x).All(x => x.IsYaochuu);
    }
}

internal class Houtei : Yaku
{
    public override int Id => 6;
    public override string Name => "河底撈魚";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsHoutei;
    }
}

internal class Iipeiko : Yaku
{
    public override int Id => 12;
    public override string Name => "一盃口";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        if (!fuuroList.HasOpen) return false;
        var shuntsus = hand.Where(x => x.IsShuntsu);
        if (!shuntsus.Any()) return false;
        var count = shuntsus.Max(x => shuntsus.Count(x.Equals));
        return count >= 2;
    }
}

internal class Ippatsu : Yaku
{
    public override int Id => 2;
    public override string Name => "一発";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsIppatsu;
    }
}

internal class Ittsu : Yaku
{
    public override int Id => 23;
    public override string Name => "一気通貫";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var shuntsus = hand.Where(x => x.IsShuntsu);
        if (shuntsus.Count() < 3) return false;
        var suits = new[]
        {
            shuntsus.Where(x=>x[0].IsMan),
            shuntsus.Where(x=>x[0].IsPin),
            shuntsus.Where(x=>x[0].IsSou),
        };
        foreach (var suit in suits)
        {
            if (suit.Count() < 3) continue;
            var casted = suit.Select(x => x.Select(y => y.Number));
            return casted.Contains(new[] { 1, 2, 3 }, IntEnumerableEqualityComparer.Singleton)
                && casted.Contains(new[] { 4, 5, 6 }, IntEnumerableEqualityComparer.Singleton)
                && casted.Contains(new[] { 7, 8, 9 }, IntEnumerableEqualityComparer.Singleton);
        }
        return false;
    }

    private class IntEnumerableEqualityComparer : IEqualityComparer<IEnumerable<int>>
    {
        public static IntEnumerableEqualityComparer Singleton { get; } = new();

        private IntEnumerableEqualityComparer()
        { }

        public bool Equals(IEnumerable<int>? x, IEnumerable<int>? y)
        {
            return (x is null && y is null) ||
                (x is not null && y is not null && x.SequenceEqual(y));
        }

        public int GetHashCode([DisallowNull] IEnumerable<int> obj)
        {
            return obj.GetHashCode();
        }
    }
}

internal class Junchan : Yaku
{
    public override int Id => 33;
    public override string Name => "純全帯么九";
    public override int HanOpen => 2;
    public override int HanClosed => 3;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var shuntsu = hand.Count(x => x.IsShuntsu);
        var routou = hand.Count(x => x.Any(x => x.IsRoutou));
        return shuntsu != 0 && routou == 5;
    }
}

internal class Kokushimusou : Yaku
{
    public override int Id => 36;
    public override string Name => "国士無双";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}

internal class Nagashimangan : Yaku
{
    public override int Id => 8;
    public override string Name => "流し満貫";
    public override int HanOpen => 5;
    public override int HanClosed => 5;
    public override bool IsYakuman => false;
}

internal class Pinfu : Yaku
{
    public override int Id => 10;
    public override string Name => "平和"; public override int HanOpen => 0; public override int HanClosed => 1; public override bool IsYakuman => false;

    public static bool Valid(FuList fuList, FuuroList fuuroList)
    {
        return (fuList.Contains(Fu.Base) && fuList.Count == 1 ||
            fuList.Contains(Fu.Base) && fuList.Contains(Fu.Menzen) && fuList.Count == 2) &&
            !fuuroList.HasOpen;
    }
}

internal class Renhou : Yaku
{
    public override int Id => 9;
    public override string Name => "人和";
    public override int HanOpen => 0;
    public override int HanClosed => 5;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config, OptionalRules rules)
    {
        return config.IsRenhou && !rules.RenhouAsYakuman;
    }
}

internal class RenhouYakuman : Yaku
{
    public override int Id => 52;
    public override string Name => "人和";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(HandConfig config, OptionalRules rules)
    {
        return config.IsRenhou && rules.RenhouAsYakuman;
    }
}

internal class Riichi : Yaku
{
    public override int Id => 1;
    public override string Name => "立直";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class Rinshan : Yaku
{
    public override int Id => 4;
    public override string Name => "嶺上開花";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config)
    {
        return config.IsRinshan;
    }
}

internal class Ryanpeikou : Yaku
{
    public override int Id => 34;
    public override string Name => "二盃口";
    public override int HanOpen => 0;
    public override int HanClosed => 3;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList)
    {
        if (!fuuroList.HasOpen) return false;
        var shuntsus = hand.Where(x => x.IsShuntsu);
        var counts = shuntsus.Select(x => shuntsus.Count(x.Equals));
        return counts.Count(x => x >= 2) == 4;
    }
}

internal class Ryuuiisou : Yaku
{
    public override int Id => 41;
    public override string Name => "緑一色";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand)
    {
        var greens = new[] { Sou2, Sou3, Sou4, Sou6, Sou8, TileKind.Hatsu };
        return hand.SelectMany(x => x).Distinct().All(x => greens.Contains(x));
    }
}

internal class Sanankou : Yaku
{
    public override int Id => 27;
    public override string Name => "三暗刻";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}

internal class Sankantsu : Yaku
{
    public override int Id => 28;
    public override string Name => "三槓子";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}

internal class Sanshoku : Yaku
{
    public override int Id => 22;
    public override string Name => "三色同順";
    public override int HanOpen => 1;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand)
    {
        var shuntsus = hand.Where(x => x.IsShuntsu);
        if (shuntsus.Count() < 3) return false;
        var mans = shuntsus.Where(x => x[0].IsMan);
        var pins = shuntsus.Where(x => x[0].IsPin);
        var sous = shuntsus.Where(x => x[0].IsSou);
        foreach (var man in mans)
        {
            foreach (var pin in pins)
            {
                foreach (var sou in sous)
                {
                    var manNum = man.Select(x => x.Number);
                    var pinNum = pin.Select(x => x.Number);
                    var souNum = sou.Select(x => x.Number);
                    if (manNum.SequenceEqual(pinNum) &&
                        pinNum.SequenceEqual(souNum) &&
                        souNum.SequenceEqual(manNum))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}

internal class Sanshokudoukou : Yaku
{
    public override int Id => 29;
    public override string Name => "三色同刻";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}

internal class Shousangen : Yaku
{
    public override int Id => 31;
    public override string Name => "小三元";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}

internal class Shousuushii : Yaku
{
    public override int Id => 40;
    public override string Name => "小四喜";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}

internal class Suuankou : Yaku
{
    public override int Id => 38;
    public override string Name => "四暗刻";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}

internal class SuuankouTanki : Yaku
{
    public override int Id => 40;
    public override string Name => "四暗刻単騎";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;
}

internal class Suukantsu : Yaku
{
    public override int Id => 42;
    public override string Name => "四槓子";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;
}

internal class Tanyao : Yaku
{
    public override int Id => 11;
    public override string Name => "断么九";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(TileKindListList hand, FuuroList fuuroList, OptionalRules rules)
    {
        return hand.SelectMany(x => x).All(x => x.IsChuuchan) &&
            (!fuuroList.HasOpen || rules.HasOpenTanyao);
    }
}

internal class Tenhou : Yaku
{
    public override int Id => 50;
    public override string Name => "天和";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(HandConfig config)
    {
        return config.IsTenhou;
    }
}

internal class Toitoihou : Yaku
{
    public override int Id => 26;
    public override string Name => "対々和";
    public override int HanOpen => 2;
    public override int HanClosed => 2;
    public override bool IsYakuman => false;
}

internal class Tsumo : Yaku
{
    public override int Id => 0;
    public override string Name => "門前清自摸和";
    public override int HanOpen => 0;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;

    public static bool Valid(HandConfig config, FuuroList fuuroList)
    {
        return config.IsTsumo && !fuuroList.HasOpen;
    }
}

internal class Tsuuiisou : Yaku
{
    public override int Id => 43;
    public override string Name => "字一色";
    public override int HanOpen => 13;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand)
    {
        return hand.SelectMany(x => x).All(x => x.IsHonor);
    }
}

internal class YakuhaiOfPlayer : Yaku
{
    public override int Id => 20;
    public override string Name => "自風牌";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}

internal class YakuhaiOfRound : Yaku
{
    public override int Id => 21;
    public override string Name => "場風牌";
    public override int HanOpen => 1;
    public override int HanClosed => 1;
    public override bool IsYakuman => false;
}