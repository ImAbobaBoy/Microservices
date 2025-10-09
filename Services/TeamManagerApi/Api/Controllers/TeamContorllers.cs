using Api.Models;
using Logic.Services.TeamUseCases.CreateTeamUseCase.Interfaces;
using Logic.Services.TeamUseCases.DeleteTeamUseCase.Interfaces;
using Logic.Services.TeamUseCases.GetTeamUseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для работы с командами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ICreateTeamUseCase _createTeamUseCase;
    private readonly IDeleteTeamUseCase _deleteTeamUseCase;
    private readonly IGetTeamUseCase _getTeamInfoUseCase;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TeamsController(
        ICreateTeamUseCase createTeamUseCase,
        IDeleteTeamUseCase deleteTeamUseCase,
        IGetTeamUseCase getTeamInfoUseCase)
    {
        _createTeamUseCase = createTeamUseCase;
        _deleteTeamUseCase = deleteTeamUseCase;
        _getTeamInfoUseCase = getTeamInfoUseCase;
    }

    /// <summary>
    /// Создать команду
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequest request)
    {
        var team = await _createTeamUseCase.ExecuteAsync(request.Name, request.Description);

        var response = new TeamResponse
        {
            Id = team.Id,
            Name = team.Name,
            Description = team.Description
        };

        return Ok(response);
    }

    /// <summary>
    /// Получить команду
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(Guid id)
    {
        var team = await _getTeamInfoUseCase.ExecuteAsync(id);
        if (team == null)
        {
            return NotFound();
        }

        var response = new TeamResponse
        {
            Id = team.Id,
            Name = team.Name,
            Description = team.Description
        };

        return Ok(response);
    }

    /// <summary>
    /// Удалить команду
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(Guid id)
    {
        await _deleteTeamUseCase.ExecuteAsync(id);
        return NoContent();
    }
}