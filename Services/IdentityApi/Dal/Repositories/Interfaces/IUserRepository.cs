using Dal.Models;

namespace Dal.Interfaces;

/// <summary>
/// Репозиторий для работы с моделью пользователя
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Добавление токена в БД
    /// </summary>
    Task AddAsync(User user);
    
    /// <summary>
    /// Получение пользователя по почте
    /// </summary>
    Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Получение пользователя по айди
    /// </summary>
    Task<User?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Получение пользователя по юзернейму
    /// </summary>
    Task<User?> GetByUsernameAsync(string username);
    
    /// <summary>
    /// Обновление пользователя
    /// </summary>
    Task UpdateAsync(User user);
    
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    Task DeleteAsync(User user);
}