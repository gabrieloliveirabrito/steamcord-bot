using Newtonsoft.Json;

namespace SteamCord.Application.Steam.Models;

public class BaseResponse<T>
{
    [JsonProperty("response")]
    public T? Response { get; set; }
}