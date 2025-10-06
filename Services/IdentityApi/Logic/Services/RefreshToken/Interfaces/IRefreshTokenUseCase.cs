namespace Logic.Services.RefreshToken.Interfaces;

/// <summary>
/// Обновление токена
/// </summary>
public interface IRefreshTokenUseCase 
{
    Task<string> ExecuteAsync(string refreshToken);
}