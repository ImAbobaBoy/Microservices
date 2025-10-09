using Domain.Interfaces;
using Logic.Services.ResultUseCases.RemoveResultUseCase.Interfaces;

namespace Logic.Services.ResultUseCases.RemoveResultUseCase;

/// <inheritdoc />
public class RemoveResultUseCase : IRemoveResultUseCase
{
    private readonly ITeamResultRepository _resultRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="resultRepository"></param>
    public RemoveResultUseCase(ITeamResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task ExecuteAsync(Guid resultId)
    {
        await _resultRepository.RemoveAsync(resultId);
    }
}