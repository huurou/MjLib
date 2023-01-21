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
        var config = new HandConfig { IsRiichi = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Riichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void DaburuRiichiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsDaburuRiichi = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.DaburuRiichi };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void TsumoTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsTsumo = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
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
        var config = new HandConfig { IsRiichi = true, IsIppatsu = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Riichi, Yaku.Ippatsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "444");
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        config = new HandConfig { IsRiichi = false, IsIppatsu = true };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void RinshanTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123");
        var winTile = Sou1;
        var fuuroList = new FuuroList { new(FuuroType.Ankan, TileKindList.Parse(sou: "4444")) };
        var config = new HandConfig { IsTsumo = true, IsRinshan = true };
        var actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        var expected = new YakuList { Yaku.Tsumo, Yaku.Rinshan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        fuuroList = new FuuroList { new(FuuroType.Minkan, TileKindList.Parse(sou: "4444")) };
        config = new HandConfig { IsTsumo = true, IsRinshan = true };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        expected = new YakuList { Yaku.Rinshan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        config = new HandConfig { IsRinshan = true };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void ChankanTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsChankan = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Chankan };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        config = new HandConfig { IsChankan = true, IsTsumo = true };
        actual = HandCalculator.Calculate(hand, winTile, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void HaiteiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsHaitei = true, IsTsumo = true };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Tsumo, Yaku.Haitei };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(30));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        config = new HandConfig { IsHaitei = true, IsTsumo = false };
        actual = HandCalculator.Calculate(hand, winTile, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void HouteiTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsHoutei = true, IsTsumo = false };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Houtei };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(1));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        config = new HandConfig { IsHoutei = true, IsTsumo = true };
        actual = HandCalculator.Calculate(hand, winTile, config: config);
        Assert.That(actual.Error, Is.Not.Null);
    }

    [Test]
    public void NagashimanganTest()
    {
        var hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123444");
        var winTile = Sou4;
        var config = new HandConfig { IsNagashimangan = true };
        var actual = HandCalculator.Calculate(hand, null, config: config);
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
        var config = new HandConfig { Rurles = new() { RenhouAsYakuman = false }, IsRenhou = true, PlayerWind = Wind.South };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Renhou };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(40));
            Assert.That(actual.Han, Is.EqualTo(5));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });

        config = new HandConfig { Rurles = new() { RenhouAsYakuman = false }, IsRenhou = true, IsTsumo = true };
        actual = HandCalculator.Calculate(hand, winTile, config: config);
        Assert.That(actual.Error, Is.Not.Null);

        config = new HandConfig { Rurles = new() { RenhouAsYakuman = false }, IsRenhou = true };
        actual = HandCalculator.Calculate(hand, winTile, config: config);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "234456", pin: "66", sou: "123");
        var fuuroList = new FuuroList { new(FuuroType.Pon, TileKindList.Parse(sou: "444")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config: config);
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

        var config = new HandConfig { Rurles = new() { Kuitan = false } };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList, config:config);
        Assert.That(actual.Error, Is.Not.Null);

        hand = TileKindList.Parse(man: "234567", pin: "55", sou: "234", honor:"222");
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
        var fuuroList = new FuuroList { new(FuuroType.Chi, TileKindList.Parse(sou: "123")) };
        actual = HandCalculator.Calculate(hand, winTile, fuuroList);
        Assert.That(actual.Error, Is.Not.Null);
    }

    #endregion 1翻

    [Test]
    public void ChiitoitsuTest()
    {
        var hand = TileKindList.Parse(man: "113355", pin: "11", sou: "113355");
        var winTile = Pin1;
        var config = new HandConfig { };
        var actual = HandCalculator.Calculate(hand, winTile, config: config);
        var expected = new YakuList { Yaku.Chiitoitsu };
        Assert.Multiple(() =>
        {
            Assert.That(actual.Fu, Is.EqualTo(25));
            Assert.That(actual.Han, Is.EqualTo(2));
            Assert.That(actual.YakuList, Is.EquivalentTo(expected));
        });
    }
}