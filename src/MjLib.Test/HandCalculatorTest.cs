using MjLib.Fuuros;
using MjLib.HandCalculating;
using MjLib.HandCalculating.Hands;
using MjLib.HandCalculating.Yakus;
using MjLib.TileKinds;
using static MjLib.Fuuros.FuuroType;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class HandCalculatorTest
{
    private TileKindList? hand_;
    private TileKind? winTile_;
    private FuuroList? fuuroList_;
    private TileKindList? doraIndicators_;
    private TileKindList? uradoraIndicators_;
    private WinSituation? situation_;
    private GameRules? rules_;
    private HandResult? actual_;
    private YakuList? expected_;

    [SetUp]
    public void Setup()
    {
        hand_ = null;
        winTile_ = null;
        fuuroList_ = null;
        doraIndicators_ = null;
        uradoraIndicators_ = null;
        situation_ = null;
        rules_ = null;
        actual_ = null;
        expected_ = null;
    }

    private HandResult Calc()
    {
        return hand_ is not null
            ? HandCalculator.Calculate(hand_, winTile_, fuuroList_, doraIndicators_, uradoraIndicators_, situation_, rules_)
            : throw new ArgumentNullException();
    }

    [Test]
    public void InvalidHandTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123445");
        winTile_ = Sou4;
        actual_ = Calc();
        Assert.That(actual_, Is.Not.Null);

        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Pin4;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    #region 状況による役

    [Test]
    public void RiichiTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Riichi = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Riichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "234456", pin: "66", sou: "444");
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void DaburuRiichiTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { DaburuRiichi = true };
        actual_ = Calc();
        expected_ = new() { Yaku.DaburuRiichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "234456", pin: "66", sou: "444");
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void TsumoTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Tsumo = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Tsumo };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "234456", pin: "66", sou: "444");
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void IppatsuTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Riichi = true, Ippatsu = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Riichi, Yaku.Ippatsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "234456", pin: "66", sou: "444");
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        situation_ = new() { Riichi = false, Ippatsu = true };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void RinshanTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123");
        winTile_ = Sou1;
        fuuroList_ = new() { new(Ankan, new(sou: "4444")) };
        situation_ = new() { Tsumo = true, Rinshan = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Tsumo, Yaku.Rinshan };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        fuuroList_ = new() { new(Minkan, new(sou: "4444")) };
        situation_ = new() { Tsumo = true, Rinshan = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Rinshan };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        situation_ = new() { Rinshan = true };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void ChankanTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Chankan = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Chankan };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        situation_ = new() { Chankan = true, Tsumo = true };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void HaiteiTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Haitei = true, Tsumo = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Tsumo, Yaku.Haitei };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        situation_ = new() { Haitei = true, Tsumo = false };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void HouteiTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Houtei = true, Tsumo = false };
        actual_ = Calc();
        expected_ = new() { Yaku.Houtei };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        situation_ = new() { Houtei = true, Tsumo = true };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void NagashimanganTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Nagashimangan = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Nagashimangan };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(5));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void RenhouTest()
    {
        hand_ = new(man: "234456", pin: "66", sou: "123444");
        winTile_ = Sou4;
        situation_ = new() { Renhou = true, Player = Wind.South };
        rules_ = new() { RenhouAsYakuman = false };
        actual_ = Calc();
        expected_ = new() { Yaku.Renhou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(5));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        situation_ = new() { Renhou = true, Tsumo = true };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        situation_ = new() { Renhou = true };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        hand_ = new(man: "234456", pin: "66", sou: "123");
        fuuroList_ = new() { new(Pon, new(sou: "444")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    #endregion 状況による役

    #region 1翻

    [Test]
    public void PinfuTest()
    {
        hand_ = new(man: "123456", pin: "55", sou: "123456");
        winTile_ = Man6;
        actual_ = Calc();
        expected_ = new() { Yaku.Pinfu };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "123456", sou: "123456", honor: "22");
        actual_ = Calc();
        expected_ = new() { Yaku.Pinfu };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        winTile_ = Man3;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        winTile_ = Man5;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        winTile_ = Pin5;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        hand_ = new(man: "123456", sou: "123456", honor: "11");
        winTile_ = Man1;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        hand_ = new(man: "123456", pin: "55", sou: "123444");
        winTile_ = Man6;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        hand_ = new(man: "456", pin: "55", sou: "123456");
        fuuroList_ = new() { new(Chi, new(man: "123")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void TanyaoTest()
    {
        hand_ = new(man: "234567", pin: "55", sou: "234555");
        winTile_ = Man7;
        actual_ = Calc();
        expected_ = new() { Yaku.Tanyao };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        fuuroList_ = new() { new(Chi, new(man: "234")) };
        actual_ = Calc();
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        rules_ = new() { Kuitan = false };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);

        hand_ = new(man: "234567", pin: "55", sou: "234", honor: "222");
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void IipeikouTest()
    {
        hand_ = new(man: "22", pin: "223344", sou: "123444");
        winTile_ = Pin2;
        actual_ = Calc();
        expected_ = new() { Yaku.Iipeikou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "22", pin: "223344", sou: "444");
        winTile_ = Pin2;
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    #endregion 1翻

    #region 2翻

    [Test]
    public void SanshokuTest()
    {
        hand_ = new(man: "12399", pin: "123456", sou: "123");
        winTile_ = Man2;
        actual_ = Calc();
        expected_ = new() { Yaku.Sanshoku };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "12399", pin: "123456");
        winTile_ = Man2;
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        expected_ = new() { Yaku.Sanshoku };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void ChantaTest()
    {
        hand_=new(man:"123789",sou:"123",honor:"22333");
        winTile_ = Sha;
        actual_ = Calc();
        expected_ = new() { Yaku.Chanta };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "123789", honor: "22333");
        winTile_ = Sha;
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        expected_ = new() { Yaku.Chanta };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(1));
            Assert.That(actual_.Fu, Is.EqualTo(30));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void HonroutouTest()
    {
        hand_ = new(man: "111", sou: "999", honor: "11222");
        winTile_ = Nan;
        fuuroList_ = new() { new(Pon, new(sou: "111")) };
        actual_ = Calc();
        expected_ = new() { Yaku.Honroto, Yaku.Toitoihou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(4));
            Assert.That(actual_.Fu, Is.EqualTo(50));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "1199", pin: "11", honor: "22334466");
        winTile_ = Man1;
        fuuroList_ = null;
        actual_ = Calc();
        expected_ = new() { Yaku.Honroto, Yaku.Chiitoitsu };
    }

    [Test]
    public void ToitoihouTest()
    {
        hand_ = new(man: "333", pin: "44555");
        winTile_ = Pin5;
        fuuroList_ = new()
        {
            new(Pon, new(sou:"111")),
            new(Pon, new(sou:"333")),
        };
        actual_ = Calc();
        expected_ = new() { Yaku.Toitoihou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "333", pin: "44555", sou: "111");
        winTile_ = Pin5;
        fuuroList_ = new() { new(Pon, new(sou: "333")) };
        situation_ = new() { Tsumo = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Toitoihou, Yaku.Sanankou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(4));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void SanankouTest()
    {
        hand_ = new(man: "333", pin: "44555", sou: "444");
        winTile_ = Pin5;
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        situation_ = new() { Tsumo = true };
        actual_ = Calc();
        expected_ = new() { Yaku.Sanankou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void SankantsuTest()
    {
        hand_ = new(man: "123", pin: "44");
        winTile_ = Man3;
        fuuroList_ = new()
        {
            new(Minkan,new(sou:"1111")),
            new(Minkan,new(sou:"3333")),
            new(Minkan,new(pin:"6666")),
        };
        actual_ = Calc();
        expected_ = new() { Yaku.Sankantsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(60));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void ShanshokudoukouTest()
    {
        hand_ = new(man: "222", pin: "22245699", sou: "222");
        winTile_ = Pin9;
        actual_ = Calc();
        expected_ = new() { Yaku.Sanshokudoukou, Yaku.Sanankou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(4));
            Assert.That(actual_.Fu, Is.EqualTo(50));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "222", pin: "22245699");
        winTile_ = Pin9;
        fuuroList_ = new() { new(Pon, new(sou: "222")) };
        actual_ = Calc();
        expected_ = new() { Yaku.Sanshokudoukou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "222", pin: "22245699");
        winTile_ = Pin9;
        fuuroList_ = new() { new(Minkan, new(sou: "2222")) };
        actual_ = Calc();
        expected_ = new() { Yaku.Sanshokudoukou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    [Test]
    public void ChiitoitsuTest()
    {
        hand_ = new(man: "113355", pin: "11", sou: "113355");
        winTile_ = Pin1;
        actual_ = Calc();
        expected_ = new() { Yaku.Chiitoitsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(2));
            Assert.That(actual_.Fu, Is.EqualTo(25));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "11335555", pin: "1133", sou: "11");
        winTile_ = Sou1;
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    [Test]
    public void ShousangenTest()
    {
        hand_ = new(man: "345", sou: "123", honor: "55666777");
        winTile_ = TileKind.Chun;
        actual_ = Calc();
        expected_ = new() { Yaku.Shousangen, Yaku.Hatsu, Yaku.Chun };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(4));
            Assert.That(actual_.Fu, Is.EqualTo(50));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });
    }

    #endregion 2翻

    #region 3翻

    [Test]
    public void RyanpeikouTest()
    {
        hand_ = new(man: "22", pin: "223344", sou: "112233");
        winTile_ = Pin3;
        actual_ = Calc();
        expected_ = new() { Yaku.Ryanpeikou };
        Assert.Multiple(() =>
        {
            Assert.That(actual_.Han, Is.EqualTo(3));
            Assert.That(actual_.Fu, Is.EqualTo(40));
            Assert.That(actual_.YakuList, Is.EquivalentTo(expected_));
        });

        hand_ = new(man: "22", pin: "223344", sou: "123");
        fuuroList_ = new() { new(Chi, new(sou: "123")) };
        actual_ = Calc();
        Assert.That(actual_.Error, Is.Not.Null);
    }

    #endregion 3翻
}