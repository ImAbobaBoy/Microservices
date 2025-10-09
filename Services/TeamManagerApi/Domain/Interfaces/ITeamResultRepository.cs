using Domain.Models;

namespace Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с результатами команды
/// </summary>
public interface ITeamResultRepository
{
    /// <summary>
    /// Добавляем результат
    /// </summary>
    Task AddAsync(TeamResult result);
    
    /// <summary>
    /// Получаем результат
    /// </summary>
    Task<IEnumerable<TeamResult>> GetByTeamIdAsync(Guid teamId);
    
    /// <summary>
    /// Удаляем результат
    /// </summary>
    Task RemoveAsync(Guid resultId);
}