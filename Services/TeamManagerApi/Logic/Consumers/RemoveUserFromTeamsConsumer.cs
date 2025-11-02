using Contracts;
using Logic.Services.TeamUseCases.RemoveUserUserCase.Interfaces;
using MassTransit;

namespace Logic.Consumers;

/// <summary>
/// Консюмер для удаления пользователя из команды по команде RemoveUserFromTeamsCommand
/// </summary>
public class RemoveUserFromTeamsConsumer : IConsumer<RemoveUserFromTeamsCommand>
{
    private readonly IRemoveMemberUseCase _removeUseCase;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveUserFromTeamsConsumer(IRemoveMemberUseCase removeUseCase)
    {
        _removeUseCase = removeUseCase;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<RemoveUserFromTeamsCommand> context)
    {
        await _removeUseCase.ExecuteAsync(context.Message.TeamId, context.Message.UserId);

        await context.Publish(new UserRemovedFromTeamsEvent(context.Message.TeamId, context.Message.UserId));
    }
}

