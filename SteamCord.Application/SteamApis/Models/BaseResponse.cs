using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SteamCord.Application.SteamApis.Models;

public class BaseResponse<T>
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("result")]
    public T? Result { get; set; }
}