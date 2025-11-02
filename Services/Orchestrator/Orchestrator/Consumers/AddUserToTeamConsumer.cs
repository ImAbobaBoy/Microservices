using MassTransit;
using Contracts;
using Orchestrator.Services.Interfaces;

namespace OrchestratorService.Consumers;

/// <summary>
/// Consumer для добавления пользователя в команду через оркестратор
/// </summary>
public class AddUserToTeamConsumer : IConsumer<AddUserToTeamCommand>
{
    private readonly IOrchestratorUseCase _useCase;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AddUserToTeamConsumer(IOrchestratorUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task Consume(ConsumeContext<AddUserToTeamCommand> context)
    {
        var teamId = context.Message.TeamId;
        var userId = context.Message.UserId;

        await _useCase.ExecuteAsync(teamId, userId);
    }
}