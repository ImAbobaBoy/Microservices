using Domain.Models;

namespace Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с командами
/// </summary>
public interface ITeamRepository
{
    /// <summary>
    /// Получаем по айди команды
    /// </summary>
    Task<Team?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Добавляем команду
    /// </summary>
    Task AddAsync(Team team);
    
    /// <summary>
    /// Обновляем команду
    /// </summary>
    Task UpdateAsync(Team team);
    
    /// <summary>
    /// Получаем список всех команд
    /// </summary>
    Task<IEnumerable<Team>> GetAllAsync();
}