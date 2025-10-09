using Domain.Interfaces;
using Domain.Models;
using Logic.Services.ResultUseCases.GetResultUseCase.Interfaces;

namespace Logic.Services.ResultUseCases.GetResultUseCase;

/// <inheritdoc />
public class GetResultsUseCase : IGetResultsUseCase
{
    private readonly ITeamResultRepository _resultRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="resultRepository"></param>
    public GetResultsUseCase(ITeamResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<IEnumerable<TeamResult>> ExecuteAsync(Guid teamId)
    {
        return await _resultRepository.GetByTeamIdAsync(teamId);
    }
}