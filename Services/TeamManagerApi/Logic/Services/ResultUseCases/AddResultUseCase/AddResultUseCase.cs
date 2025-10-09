using Domain.Interfaces;
using Domain.Models;
using Logic.Services.ResultUseCases.AddResultUseCase.Interfaces;

namespace Logic.Services.ResultUseCases.AddResultUseCase;

/// <inheritdoc />
public class AddResultUseCase : IAddResultUseCase
{
    private readonly ITeamResultRepository _resultRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="resultRepository"></param>
    public AddResultUseCase(ITeamResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task ExecuteAsync(TeamResult result)
    {
        await _resultRepository.AddAsync(result);
    }
}