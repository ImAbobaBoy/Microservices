using System.Security.Claims;
using Logic.Services.AttachUserToTeam.Interfaces;
using Logic.Services.DetachUserFromTeam.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Управление участниками команд.
/// </summary>
[ApiController]
[Route("v1/teams")]
public class TeamsController : ControllerBase
{
    private readonly IAttachUserToTeamUseCase _attachUserToTeam;
    private readonly IDetachUserFromTeamUseCase _detachUserFromTeam;

    /// <summary>
    /// Конструктор контроллера команд.
    /// </summary>
    public TeamsController(
        IAttachUserToTeamUseCase attachUserToTeam,
        IDetachUserFromTeamUseCase detachUserFromTeam)
    {
        _attachUserToTeam = attachUserToTeam;
        _detachUserFromTeam = detachUserFromTeam;
    }

    /// <summary>
    /// Добавляет пользователя в команду.
    /// </summary>
    [HttpPost("{teamId}/users/{userId}")]
    public async Task<IActionResult> AttachUserToTeam(Guid teamId, Guid userId)
    {
        await _attachUserToTeam.ExecuteAsync(userId, teamId);
        return NoContent();
    }

    /// <summary>
    /// Удаляет пользователя из команды.
    /// </summary>
    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DetachUserFromTeam(Guid userId)
    {
        await _detachUserFromTeam.ExecuteAsync(userId);
        return NoContent();
    }
}
