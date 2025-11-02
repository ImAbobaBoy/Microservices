using Dal.Interfaces;
using Logic.Services.AttachUserToTeam.Interfaces;

namespace Logic.Services.AttachUserToTeam;

/// <inheritdoc/>
public class AttachUserToTeamUseCase : IAttachUserToTeamUseCase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="userRepository">Репозиторий пользователей.</param>
    public AttachUserToTeamUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Привязывает пользователя к команде.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="teamId">Идентификатор команды.</param>
    /// <exception cref="InvalidOperationException">Если пользователь не найден.</exception>
    public async Task ExecuteAsync(Guid userId, Guid teamId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException($"User with id {userId} not found");

        user.TeamId = teamId;

        await _userRepository.UpdateAsync(user);
    }
}