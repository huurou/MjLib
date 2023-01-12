using MjLib.Fuuros;
using MjLib.HandCalculating;
using MjLib.HandCalculating.Dividings;
using MjLib.HandCalculating.Fus;
using MjLib.TileKinds;
using static MjLib.Fuuros.FuuroType;
using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.HandCalculating;

public class FuTest
{
    [Test]
    public void ChiitoitsuTest()
    {
        var hand = TileKindList.Parse(man: "115599", pin: "66", sou: "112244");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var winGroup = TileKindList.Parse(pin: "66");
        var handConfig = new HandConfig();
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Chiitoitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(25));
    }

    [Test]
    public void OpenHandBaseTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "11", sou: "678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(pin: "678");
        var handConfig = new HandConfig();
        var fuuroList = new FuuroList { new(Pon, TileKindList.Parse(man: "222")) };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        var expected = new FuList { Fu.Base, Fu.ChuuchanMinko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));
    }

    [Test]
    public void FuBasedOnWinGroupTest()
    {
        var hand = TileKindList.Parse(man: "234789", pin: "12345666");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var winGroup = TileKindList.Parse(pin: "456");
        var handConfig = new HandConfig();
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Menzen };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));

        winGroup = TileKindList.Parse(pin: "66");
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.Tanki };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void OpenHandWithoutAdditionalFuTest()
    {
        var hand = TileKindList.Parse(man: "234567", pin: "22", sou: "234678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "678");
        var handConfig = new HandConfig();
        var fuuroList = new FuuroList { new(Chi, TileKindList.Parse(sou: "234")) };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        var expected = new FuList { Fu.OpenPinfuBase };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));
    }

    [Test]
    public void TsumoBaseTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "11", sou: "222678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "678");
        var handConfig = new HandConfig { IsTsumo = true };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Tsumo, Fu.ChuuchanAnko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));
    }

    [Test]
    public void TsumoPinfuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "123", sou: "22678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "678");
        var handConfig = new HandConfig { IsTsumo = true };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(20));
    }

    [Test]
    public void TsumoPinfuDisabledTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "123", sou: "22678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "678");
        var handConfig = new HandConfig { Rurles = new() { Pinzumo = false }, IsTsumo = true };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Tsumo };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));
    }

    [Test]
    public void TsumoNotPinfuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "111", sou: "22678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "678");
        var handConfig = new HandConfig { IsTsumo = true };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Tsumo, Fu.YaochuuAnko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));
    }

    [Test]
    public void PenchanFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "55", sou: "123456");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou3;
        var winGroup = TileKindList.Parse(sou: "123");
        var handConfig = new HandConfig();
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Menzen, Fu.Penchan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", pin: "55", sou: "345789");
        devided = HandDevider.Devide(hand)[0];
        winTile = Sou7;
        winGroup = TileKindList.Parse(sou: "789");
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.Penchan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void KanchanFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "55", sou: "123567");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "567");
        var handConfig = new HandConfig();
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Menzen, Fu.Kanchan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void TankiFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "11", sou: "123567");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin1;
        var winGroup = TileKindList.Parse(pin: "11");
        var handConfig = new HandConfig();
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Menzen, Fu.Tanki };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void YakuhaiJantouFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", sou: "123678", honor: "11");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(sou: "678");
        var handConfig = new HandConfig { PlayerWind = Wind.East, RoundWind = Wind.South };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Menzen, Fu.PlayerWindToitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", sou: "123678", honor: "22");
        devided = HandDevider.Devide(hand)[0];
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.RoundWindToitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
        
        hand = TileKindList.Parse(man: "123456", sou: "123678", honor: "44");
        devided = HandDevider.Devide(hand)[0];
        handConfig = new HandConfig { PlayerWind = Wind.East, RoundWind = Wind.North };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.RoundWindToitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", sou: "123678", honor: "33");
        devided = HandDevider.Devide(hand)[0];
        handConfig = new HandConfig { PlayerWind = Wind.West, RoundWind = Wind.West };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.PlayerWindToitsu, Fu.RoundWindToitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", sou: "123678", honor: "55");
        devided = HandDevider.Devide(hand)[0];
        handConfig = new HandConfig();
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.DragonToitsu };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void MinkoFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "456", sou: "66");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var winGroup = TileKindList.Parse(pin: "456");
        var handConfig = new HandConfig();
        var fuuroList = new FuuroList { new(Pon, TileKindList.Parse(sou: "222")) };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        var expected = new FuList { Fu.Base, Fu.ChuuchanMinko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));

        fuuroList = new FuuroList { new(Pon, TileKindList.Parse(sou: "999")) };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        expected = new FuList { Fu.Base, Fu.YaochuuMinko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));

        hand = TileKindList.Parse(man: "123456", pin: "444", sou: "12366");
        devided = HandDevider.Devide(hand)[0];
        winTile = Pin4;
        winGroup = TileKindList.Parse(pin: "444");
        handConfig = new HandConfig();
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.ChuuchanMinko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", pin: "111", sou: "12366");
        devided = HandDevider.Devide(hand)[0];
        winTile = Pin1;
        winGroup = TileKindList.Parse(pin: "111");
        handConfig = new HandConfig();
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.YaochuuMinko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void AnkoFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "11", sou: "222678");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Sou6;
        var winGroup = TileKindList.Parse(pin: "678");
        var handConfig = new HandConfig();
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        var expected = new FuList { Fu.Base, Fu.Menzen, Fu.ChuuchanAnko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", pin: "11", sou: "678", honor: "111");
        devided = HandDevider.Devide(hand)[0];
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.YaochuuAnko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        hand = TileKindList.Parse(man: "123456", pin: "11", sou: "222678");
        devided = HandDevider.Devide(hand)[0];
        winTile = Sou2;
        winGroup = TileKindList.Parse(sou: "222");
        handConfig = new HandConfig { IsTsumo = true };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Tsumo, Fu.ChuuchanAnko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));

        hand = TileKindList.Parse(man: "123456", pin: "11", sou: "111678");
        devided = HandDevider.Devide(hand)[0];
        winTile = Sou1;
        winGroup = TileKindList.Parse(sou: "111");
        handConfig = new HandConfig { IsTsumo = true };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig);
        expected = new FuList { Fu.Base, Fu.Tsumo, Fu.YaochuuAnko };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));
    }

    [Test]
    public void MinkanFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "456", sou: "66");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var winGroup = TileKindList.Parse(pin: "456");
        var handConfig = new HandConfig();
        var fuuroList = new FuuroList { new(Daiminkan, TileKindList.Parse(pin: "2222")) };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        var expected = new FuList { Fu.Base, Fu.ChuuchanMinkan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));

        fuuroList = new FuuroList { new(Shouminkan, TileKindList.Parse(pin: "2222")) };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        expected = new FuList { Fu.Base, Fu.ChuuchanMinkan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(30));

        fuuroList = new FuuroList { new(Daiminkan, TileKindList.Parse(honor: "7777")) };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        expected = new FuList { Fu.Base, Fu.YaochuuMinkan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));

        fuuroList = new FuuroList { new(Shouminkan, TileKindList.Parse(sou: "1111")) };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        expected = new FuList { Fu.Base, Fu.YaochuuMinkan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(40));
    }

    [Test]
    public void AnkanFuTest()
    {
        var hand = TileKindList.Parse(man: "123456", pin: "456", sou: "66");
        var devided = HandDevider.Devide(hand)[0];
        var winTile = Pin6;
        var winGroup = TileKindList.Parse(pin: "456");
        var handConfig = new HandConfig();
        var fuuroList = new FuuroList { new(Ankan, TileKindList.Parse(pin: "2222")) };
        var actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        var expected = new FuList { Fu.Base, Fu.Menzen, Fu.ChuuchanAnkan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(50));

        fuuroList = new FuuroList { new(Ankan, TileKindList.Parse(honor: "7777")) };
        actual = FuCalculator.Calc(devided, winTile, winGroup, handConfig, fuuroList);
        expected = new FuList { Fu.Base, Fu.Menzen, Fu.YaochuuAnkan };
        Assert.That(actual, Is.EquivalentTo(expected));
        Assert.That(actual.Total, Is.EqualTo(70));
    }
}