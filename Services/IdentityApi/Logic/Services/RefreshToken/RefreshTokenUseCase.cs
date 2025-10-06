using CoreLib.Exceptions;
using Dal.Interfaces;
using Logic.Services.RefreshToken.Interfaces;

namespace Logic.Services.RefreshToken;

/// <inheritdoc/>
public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IRefreshTokenRepository _tokenRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RefreshTokenUseCase(IRefreshTokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public async Task<string> ExecuteAsync(string refreshToken)
    {
        var token = await _tokenRepository.GetByHashAsync(refreshToken);
        if (token == null || !token.IsActive)
        {
            throw new InvalidTokenException("Недействительный токен");
        }

        var newAccessToken = Guid.NewGuid().ToString();

        return newAccessToken;
    }
}