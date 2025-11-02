using MassTransit;
using Contracts;

/// <summary>
/// Сага для удаления пользователя и его отвязки от команд
/// </summary>
public class DeleteUserSaga : MassTransitStateMachine<DeleteUserState>
{
    public State WaitingForTeamRemoval { get; private set; }
    public Event<UserDeletedEvent> UserDeleted { get; private set; }
    public Event<UserRemovedFromTeamsEvent> UserRemovedFromTeams { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public DeleteUserSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UserDeleted, x =>
        {
            x.CorrelateById(m => m.Message.UserId);
            x.SelectId(context => context.Message.UserId); 
        });

        Event(() => UserRemovedFromTeams, x =>
        {
            x.CorrelateById(context => context.Message.UserId);
        });

        Initially(
            When(UserDeleted)
                .Then(context =>
                {
                    context.Instance.TeamId = context.Data.TeamId;

                    context.Publish(new RemoveUserFromTeamsCommand(context.Instance.TeamId, context.Data.UserId));
                })
                .TransitionTo(WaitingForTeamRemoval)
        );

        During(WaitingForTeamRemoval,
            When(UserRemovedFromTeams)
                .Then(context => Console.WriteLine($"User {context.Data.UserId} removed from all teams"))
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}

/// <summary>
/// Состояние саги удаления пользователя
/// </summary>
public class DeleteUserState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid TeamId { get; set; }
}