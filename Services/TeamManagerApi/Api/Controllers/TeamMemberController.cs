using Api.Models;
using Domain.Models;
using Logic.Services.TeamUseCases.AddMemberUseCase.Interfaces;
using Logic.Services.TeamUseCases.RemoveUserUserCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления участниками команды
/// </summary>
[ApiController]
[Route("api/teams/{teamId}/[controller]")]
public class TeamMembersController : ControllerBase
{
    private readonly IAddMemberUseCase _addMemberUseCase;
    private readonly IRemoveMemberUseCase _removeMemberUseCase;

    public TeamMembersController(
        IAddMemberUseCase addMemberUseCase,
        IRemoveMemberUseCase removeMemberUseCase)
    {
        _addMemberUseCase = addMemberUseCase;
        _removeMemberUseCase = removeMemberUseCase;
    }

    /// <summary>
    /// Добавить игрока в команду
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddMember(Guid teamId, [FromBody] AddTeamMemberRequest request)
    {
        var member = new TeamMember
        {
            UserId = request.UserId,
            Role = request.Role
        };

        await _addMemberUseCase.ExecuteAsync(teamId, member);

        var response = new TeamMemberResponse
        {
            Id = member.Id,
            UserId = member.UserId,
            Role = member.Role
        };

        return Ok(response);
    }

    /// <summary>
    /// Удалить игрока из команды
    /// </summary>
    [HttpDelete("{memberId}")]
    public async Task<IActionResult> RemoveMember(Guid teamId, Guid memberId)
    {
        await _removeMemberUseCase.ExecuteAsync(teamId, memberId);
        return NoContent();
    }
}