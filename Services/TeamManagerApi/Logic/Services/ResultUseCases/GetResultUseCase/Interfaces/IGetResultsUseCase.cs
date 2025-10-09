using Domain.Models;

namespace Logic.Services.ResultUseCases.GetResultUseCase.Interfaces;

/// <summary>
/// Получение результата
/// </summary>
public interface IGetResultsUseCase
{
    Task<IEnumerable<TeamResult>> ExecuteAsync(Guid teamId);
}