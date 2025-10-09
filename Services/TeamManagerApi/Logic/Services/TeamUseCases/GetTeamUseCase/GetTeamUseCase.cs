using Domain.Interfaces;
using Domain.Models;
using Logic.Services.TeamUseCases.GetTeamUseCase.Interfaces;

namespace Logic.Services.TeamUseCases.GetTeamUseCase;

/// <inheritdoc />
public class GetTeamUseCase : IGetTeamUseCase
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITeamMemberRepository _memberRepository;
    private readonly ITeamResultRepository _resultRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetTeamUseCase(
        ITeamRepository teamRepository,
        ITeamMemberRepository memberRepository,
        ITeamResultRepository resultRepository)
    {
        _teamRepository = teamRepository;
        _memberRepository = memberRepository;
        _resultRepository = resultRepository;
    }

    public async Task<Team?> ExecuteAsync(Guid teamId)
    {
        var team = await _teamRepository.GetByIdAsync(teamId);
        if (team != null)
        {
            team.Members = (await _memberRepository.GetByTeamIdAsync(teamId))?.ToList() 
                           ?? new List<TeamMember>();
            team.Results = (await _resultRepository.GetByTeamIdAsync(teamId))?.ToList() 
                           ?? new List<TeamResult>();
        }
        
        return team;
    }
}