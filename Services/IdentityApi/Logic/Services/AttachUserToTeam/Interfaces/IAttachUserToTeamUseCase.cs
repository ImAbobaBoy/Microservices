namespace Logic.Services.AttachUserToTeam.Interfaces;

/// <summary>
/// Присоединить игрока к команде
/// </summary>
public interface IAttachUserToTeamUseCase
{
    Task ExecuteAsync(Guid userId, Guid teamId);
}