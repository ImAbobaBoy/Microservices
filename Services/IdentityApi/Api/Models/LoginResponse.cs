using Logic.Models;

namespace Api.Models;

/// <summary>
/// Ответ при логине
/// </summary>
public record LoginResponse(UserDto User, string AccessToken, string RefreshToken);