using MediatR;

namespace SteamCord.Application.Features.Steam.Responses;

public record CreateAuthTokenResponse(bool Success, string? Error = null, string? Token = null);