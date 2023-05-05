using MjLib.Agaris;
using MjLib.Fuuros;
using MjLib.HandCalculating.Hands;
using MjLib.Shantens;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MjLib;

internal static class Common
{
    public static JsonSerializerOptions JsonOptions => new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}

internal class Output
{
    public object? Result { get; set; }
    public string? Error { get; set; }

    public string ToJson()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        return JsonSerializer.Serialize(this, options);
    }
}

public static class MjCalculator
{
    public static int AGARI_SHANTEN => Shanten.AGARI_SHANTEN;

    public static bool IsAgari(MjTiles hand)
    {
        return Agari.IsAgari(hand.KindList);
    }

    public static string IsAgari(string json)
    {
        var input = JsonNode.Parse(json);
        var tiles = input?["hand"]?.ToTiles();
        var output = new Output();
        if (tiles is not null)
        {
            try
            {
                output.Result = IsAgari(tiles);
            }
            catch (Exception ex)
            {
                output.Error = ex.Message;
            }
        }
        else
        {
            output.Error = "Input JSON is invalid format.";
        }
        return output.ToJson();
    }

    public static int CalcShanten(MjTiles hand)
    {
        return Shanten.Calculate(hand.KindList);
    }

    public static string CalcShanten(string json)
    {
        var input = JsonNode.Parse(json);
        var tiles = input?["hand"]?.ToTiles();
        var output = new Output();
        if (tiles is not null)
        {
            try
            {
                output.Result = CalcShanten(tiles);
            }
            catch (Exception ex)
            {
                output.Error = ex.Message;
            }
        }
        else
        {
            output.Error = "Input JSON is invalid format.";
        }
        return output.ToJson();
    }

    public static MjHandResult CalcHand(
        MjTiles hand,
        MjTiles winTile,
        IEnumerable<MjMeld> melds,
        MjTiles doraIndicators,
        MjTiles uradoraIndicators,
        MjWinSituation situation,
        MjGameRules rules)
    {
        var fuuroList = new FuuroList(melds.Select(x => x.ToInternalModel()));
        var result = HandCalculator.Calculate(
            hand.KindList,
            winTile.KindList[0],
            fuuroList,
            doraIndicators.KindList,
            uradoraIndicators.KindList,
            situation.ToInternalModel(),
            rules.ToInternalModel());
        return result.Error is null
            ? new(result, fuuroList.HasOpen)
            : throw new InvalidOperationException(result.Error);
    }

    public static string CalcHand(string json)
    {
        var input = JsonNode.Parse(json);
        var hand = input?["hand"]?.ToTiles();
        var winTile = input?["winTile"]?.ToTiles();
        var melds = input?["melds"]?.ToMelds();
        var doraIndicators = input?["doraIndicators"]?.ToTiles();
        var uradoraIndicators = input?["uradoraIndicators"]?.ToTiles();
        var situation = input?["situation"]?.ToWinSituation();
        var rules = input?["rules"]?.ToGameRules();
        throw new NotImplementedException();
    }

    #region Internals

    private static MjWinSituation ToWinSituation(this JsonNode situation)
    {
        var res = new MjWinSituation();
        if (situation["tsumo"]?.GetValue<bool>() is bool tsumo) res = res with { Tsumo = tsumo };
        if (situation["riichi"]?.GetValue<bool>() is bool riichi) res = res with { Riichi = riichi };
        if (situation["ippatsu"]?.GetValue<bool>() is bool ippatsu) res = res with { Ippatsu = ippatsu };
        if (situation["rinshan"]?.GetValue<bool>() is bool rinshan) res = res with { Rinshan = rinshan };
        if (situation["chankan"]?.GetValue<bool>() is bool chankan) res = res with { Chankan = chankan };
        if (situation["haitei"]?.GetValue<bool>() is bool haitei) res = res with { Haitei = haitei };
        if (situation["houtei"]?.GetValue<bool>() is bool houtei) res = res with { Houtei = houtei };
        if (situation["daburu_riichi"]?.GetValue<bool>() is bool daburuRiichi) res = res with { DaburuRiichi = daburuRiichi };
        if (situation["nagashimangan"]?.GetValue<bool>() is bool nagashimangan) res = res with { Nagashimangan = nagashimangan };
        if (situation["tenhou"]?.GetValue<bool>() is bool tenhou) res = res with { Tenhou = tenhou };
        if (situation["chiihou"]?.GetValue<bool>() is bool chiihou) res = res with { Chiihou = chiihou };
        if (situation["renhou"]?.GetValue<bool>() is bool renhou) res = res with { Renhou = renhou };
        if (StringToWind(situation["player_wind"]?.GetValue<string>()) is MjWind playerWind) res = res with { PlayerWind = playerWind };
        if (StringToWind(situation["round_wind"]?.GetValue<string>()) is MjWind roundPlayer) res = res with { RoundWind = roundPlayer };
        if (situation["akadora"]?.GetValue<int>() is int akadora) res = res with { Akadora = akadora };
        return res;

        static MjWind? StringToWind(string? wind) => wind switch
        {
            "east" => MjWind.East,
            "south" => MjWind.South,
            "west" => MjWind.West,
            "north" => MjWind.North,
            _ => null,
        };
    }

    private static MjGameRules ToGameRules(this JsonNode rules)
    {
        var res = new MjGameRules();
        if (rules["kuitan"]?.GetValue<bool>() is bool kuitan) res = res with { Kuitan = kuitan };
        return res;
    }

    private static MjTiles? ToTiles(this JsonNode tiles)
    {
        return tiles is JsonArray array ? new MjTiles(array.Select(x => (int)x!))
            : tiles is JsonObject obj ? new MjTiles(
                man: obj["man"]?.ToString(),
                pin: obj["pin"]?.ToString(),
                sou: obj["sou"]?.ToString(),
                honor: obj["honor"]?.ToString())
            : null;
    }

    private static IEnumerable<MjMeld>? ToMelds(this JsonNode melds)
    {
        var mjMelds = melds.AsArray().Select(x => x?.ToMeld());
        return mjMelds is not null && mjMelds.All(x => x is not null) ? mjMelds.OfType<MjMeld>() : null;
    }

    private static MjMeld? ToMeld(this JsonNode meld)
    {
        return meld["type"]?.ToMeldType() is MjMeldType mjMeldType &&
               meld["tiles"]?.ToTiles() is MjTiles mjTiles
            ? new(mjMeldType, mjTiles)
            : null;
    }

    private static MjMeldType? ToMeldType(this JsonNode type)
    {
        return type.ToString() switch
        {
            "chi" => MjMeldType.Chi,
            "pon" => MjMeldType.Pon,
            "ankan" => MjMeldType.Ankan,
            "minkan" => MjMeldType.Minkan,
            "nuki" => MjMeldType.Nuki,
            _ => null
        };
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