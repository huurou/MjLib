namespace MjLib.HandCalculating.Yakus;

internal abstract class Yaku : ValueObject<Yaku>
{
    // 状況による役
    public static Riichi Riichi { get; }
    public static DaburuRiichi DaburuRiichi { get; }
    public static Tsumo Tsumo { get; }
    public static Ippatsu Ippatsu { get; }
    public static Chankan Chankan { get; }
    public static Rinshan Rinshan { get; }
    public static Haitei Haitei { get; }
    public static Houtei Houtei { get; }
    public static Nagashimangan Nagashimangan { get; }
    public static Renhou Renhou { get; }

    // 1翻
    public static Pinfu Pinfu { get; }
    public static Tanyao Tanyao { get; }
    public static Iipeikou Iipeikou { get; }
    public static Haku Haku { get; }
    public static Hatsu Hatsu { get; }
    public static Chun Chun { get; }
    public static YakuhaiOfPlayer YakuhaiOfPlayer { get; }
    public static YakuhaiOfRound YakuhaiOfRound { get; }

    // 2翻
    public static Sanshoku Sanshoku { get; }
    public static Ittsu Ittsu { get; }
    public static Chanta Chanta { get; }
    public static Honrouto Honroto { get; }
    public static Toitoihou Toitoihou { get; }
    public static Sanankou Sanankou { get; }
    public static Sankantsu Sankantsu { get; }
    public static Sanshokudoukou Sanshokudoukou { get; }
    public static Chiitoitsu Chiitoitsu { get; }
    public static Shousangen Shousangen { get; }

    // 3翻
    public static Honitsu Honitsu { get; }
    public static Junchan Junchan { get; }
    public static Ryanpeikou Ryanpeikou { get; }

    // 6翻
    public static Chinitsu Chinitsu { get; }

    // 役満
    public static Kokushimusou Kokushimusou { get; }
    public static Chuurenpoutou Chuurenpoutou { get; }
    public static Suuankou Suuankou { get; }
    public static Daisangen Daisangen { get; }
    public static Shousuushii Shousuushii { get; }
    public static Ryuuiisou Ryuuiisou { get; }
    public static Suukantsu Suukantsu { get; }
    public static Tsuuiisou Tsuuiisou { get; }
    public static Chinroutou Chinroutou { get; }
    public static Daisharin Daisharin { get; }

    // ダブル役満
    public static Daisuushi Daisuushi { get; }
    public static Kokushimusou13 Kokushimusou13 { get; }
    public static SuuankouTanki SuuankouTanki { get; }
    public static JunseiChuurenpoutou JunseiChuurenpoutou { get; }

    // 状況による役満
    public static Tenhou Tenhou { get; }
    public static Chiihou Chiihou { get; }
    public static RenhouYakuman RenhouYakuman { get; }

    // ドラ
    public static Dora Dora { get; }
    public static Akadora Akadora { get; }

    static Yaku()
    {
        var id = 0;
        Riichi = new(id++);
        DaburuRiichi = new(id++);
        Tsumo = new(id++);
        Ippatsu = new(id++);
        Chankan = new(id++);
        Rinshan = new(id++);
        Haitei = new(id++);
        Houtei = new(id++);
        Nagashimangan = new(id++);
        Renhou = new(id++);
        Pinfu = new(id++);
        Tanyao = new(id++);
        Iipeikou = new(id++);
        Haku = new(id++);
        Hatsu = new(id++);
        Chun = new(id++);
        YakuhaiOfPlayer = new(id++);
        YakuhaiOfRound = new(id++);
        Sanshoku = new(id++);
        Ittsu = new(id++);
        Chanta = new(id++);
        Honroto = new(id++);
        Toitoihou = new(id++);
        Sanankou = new(id++);
        Sankantsu = new(id++);
        Sanshokudoukou = new(id++);
        Chiitoitsu = new(id++);
        Shousangen = new(id++);
        Honitsu = new(id++);
        Junchan = new(id++);
        Ryanpeikou = new(id++);
        Chinitsu = new(id++);
        Kokushimusou = new(id++);
        Chuurenpoutou = new(id++);
        Suuankou = new(id++);
        Daisangen = new(id++);
        Shousuushii = new(id++);
        Ryuuiisou = new(id++);
        Suukantsu = new(id++);
        Tsuuiisou = new(id++);
        Chinroutou = new(id++);
        Daisharin = new(id++);
        Daisuushi = new(id++);
        Kokushimusou13 = new(id++);
        SuuankouTanki = new(id++);
        JunseiChuurenpoutou = new(id++);
        Tenhou = new(id++);
        Chiihou = new(id++);
        RenhouYakuman = new(id++);
        Dora = new(id++);
        Akadora = new(id++);
    }

    public int Id { get; }
    public abstract string Name { get; }
    public abstract int HanOpen { get; }
    public abstract int HanClosed { get; }
    public abstract bool IsYakuman { get; }

    protected Yaku(int id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return Name;
    }

    #region ValueObject<T>の実装

    public override bool EqualsCore(ValueObject<Yaku>? other)
    {
        return other is Yaku x && x.Id == Id;
    }

    public override int GetHashCodeCore()
    {
        return new { Id }.GetHashCode();
    }

    #endregion ValueObject<T>の実装
}