using Domain.Models;

namespace Logic.Services.TeamUseCases.CreateTeamUseCase.Interfaces;

/// <summary>
/// Создание команды
/// </summary>
public interface ICreateTeamUseCase
{
    Task<Team> ExecuteAsync(string name, string description);
}