using Api.Models;
using Domain.Models;
using Logic.Services.ResultUseCases.AddResultUseCase.Interfaces;
using Logic.Services.ResultUseCases.GetResultUseCase.Interfaces;
using Logic.Services.ResultUseCases.RemoveResultUseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления результатами команды
/// </summary>
[ApiController]
[Route("api/teams/{teamId}/[controller]")]
public class TeamResultsController : ControllerBase
{
    private readonly IAddResultUseCase _addResultUseCase;
    private readonly IRemoveResultUseCase _removeResultUseCase;
    private readonly IGetResultsUseCase _getResultsUseCase;

    public TeamResultsController(
        IAddResultUseCase addResultUseCase,
        IRemoveResultUseCase removeResultUseCase,
        IGetResultsUseCase getResultsUseCase)
    {
        _addResultUseCase = addResultUseCase;
        _removeResultUseCase = removeResultUseCase;
        _getResultsUseCase = getResultsUseCase;
    }

    /// <summary>
    /// Добавить результат игры между командами
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddResult(Guid teamId, [FromBody] AddTeamResultRequest request)
    {
        var result = new TeamResult
        {
            TeamId = teamId,
            Description = request.Description,
        };

        await _addResultUseCase.ExecuteAsync(result);

        var response = new TeamResultResponse
        {
            Id = result.Id,
            Description = result.Description,
        };

        return Ok(response);
    }

    /// <summary>
    /// Получить результат
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetResults(Guid teamId)
    {
        var results = await _getResultsUseCase.ExecuteAsync(teamId);
        var response = results.Select(r => new TeamResultResponse
        {
            Id = r.Id,
            Description = r.Description,
        });

        return Ok(response);
    }

    /// <summary>
    /// Удалить результат команды
    /// </summary>
    [HttpDelete("{resultId}")]
    public async Task<IActionResult> RemoveResult(Guid teamId, Guid resultId)
    {
        await _removeResultUseCase.ExecuteAsync(resultId);
        return NoContent();
    }
}