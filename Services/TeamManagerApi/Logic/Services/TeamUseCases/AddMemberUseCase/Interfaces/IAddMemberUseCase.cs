using Domain.Models;

namespace Logic.Services.TeamUseCases.AddMemberUseCase.Interfaces;

/// <summary>
/// Добавление игрока в команду
/// </summary>
public interface IAddMemberUseCase
{
    Task ExecuteAsync(Guid teamId, TeamMember member);
}