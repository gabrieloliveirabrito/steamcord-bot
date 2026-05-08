using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public sealed record SteamPriceOverview
{
    [JsonProperty("currency")]
    public string? Currency { get; init; }

    [JsonProperty("initial")]
    public int Initial { get; init; }

    [JsonProperty("final")]
    public int Final { get; init; }

    [JsonProperty("discountPercent")]
    public int DiscountPercent { get; init; }

    [JsonProperty("finalFormatted")]
    public string? FinalFormatted { get; init; }
}