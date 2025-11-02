using Domain.Interfaces;
using Logic.Services.TeamUseCases.RemoveUserUserCase.Interfaces;

namespace Logic.Services.TeamUseCases.RemoveUserUserCase;

/// <inheritdoc />
public class RemoveMemberUseCase : IRemoveMemberUseCase
{
    private readonly ITeamMemberRepository _memberRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveMemberUseCase(ITeamMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task ExecuteAsync(Guid teamId, Guid userId)
    {
        var member = await _memberRepository.GetByUserIdAsync(userId);
        if (member != null && member.TeamId == teamId)
        {
            await _memberRepository.RemoveAsync(member.Id); 
        }
    }
}