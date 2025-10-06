using Dal.Models;

namespace Dal.Interfaces;

/// <summary>
/// Репозиторий для работы с моделью рефреш токена
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Добавление токена в БД
    /// </summary>
    Task AddAsync(RefreshToken token);
    
    /// <summary>
    /// Получение токена по хешу
    /// </summary>
    Task<RefreshToken?> GetByHashAsync(string tokenHash);
    
    /// <summary>
    /// Обновление токена
    /// </summary>
    Task UpdateAsync(RefreshToken token);
}