using Dal.Interfaces;
using Logic.Services.LogoutUser.Interfaces;

namespace Logic.Services.LogoutUser;

/// <inheritdoc/>
public class LogoutUseCase : ILogoutUseCase
{
    private readonly IRefreshTokenRepository _tokenRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public LogoutUseCase(IRefreshTokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public async Task ExecuteAsync(string refreshToken)
    {
        var token = await _tokenRepository.GetByHashAsync(refreshToken);
        if (token != null)
        {
            token.Revoked = DateTimeOffset.UtcNow;
            await _tokenRepository.UpdateAsync(token);
        }
    }
}