using CoreLib.Exceptions;
using Dal.Interfaces;
using Logic.Services.SetSkillLevel.Interfaces;

namespace Logic.Services.SetSkillLevel;

/// <inheritdoc/>
public class SetSkillLevelUseCase : ISetSkillLevelUseCase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public SetSkillLevelUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(Guid userId, string skillLevel)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("Пользователь не найден");
        }

        user.SkillLevel = skillLevel;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _userRepository.UpdateAsync(user);
    }
}