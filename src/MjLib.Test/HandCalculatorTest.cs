using MjLib.Fuuros;
using MjLib.HandCalculating;
using MjLib.HandCalculating.Hands;
using MjLib.HandCalculating.Yakus;
using MjLib.TileKinds;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class HandCalculatorTest
{
    [Test]
    public void InvalidHandTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123445");
        var winTile = Sou4;
        var actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        winTile = Pin4;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);
    }

    #region 状況による役

    [Test]
    public void RiichiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Riichi = true };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.Riichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void DaburuRiichiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { DaburuRiichi = true };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.DaburuRiichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void TsumoTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Tsumo = true };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.Tsumo };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void IppatsuTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Riichi = true, Ippatsu = true };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.Riichi, Yaku.Ippatsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        situation = new WinSituation { Riichi = false, Ippatsu = true };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void RinshanTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123");
        var winTile = Sou1;
        var fuuroList = new FuuroList { new(FuuroType.Ankan, TileKindList.Parse(sou: "4444")) };
        var situation = new WinSituation { Tsumo = true, Rinshan = true };
        var actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        var expected = new YakuList { Yaku.Tsumo, Yaku.Rinshan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        fuuroList = new FuuroList { new(FuuroType.Minkan, TileKindList.Parse(sou: "4444")) };
        situation = new WinSituation { Tsumo = true, Rinshan = true };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        expected = new YakuList { Yaku.Rinshan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        situation = new WinSituation { Rinshan = true };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void ChankanTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Chankan = true };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.Chankan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        situation = new WinSituation { Chankan = true, Tsumo = true };
        actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void HaiteiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Haitei = true, Tsumo = true };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.Tsumo, Yaku.Haitei };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        situation = new WinSituation { Haitei = true, Tsumo = false };
        actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void HouteiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Houtei = true, Tsumo = false };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        var expected = new YakuList { Yaku.Houtei };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        situation = new WinSituation { Houtei = true, Tsumo = true };
        actual = HandCalculator.Calculate(hand, winTile, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void NagashimanganTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Nagashimangan = true };
        var actual = HandCalculator.Calculate(hand, null, situation: situation);
        var expected = new YakuList { Yaku.Nagashimangan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(5));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });
    }

    [Test]
    public void RenhouTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var situation = new WinSituation { Renhou = true, Player = Wind.South };
        var rules = new GameRules { RenhouAsYakuman = false };
        var actual = HandCalculator.Calculate(hand, winTile, situation: situation, rules: rules);
        var expected = new YakuList { Yaku.Renhou };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(5));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        situation = new WinSituation { Renhou = true, Tsumo = true };
        actual = HandCalculator.Calculate(hand, winTile, situation: situation, rules: rules);
        Assert.That(actual.Error, Is.Not.Null);

        situation = new WinSituation { Renhou = true };
        actual = HandCalculator.Calculate(hand, winTile, situation: situation, rules: rules);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123");
        var fuuroList = new FuuroList { new(FuuroType.Pon, TileKindList.Parse(sou: "444")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, situation: situation);
        Assert.That(actual.Error, Is.Not.Null);
    }

    #endregion 状況による役

    #region 1翻

    [Test]
    public void PinfuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "55", sou: "123456");
        var winTile = Man6;
        var actual = HandCalculator.Calculate(hand, winTile);
        var expected = new YakuList { Yaku.Pinfu };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "123456", sou: "123456", honor: "22");
        actual = HandCalculator.Calculate(hand, winTile);
        expected = new YakuList { Yaku.Pinfu };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        winTile = Man3;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);

        winTile = Man5;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);

        winTile = Pin5;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "123456", sou: "123456", honor: "11");
        winTile = Man1;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "123456", pin: "55", sou: "123444");
        winTile = Man6;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "456", pin: "55", sou: "123456");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(man: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void TanyaoTest()
    {
        var hand = TileKindList.Parse(man: "234567", pin: "55", sou: "234555");
        var winTile = Man7;
        var actual = HandCalculator.Calculate(hand, winTile);
        var expected = new YakuList { Yaku.Tanyao };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(man: "234")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        var rules = new GameRules { Kuitan = false };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, rules: rules);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "234567", pin: "55", sou: "234", honor: "222");
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void IipeikouTest()
    {
        var hand = TileKindList.Parse(man: "22", pin: "223344", sou: "123444");
        var winTile = Pin2;
        var actual = HandCalculator.Calculate(hand, winTile);
        var expected = new YakuList { Yaku.Iipeikou };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "22", pin: "223344", sou: "444");
        winTile = Pin2;
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.That(actual.Error, Is.Not.Null);
    }

    #endregion 1翻

    #region 2翻

    [Test]
    public void ChiitoitsuTest()
    {
        var hand = TileKindList.Parse(man: "113355", pin: "11", sou: "113355");
        var winTile = Pin1;
        var actual = HandCalculator.Calculate(hand, winTile);
        var expected = new YakuList { Yaku.Chiitoitsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(25));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "11335555", pin: "1133", sou: "11");
        winTile = Sou1;
        actual = HandCalculator.Calculate(hand, winTile);
        Assert.That(actual.Error, Is.Not.Null);
    }

    #endregion 2翻

    #region 3翻

    [Test]
    public void RyanpeikouTest()
    {
        var hand = TileKindList.Parse(man: "22", pin: "223344", sou: "112233");
        var winTile = Pin3;
        var actual = HandCalculator.Calculate(hand, winTile);
        var expected = new YakuList { Yaku.Ryanpeikou };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Error, Is.Null);
            Assert.That(actual.Han, Is.EqualTo(3));
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "22", pin: "223344", sou: "123");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.That(actual.Error, Is.Not.Null);
    }

    #endregion 3翻
}