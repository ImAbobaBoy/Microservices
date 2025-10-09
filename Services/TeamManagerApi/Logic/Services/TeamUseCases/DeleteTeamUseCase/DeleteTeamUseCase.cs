using Domain.Interfaces;
using Logic.Services.TeamUseCases.DeleteTeamUseCase.Interfaces;

namespace Logic.Services.TeamUseCases.DeleteTeamUseCase;

/// <inheritdoc />
public class DeleteTeamUseCase : IDeleteTeamUseCase
{
    private readonly ITeamRepository _teamRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public DeleteTeamUseCase(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task ExecuteAsync(Guid teamId)
    {
        var team = await _teamRepository.GetByIdAsync(teamId);
        if (team != null)
        {
            team.IsDeleted = true; 
            await _teamRepository.UpdateAsync(team);
        }
    }
}