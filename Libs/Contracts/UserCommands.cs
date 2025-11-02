namespace Contracts;

/// <summary>
/// Команда на удаление пользователя из команды
/// </summary>
public record RemoveUserFromTeamsCommand(Guid TeamId, Guid UserId);

/// <summary>
/// Команда на добавление пользователя в команду
/// </summary>
public record AddUserToTeamCommand(Guid TeamId, Guid UserId);

/// <summary>
/// Команда на привязку пользователя к профилю
/// </summary>
public record AttachUserToProfileCommand(Guid TeamId, Guid UserId);

