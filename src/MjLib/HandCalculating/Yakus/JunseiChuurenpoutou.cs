using MjLib.HandCalculating.Dividings;
using MjLib.TileKinds;

namespace MjLib.HandCalculating.Yakus;

internal class JunseiChuurenpoutou : Yaku
{
    public JunseiChuurenpoutou(int id)
        : base(id) { }

    public override string Name => "純正九蓮宝燈";
    public override int HanOpen => 0;
    public override int HanClosed => 26;
    public override bool IsYakuman => true;

    public static bool Valid(TileKindListList hand, TileKind winTile, GameRules rules)
    {
        if (!rules.DaburuYakuman) return false;
        var mans = hand.Where(x => x[0].IsMan);
        var pins = hand.Where(x => x[0].IsPin);
        var sous = hand.Where(x => x[0].IsSou);
        var honor = hand.Count(x => x[0].IsHonor);
        var suit = new[] { mans, pins, sous }.Where(x => x.Any());
        if (suit.Count() != 1 && honor == 0) return false;
        var nums = suit.First().SelectMany(x => x, (x, y) => y.Number).ToList();
        // 純正九蓮宝燈用
        if (nums.Count(x => x == winTile.Number) is not 2 and not 4) return false;
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