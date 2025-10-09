namespace Logic.Services.TeamUseCases.DeleteTeamUseCase.Interfaces;

/// <summary>
/// Удаление команды
/// </summary>
public interface IDeleteTeamUseCase
{
    Task ExecuteAsync(Guid teamId);
}