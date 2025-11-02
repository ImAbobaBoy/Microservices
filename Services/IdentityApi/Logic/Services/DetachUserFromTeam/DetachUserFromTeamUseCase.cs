using Dal.Interfaces;
using Logic.Services.DetachUserFromTeam.Interfaces;

namespace Logic.Services.DetachUserFromTeam;

/// <inheritdoc/>
public class DetachUserFromTeamUseCase : IDetachUserFromTeamUseCase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public DetachUserFromTeamUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Отвязывает пользователя от команды (устанавливает TeamId = null).
    /// </summary>
    public async Task ExecuteAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException($"User with id {userId} not found");

        user.TeamId = Guid.Empty;

        await _userRepository.UpdateAsync(user);
    }
}