namespace Orchestrator.Services.Interfaces;

/// <summary>
/// Интерфейс юзкейса оркестратора для добавления пользователя в команду
/// </summary>
public interface IOrchestratorUseCase
{
    /// <summary>
    /// Выполняет добавление пользователя в команду
    /// </summary>
    Task<bool> ExecuteAsync(Guid teamId, Guid userId);
}