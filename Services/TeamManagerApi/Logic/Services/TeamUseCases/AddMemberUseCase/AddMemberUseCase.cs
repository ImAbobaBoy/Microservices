using Domain.Interfaces;
using Domain.Models;
using Logic.Services.TeamUseCases.AddMemberUseCase.Interfaces;

namespace Logic.Services.TeamUseCases.AddMemberUseCase;

/// <inheritdoc />
public class AddMemberUseCase : IAddMemberUseCase
{
    private readonly ITeamMemberRepository _memberRepo;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AddMemberUseCase(ITeamMemberRepository memberRepo)
    {
        _memberRepo = memberRepo;
    } 

    public async Task ExecuteAsync(Guid teamId, TeamMember member)
    {
        member.TeamId = teamId;
        await _memberRepo.AddAsync(member);
    }
}