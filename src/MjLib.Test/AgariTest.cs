using MjLib.Agaris;
using MjLib.TileKinds;

namespace MjLib.Test;

public class AgariTest
{
    private TileKindList? hand_;

    [Test]
    public void IsAgariTest()
    {
        hand_ = new(man: "33", pin: "123", sou: "123456789");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "11123", sou: "123456789");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(sou: "123456789", honor: "11777");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(sou: "12345556778899");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(sou: "11123456788999");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "345", pin: "789", sou: "233334", honor: "55");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "12345", sou: "123456789");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        hand_ = new(pin: "11145", sou: "111222444");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        hand_ = new(sou: "11122233356888");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        hand_ = new(sou: "12345667788889");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "12344456777888");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "45566777789999");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "45566677888999");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "12356667778999");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        // 字牌4枚
        hand_ = new(man: "1234567899", honor: "1111");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        // 数牌1枚余り
        hand_ = new(man: "123456789", pin: "1", honor: "111");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        // 対子2つ
        hand_ = new(man: "123456", pin: "11", sou: "11", honor: "111");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        // 数牌2つ余り&&字牌の対子あり
        hand_ = new(man: "123456", pin: "111", sou: "12", honor: "11");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        // 一盃口形
        hand_ = new(man: "112233", pin: "123", sou: "11", honor: "111");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        // 順子を取った残りが不適切
        hand_ = new(man: "112334", pin: "123", sou: "123", honor: "111");
        Assert.That(Agari.IsAgari(hand_), Is.False);
    }

    [Test]
    public void IsAgariChiitoitsuTest()
    {
        hand_ = new(pin: "1199", sou: "1133557799");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "11", pin: "1199", sou: "2244", honor: "2277");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "11223344556677");
        Assert.That(Agari.IsAgari(hand_), Is.True);
    }

    [Test]
    public void IsAgariKokushiTest()
    {
        hand_ = new(man: "199", pin: "19", sou: "19", honor: "1234567");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "19", pin: "19", sou: "19", honor: "11234567");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "19", pin: "19", sou: "19", honor: "12345677");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(man: "19", pin: "19", sou: "129", honor: "1234567");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        hand_ = new(man: "19", pin: "19", sou: "19", honor: "11134567");
        Assert.That(Agari.IsAgari(hand_), Is.False);
    }

    [Test]
    public void IsAgariOpenHand()
    {
        hand_ = new(pin: "222", sou: "23466");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "22", sou: "234678");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "222", sou: "44499");
        Assert.That(Agari.IsAgari(hand_), Is.True);

        hand_ = new(pin: "222", sou: "444789");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        hand_ = new(pin: "222");
        Assert.That(Agari.IsAgari(hand_), Is.False);

        hand_ = new(pin: "22");
        Assert.That(Agari.IsAgari(hand_), Is.True);
    }
}