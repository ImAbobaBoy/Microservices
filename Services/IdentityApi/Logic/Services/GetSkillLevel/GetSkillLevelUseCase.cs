using CoreLib.Exceptions;
using Dal.Interfaces;
using Logic.Services.GetSkillLevel.Interfaces;

namespace Logic.Services.GetSkillLevel;

/// <inheritdoc/>
public class GetSkillLevelUseCase : IGetSkillLevelUseCase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetSkillLevelUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string?> ExecuteAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("Пользователь не найден");
        }

        return user.SkillLevel;
    }
}