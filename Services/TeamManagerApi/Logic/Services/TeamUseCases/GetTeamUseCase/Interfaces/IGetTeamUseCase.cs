using Domain.Models;

namespace Logic.Services.TeamUseCases.GetTeamUseCase.Interfaces;

/// <summary>
/// Получение инфо о команде
/// </summary>
public interface IGetTeamUseCase
{
    Task<Team> ExecuteAsync(Guid teamId);
}