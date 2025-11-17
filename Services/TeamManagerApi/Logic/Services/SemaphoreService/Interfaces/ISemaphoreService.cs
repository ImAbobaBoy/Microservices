using DistributedLocking.Interfaces;

namespace Logic.Services.SemaphoreService.Interfaces;

/// <summary>
/// Сервис для сефамора
/// </summary>
public interface ISemaphoreService
{
    /// <summary>
    /// Получить семафор
    /// </summary>
    IDistributedSemaphore GetSemaphore();
}