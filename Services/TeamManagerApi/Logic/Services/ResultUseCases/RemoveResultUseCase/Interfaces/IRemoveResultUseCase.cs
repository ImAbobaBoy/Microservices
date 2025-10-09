namespace Logic.Services.ResultUseCases.RemoveResultUseCase.Interfaces;

/// <summary>
/// Удаление результата
/// </summary>
public interface IRemoveResultUseCase
{
    Task ExecuteAsync(Guid resultId);
}