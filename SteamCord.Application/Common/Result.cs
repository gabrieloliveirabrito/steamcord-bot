namespace SteamCord.Application.Common;

public class Result(bool success, string? error = null)
{
    public bool Success { get; } = success;
    public string? Error { get; } = error;

    public static Result Ok() => new(true, null);
    public static Result Fail(string error) => new(false, error);
}