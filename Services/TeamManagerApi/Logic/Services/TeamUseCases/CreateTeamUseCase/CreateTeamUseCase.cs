using Domain.Interfaces;
using Domain.Models;
using Logic.Services.TeamUseCases.CreateTeamUseCase.Interfaces;

namespace Logic.Services.TeamUseCases.CreateTeamUseCase;

/// <inheritdoc />
public class CreateTeamUseCase : ICreateTeamUseCase
{
    private readonly ITeamRepository _teamRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public CreateTeamUseCase(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Team> ExecuteAsync(string name, string description)
    {
        var team = new Team { Name = name, Description = description };
        await _teamRepository.AddAsync(team);
        
        return team;
    }
}