using static MjLib.TileKinds.TileKind;

namespace MjLib.Test.TileKinds;

public class TileKindTest
{
    [Test]
    public void NumberTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.Number, Is.EqualTo(1));
            Assert.That(Man2.Number, Is.EqualTo(2));
            Assert.That(Man3.Number, Is.EqualTo(3));
            Assert.That(Man4.Number, Is.EqualTo(4));
            Assert.That(Man5.Number, Is.EqualTo(5));
            Assert.That(Man6.Number, Is.EqualTo(6));
            Assert.That(Man7.Number, Is.EqualTo(7));
            Assert.That(Man8.Number, Is.EqualTo(8));
            Assert.That(Man9.Number, Is.EqualTo(9));
            Assert.That(Pin1.Number, Is.EqualTo(1));
            Assert.That(Pin2.Number, Is.EqualTo(2));
            Assert.That(Pin3.Number, Is.EqualTo(3));
            Assert.That(Pin4.Number, Is.EqualTo(4));
            Assert.That(Pin5.Number, Is.EqualTo(5));
            Assert.That(Pin6.Number, Is.EqualTo(6));
            Assert.That(Pin7.Number, Is.EqualTo(7));
            Assert.That(Pin8.Number, Is.EqualTo(8));
            Assert.That(Pin9.Number, Is.EqualTo(9));
            Assert.That(Sou1.Number, Is.EqualTo(1));
            Assert.That(Sou2.Number, Is.EqualTo(2));
            Assert.That(Sou3.Number, Is.EqualTo(3));
            Assert.That(Sou4.Number, Is.EqualTo(4));
            Assert.That(Sou5.Number, Is.EqualTo(5));
            Assert.That(Sou6.Number, Is.EqualTo(6));
            Assert.That(Sou7.Number, Is.EqualTo(7));
            Assert.That(Sou8.Number, Is.EqualTo(8));
            Assert.That(Sou9.Number, Is.EqualTo(9));
            Assert.That(Ton.Number, Is.EqualTo(1));
            Assert.That(Nan.Number, Is.EqualTo(2));
            Assert.That(Sha.Number, Is.EqualTo(3));
            Assert.That(Pei.Number, Is.EqualTo(4));
            Assert.That(Haku.Number, Is.EqualTo(5));
            Assert.That(Hatsu.Number, Is.EqualTo(6));
            Assert.That(Chun.Number, Is.EqualTo(7));
        });
    }

    [Test]
    public void IsManTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsMan, Is.True);
            Assert.That(Man2.IsMan, Is.True);
            Assert.That(Man3.IsMan, Is.True);
            Assert.That(Man4.IsMan, Is.True);
            Assert.That(Man5.IsMan, Is.True);
            Assert.That(Man6.IsMan, Is.True);
            Assert.That(Man7.IsMan, Is.True);
            Assert.That(Man8.IsMan, Is.True);
            Assert.That(Man9.IsMan, Is.True);
            Assert.That(Pin1.IsMan, Is.False);
            Assert.That(Pin2.IsMan, Is.False);
            Assert.That(Pin3.IsMan, Is.False);
            Assert.That(Pin4.IsMan, Is.False);
            Assert.That(Pin5.IsMan, Is.False);
            Assert.That(Pin6.IsMan, Is.False);
            Assert.That(Pin7.IsMan, Is.False);
            Assert.That(Pin8.IsMan, Is.False);
            Assert.That(Pin9.IsMan, Is.False);
            Assert.That(Sou1.IsMan, Is.False);
            Assert.That(Sou2.IsMan, Is.False);
            Assert.That(Sou3.IsMan, Is.False);
            Assert.That(Sou4.IsMan, Is.False);
            Assert.That(Sou5.IsMan, Is.False);
            Assert.That(Sou6.IsMan, Is.False);
            Assert.That(Sou7.IsMan, Is.False);
            Assert.That(Sou8.IsMan, Is.False);
            Assert.That(Sou9.IsMan, Is.False);
            Assert.That(Ton.IsMan, Is.False);
            Assert.That(Nan.IsMan, Is.False);
            Assert.That(Sha.IsMan, Is.False);
            Assert.That(Pei.IsMan, Is.False);
            Assert.That(Haku.IsMan, Is.False);
            Assert.That(Hatsu.IsMan, Is.False);
            Assert.That(Chun.IsMan, Is.False);
        });
    }

    [Test]
    public void IsPinTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsPin, Is.False);
            Assert.That(Man2.IsPin, Is.False);
            Assert.That(Man3.IsPin, Is.False);
            Assert.That(Man4.IsPin, Is.False);
            Assert.That(Man5.IsPin, Is.False);
            Assert.That(Man6.IsPin, Is.False);
            Assert.That(Man7.IsPin, Is.False);
            Assert.That(Man8.IsPin, Is.False);
            Assert.That(Man9.IsPin, Is.False);
            Assert.That(Pin1.IsPin, Is.True);
            Assert.That(Pin2.IsPin, Is.True);
            Assert.That(Pin3.IsPin, Is.True);
            Assert.That(Pin4.IsPin, Is.True);
            Assert.That(Pin5.IsPin, Is.True);
            Assert.That(Pin6.IsPin, Is.True);
            Assert.That(Pin7.IsPin, Is.True);
            Assert.That(Pin8.IsPin, Is.True);
            Assert.That(Pin9.IsPin, Is.True);
            Assert.That(Sou1.IsPin, Is.False);
            Assert.That(Sou2.IsPin, Is.False);
            Assert.That(Sou3.IsPin, Is.False);
            Assert.That(Sou4.IsPin, Is.False);
            Assert.That(Sou5.IsPin, Is.False);
            Assert.That(Sou6.IsPin, Is.False);
            Assert.That(Sou7.IsPin, Is.False);
            Assert.That(Sou8.IsPin, Is.False);
            Assert.That(Sou9.IsPin, Is.False);
            Assert.That(Ton.IsPin, Is.False);
            Assert.That(Nan.IsPin, Is.False);
            Assert.That(Sha.IsPin, Is.False);
            Assert.That(Pei.IsPin, Is.False);
            Assert.That(Haku.IsPin, Is.False);
            Assert.That(Hatsu.IsPin, Is.False);
            Assert.That(Chun.IsPin, Is.False);
        });
    }

    [Test]
    public void IsSouTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsSou, Is.False);
            Assert.That(Man2.IsSou, Is.False);
            Assert.That(Man3.IsSou, Is.False);
            Assert.That(Man4.IsSou, Is.False);
            Assert.That(Man5.IsSou, Is.False);
            Assert.That(Man6.IsSou, Is.False);
            Assert.That(Man7.IsSou, Is.False);
            Assert.That(Man8.IsSou, Is.False);
            Assert.That(Man9.IsSou, Is.False);
            Assert.That(Pin1.IsSou, Is.False);
            Assert.That(Pin2.IsSou, Is.False);
            Assert.That(Pin3.IsSou, Is.False);
            Assert.That(Pin4.IsSou, Is.False);
            Assert.That(Pin5.IsSou, Is.False);
            Assert.That(Pin6.IsSou, Is.False);
            Assert.That(Pin7.IsSou, Is.False);
            Assert.That(Pin8.IsSou, Is.False);
            Assert.That(Pin9.IsSou, Is.False);
            Assert.That(Sou1.IsSou, Is.True);
            Assert.That(Sou2.IsSou, Is.True);
            Assert.That(Sou3.IsSou, Is.True);
            Assert.That(Sou4.IsSou, Is.True);
            Assert.That(Sou5.IsSou, Is.True);
            Assert.That(Sou6.IsSou, Is.True);
            Assert.That(Sou7.IsSou, Is.True);
            Assert.That(Sou8.IsSou, Is.True);
            Assert.That(Sou9.IsSou, Is.True);
            Assert.That(Ton.IsSou, Is.False);
            Assert.That(Nan.IsSou, Is.False);
            Assert.That(Sha.IsSou, Is.False);
            Assert.That(Pei.IsSou, Is.False);
            Assert.That(Haku.IsSou, Is.False);
            Assert.That(Hatsu.IsSou, Is.False);
            Assert.That(Chun.IsSou, Is.False);
        });
    }

    [Test]
    public void IsHonorTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsHonor, Is.False);
            Assert.That(Man2.IsHonor, Is.False);
            Assert.That(Man3.IsHonor, Is.False);
            Assert.That(Man4.IsHonor, Is.False);
            Assert.That(Man5.IsHonor, Is.False);
            Assert.That(Man6.IsHonor, Is.False);
            Assert.That(Man7.IsHonor, Is.False);
            Assert.That(Man8.IsHonor, Is.False);
            Assert.That(Man9.IsHonor, Is.False);
            Assert.That(Pin1.IsHonor, Is.False);
            Assert.That(Pin2.IsHonor, Is.False);
            Assert.That(Pin3.IsHonor, Is.False);
            Assert.That(Pin4.IsHonor, Is.False);
            Assert.That(Pin5.IsHonor, Is.False);
            Assert.That(Pin6.IsHonor, Is.False);
            Assert.That(Pin7.IsHonor, Is.False);
            Assert.That(Pin8.IsHonor, Is.False);
            Assert.That(Pin9.IsHonor, Is.False);
            Assert.That(Sou1.IsHonor, Is.False);
            Assert.That(Sou2.IsHonor, Is.False);
            Assert.That(Sou3.IsHonor, Is.False);
            Assert.That(Sou4.IsHonor, Is.False);
            Assert.That(Sou5.IsHonor, Is.False);
            Assert.That(Sou6.IsHonor, Is.False);
            Assert.That(Sou7.IsHonor, Is.False);
            Assert.That(Sou8.IsHonor, Is.False);
            Assert.That(Sou9.IsHonor, Is.False);
            Assert.That(Ton.IsHonor, Is.True);
            Assert.That(Nan.IsHonor, Is.True);
            Assert.That(Sha.IsHonor, Is.True);
            Assert.That(Pei.IsHonor, Is.True);
            Assert.That(Haku.IsHonor, Is.True);
            Assert.That(Hatsu.IsHonor, Is.True);
            Assert.That(Chun.IsHonor, Is.True);
        });
    }

    [Test]
    public void IsWind()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsWind, Is.False);
            Assert.That(Man2.IsWind, Is.False);
            Assert.That(Man3.IsWind, Is.False);
            Assert.That(Man4.IsWind, Is.False);
            Assert.That(Man5.IsWind, Is.False);
            Assert.That(Man6.IsWind, Is.False);
            Assert.That(Man7.IsWind, Is.False);
            Assert.That(Man8.IsWind, Is.False);
            Assert.That(Man9.IsWind, Is.False);
            Assert.That(Pin1.IsWind, Is.False);
            Assert.That(Pin2.IsWind, Is.False);
            Assert.That(Pin3.IsWind, Is.False);
            Assert.That(Pin4.IsWind, Is.False);
            Assert.That(Pin5.IsWind, Is.False);
            Assert.That(Pin6.IsWind, Is.False);
            Assert.That(Pin7.IsWind, Is.False);
            Assert.That(Pin8.IsWind, Is.False);
            Assert.That(Pin9.IsWind, Is.False);
            Assert.That(Sou1.IsWind, Is.False);
            Assert.That(Sou2.IsWind, Is.False);
            Assert.That(Sou3.IsWind, Is.False);
            Assert.That(Sou4.IsWind, Is.False);
            Assert.That(Sou5.IsWind, Is.False);
            Assert.That(Sou6.IsWind, Is.False);
            Assert.That(Sou7.IsWind, Is.False);
            Assert.That(Sou8.IsWind, Is.False);
            Assert.That(Sou9.IsWind, Is.False);
            Assert.That(Ton.IsWind, Is.True);
            Assert.That(Nan.IsWind, Is.True);
            Assert.That(Sha.IsWind, Is.True);
            Assert.That(Pei.IsWind, Is.True);
            Assert.That(Haku.IsWind, Is.False);
            Assert.That(Hatsu.IsWind, Is.False);
            Assert.That(Chun.IsWind, Is.False);
        });
    }

    [Test]
    public void IsDragon()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsDragon, Is.False);
            Assert.That(Man2.IsDragon, Is.False);
            Assert.That(Man3.IsDragon, Is.False);
            Assert.That(Man4.IsDragon, Is.False);
            Assert.That(Man5.IsDragon, Is.False);
            Assert.That(Man6.IsDragon, Is.False);
            Assert.That(Man7.IsDragon, Is.False);
            Assert.That(Man8.IsDragon, Is.False);
            Assert.That(Man9.IsDragon, Is.False);
            Assert.That(Pin1.IsDragon, Is.False);
            Assert.That(Pin2.IsDragon, Is.False);
            Assert.That(Pin3.IsDragon, Is.False);
            Assert.That(Pin4.IsDragon, Is.False);
            Assert.That(Pin5.IsDragon, Is.False);
            Assert.That(Pin6.IsDragon, Is.False);
            Assert.That(Pin7.IsDragon, Is.False);
            Assert.That(Pin8.IsDragon, Is.False);
            Assert.That(Pin9.IsDragon, Is.False);
            Assert.That(Sou1.IsDragon, Is.False);
            Assert.That(Sou2.IsDragon, Is.False);
            Assert.That(Sou3.IsDragon, Is.False);
            Assert.That(Sou4.IsDragon, Is.False);
            Assert.That(Sou5.IsDragon, Is.False);
            Assert.That(Sou6.IsDragon, Is.False);
            Assert.That(Sou7.IsDragon, Is.False);
            Assert.That(Sou8.IsDragon, Is.False);
            Assert.That(Sou9.IsDragon, Is.False);
            Assert.That(Ton.IsDragon, Is.False);
            Assert.That(Nan.IsDragon, Is.False);
            Assert.That(Sha.IsDragon, Is.False);
            Assert.That(Pei.IsDragon, Is.False);
            Assert.That(Haku.IsDragon, Is.True);
            Assert.That(Hatsu.IsDragon, Is.True);
            Assert.That(Chun.IsDragon, Is.True);
        });
    }

    [Test]
    public void IsChuchan()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsChuchan, Is.False);
            Assert.That(Man2.IsChuchan, Is.True);
            Assert.That(Man3.IsChuchan, Is.True);
            Assert.That(Man4.IsChuchan, Is.True);
            Assert.That(Man5.IsChuchan, Is.True);
            Assert.That(Man6.IsChuchan, Is.True);
            Assert.That(Man7.IsChuchan, Is.True);
            Assert.That(Man8.IsChuchan, Is.True);
            Assert.That(Man9.IsChuchan, Is.False);
            Assert.That(Pin1.IsChuchan, Is.False);
            Assert.That(Pin2.IsChuchan, Is.True);
            Assert.That(Pin3.IsChuchan, Is.True);
            Assert.That(Pin4.IsChuchan, Is.True);
            Assert.That(Pin5.IsChuchan, Is.True);
            Assert.That(Pin6.IsChuchan, Is.True);
            Assert.That(Pin7.IsChuchan, Is.True);
            Assert.That(Pin8.IsChuchan, Is.True);
            Assert.That(Pin9.IsChuchan, Is.False);
            Assert.That(Sou1.IsChuchan, Is.False);
            Assert.That(Sou2.IsChuchan, Is.True);
            Assert.That(Sou3.IsChuchan, Is.True);
            Assert.That(Sou4.IsChuchan, Is.True);
            Assert.That(Sou5.IsChuchan, Is.True);
            Assert.That(Sou6.IsChuchan, Is.True);
            Assert.That(Sou7.IsChuchan, Is.True);
            Assert.That(Sou8.IsChuchan, Is.True);
            Assert.That(Sou9.IsChuchan, Is.False);
            Assert.That(Ton.IsChuchan, Is.False);
            Assert.That(Nan.IsChuchan, Is.False);
            Assert.That(Sha.IsChuchan, Is.False);
            Assert.That(Pei.IsChuchan, Is.False);
            Assert.That(Haku.IsChuchan, Is.False);
            Assert.That(Hatsu.IsChuchan, Is.False);
            Assert.That(Chun.IsChuchan, Is.False);
        });
    }

    [Test]
    public void IsYaochu()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Man1.IsYaochu, Is.True);
            Assert.That(Man2.IsYaochu, Is.False);
            Assert.That(Man3.IsYaochu, Is.False);
            Assert.That(Man4.IsYaochu, Is.False);
            Assert.That(Man5.IsYaochu, Is.False);
            Assert.That(Man6.IsYaochu, Is.False);
            Assert.That(Man7.IsYaochu, Is.False);
            Assert.That(Man8.IsYaochu, Is.False);
            Assert.That(Man9.IsYaochu, Is.True);
            Assert.That(Pin1.IsYaochu, Is.True);
            Assert.That(Pin2.IsYaochu, Is.False);
            Assert.That(Pin3.IsYaochu, Is.False);
            Assert.That(Pin4.IsYaochu, Is.False);
            Assert.That(Pin5.IsYaochu, Is.False);
            Assert.That(Pin6.IsYaochu, Is.False);
            Assert.That(Pin7.IsYaochu, Is.False);
            Assert.That(Pin8.IsYaochu, Is.False);
            Assert.That(Pin9.IsYaochu, Is.True);
            Assert.That(Sou1.IsYaochu, Is.True);
            Assert.That(Sou2.IsYaochu, Is.False);
            Assert.That(Sou3.IsYaochu, Is.False);
            Assert.That(Sou4.IsYaochu, Is.False);
            Assert.That(Sou5.IsYaochu, Is.False);
            Assert.That(Sou6.IsYaochu, Is.False);
            Assert.That(Sou7.IsYaochu, Is.False);
            Assert.That(Sou8.IsYaochu, Is.False);
            Assert.That(Sou9.IsYaochu, Is.True);
            Assert.That(Ton.IsYaochu, Is.True);
            Assert.That(Nan.IsYaochu, Is.True);
            Assert.That(Sha.IsYaochu, Is.True);
            Assert.That(Pei.IsYaochu, Is.True);
            Assert.That(Haku.IsYaochu, Is.True);
            Assert.That(Hatsu.IsYaochu, Is.True);
            Assert.That(Chun.IsYaochu, Is.True);
        });
    }
}