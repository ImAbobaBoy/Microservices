namespace Logic.Services.DetachUserFromTeam.Interfaces;

/// <summary>
/// Отсоединить игркоа от команды
/// </summary>
public interface IDetachUserFromTeamUseCase
{
    Task ExecuteAsync(Guid userId);
}