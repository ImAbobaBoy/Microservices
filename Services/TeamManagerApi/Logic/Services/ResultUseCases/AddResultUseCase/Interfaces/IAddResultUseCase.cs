using Domain.Models;

namespace Logic.Services.ResultUseCases.AddResultUseCase.Interfaces;

/// <summary>
/// Добавление результата
/// </summary>
public interface IAddResultUseCase
{
    Task ExecuteAsync(TeamResult result);
}