using MjLib.Agaris;
using MjLib.Fuuros;
using MjLib.HandCalculating.Hands;
using MjLib.Shantens;
using MjLib.TileKinds;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MjLib;

public static class MjCalculator
{
    public static int AGARI_SHANTEN => Shanten.AGARI_SHANTEN;

    public static bool IsAgari(MjTiles hand)
    {
        return Agari.IsAgari(hand.KindList);
    }

    private static IEnumerable<MjMeld>? ToMelds(this JsonNode? melds)
    {
        var mjMelds = melds?.AsArray().Select(x => x?.ToMeld());
        return mjMelds is not null && mjMelds.All(x => x is not null) ? mjMelds.OfType<MjMeld>() : null;
    }

    private static MjMeld? ToMeld(this JsonNode? meld)
    {
        return meld?["type"]?.ToMeldType() is MjMeldType mjMeldType &&
               meld?["tiles"]?.ToTiles() is MjTiles mjTiles
            ? new(mjMeldType, mjTiles)
            : null;
    }

    private static MjMeldType? ToMeldType(this JsonNode type)
    {
        return type?.ToString() switch
        {
            "chi" => MjMeldType.Chi,
            "pon" => MjMeldType.Pon,
            "ankan" => MjMeldType.Ankan,
            "minkan" => MjMeldType.Minkan,
            "nuki" => MjMeldType.Nuki,
            _ => null
        };
    }

    private static MjTiles? ToTiles(this JsonNode? tiles)
    {
        return tiles is JsonArray array ? new MjTiles(array.Select(x => (int)x!))
            : tiles is JsonObject obj ? new MjTiles(
                man: obj["man"]?.ToString(),
                pin: obj["pin"]?.ToString(),
                sou: obj["sou"]?.ToString(),
                honor: obj["honor"]?.ToString())
            : null;
    }

    private static void ParseHand(ref Utf8JsonReader reader)
    {
        // 配列の場合 [ 0, 1, 2, 9, 10, 11, 27, 27, 27 ] のような記法
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            while (reader.TokenType != JsonTokenType.EndArray)
            {
            }
        }
        // オブジェクトの場合 { "man": "123", "pin": "123", "sou": "123", "honor": "11122" } のような記法
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.TokenType != JsonTokenType.EndObject)
            {
            }
        }
    }

    public static int CalcShanten(MjTiles hand)
    {
        return Shanten.Calculate(hand.KindList);
    }

    public static MjHandResult CalcHand(
        MjTiles hand,
        MjTiles winTile,
        IEnumerable<MjMeld> melds,
        IEnumerable<int> doraIndicators,
        IEnumerable<int> uradoraIndicators,
        MjWinSituation situation,
        MjGameRules rules)
    {
        var fuuroList = new FuuroList(melds.Select(x => x.ToInternalModel()));
        var result = HandCalculator.Calculate(
            hand.KindList,
            winTile.KindList[0],
            fuuroList,
            new TileKindList(doraIndicators),
            new TileKindList(uradoraIndicators),
            situation.ToInternalModel(),
            rules.ToInternalModel());
        return result.Error is null
            ? new(result, fuuroList.HasOpen)
            : throw new InvalidOperationException(result.Error);
    }

    #region Internals

    private static IsAgariOutput IsAgari(IsAgariInput input)
    {
        return new() { Result = IsAgari(input.Hand) };
    }

    private static int CalcShanten(CalcShantenInput input)
    {
        return CalcShanten(input.Hand);
    }

    private class IsAgariInput
    {
        public MjTiles Hand { get; init; }

        public IsAgariInput(string input)
        {
            Hand = new MjTiles(input);
        }
    }

    private class IsAgariOutput
    {
        public bool Result { get; init; }
    }

    private class CalcShantenInput
    {
        public required MjTiles Hand { get; init; }
    }

    private class CalcShantenOutput
    {
        public int Result { get; init; }
    }

    #endregion Internals
}