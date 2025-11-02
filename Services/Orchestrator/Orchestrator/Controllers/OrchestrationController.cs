using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Contracts;

namespace OrchestratorService.Controllers;

/// <summary>
/// Контроллер оркестрации операций с пользователями
/// </summary>
[ApiController]
[Route("api/orchestrator")]
public class OrchestrationController : ControllerBase
{
    private readonly IPublishEndpoint _publish;

    /// <summary>
    /// Конструктор
    /// </summary>
    public OrchestrationController(IPublishEndpoint publish)
    {
        _publish = publish;
    }

    /// <summary>
    /// Добавляет пользователя в команду через оркестратор
    /// </summary>
    [HttpPost("add-user")]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest req)
    {
        if (req.TeamId == Guid.Empty || req.UserId == Guid.Empty)
            return BadRequest();

        await _publish.Publish(new AddUserToTeamCommand(req.TeamId, req.UserId));
        return Accepted();
    }

    /// <summary>
    /// Модель запроса на добавление пользователя в команду
    /// </summary>
    public record AddUserRequest(Guid TeamId, Guid UserId);
}