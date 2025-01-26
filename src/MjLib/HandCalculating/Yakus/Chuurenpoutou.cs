using MjLib.HandCalculating.Dividings;

namespace MjLib.HandCalculating.Yakus;

internal record Chuurenpoutou : Yaku
{
    public Chuurenpoutou(int id)
        : base(id) { }

    public override string Name => "九蓮宝燈";
    public override int HanOpen => 0;
    public override int HanClosed => 13;
    public override bool IsYakuman => true;

    public static bool Valid(TileListList hand)
    {
        var mans = hand.Where(x => x[0].IsMan);
        var pins = hand.Where(x => x[0].IsPin);
        var sous = hand.Where(x => x[0].IsSou);
        var honor = hand.Count(x => x[0].IsHonor);
        var suit = new[] { mans, pins, sous }.Where(x => x.Any());
        if (suit.Count() != 1 && honor == 0) return false;
        var nums = suit.First().SelectMany(x => x, (x, y) => y.Number).ToList();
        if (nums.Count(x => x == 1) < 3 || nums.Count(x => x == 9) < 3) return false;
        nums.Remove(1); nums.Remove(1);
        nums.Remove(9); nums.Remove(9);
        for (var n = 1; n <= 9; n++)
        {
            if (nums.Contains(n))
            {
                nums.Remove(n);
            }
        }
        return nums.Count == 1;
    }
}