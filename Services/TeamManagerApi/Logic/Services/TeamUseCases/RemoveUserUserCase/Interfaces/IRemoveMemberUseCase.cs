namespace Logic.Services.TeamUseCases.RemoveUserUserCase.Interfaces;

/// <summary>
/// Удаление игрока из команды
/// </summary>
public interface IRemoveMemberUseCase
{
    Task ExecuteAsync(Guid teamId, Guid memberId);
}