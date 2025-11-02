namespace Contracts;

/// <summary>
/// Событие удаления пользователя
/// </summary>
public record UserDeletedEvent(Guid TeamId, Guid UserId);

/// <summary>
/// Событие удаления пользователя из команды
/// </summary>
public record UserRemovedFromTeamsEvent(Guid TeamId, Guid UserId);

/// <summary>
/// Событие добавления пользователя в команду
/// </summary>
public record UserAddedToTeamEvent(Guid TeamId, Guid UserId);

/// <summary>
/// Событие прикрепления пользователя к профилю
/// </summary>
public record UserAttachedToProfileEvent(Guid TeamId, Guid UserId);

