using CoreLib.Exceptions;
using Dal.Interfaces;
using Dal.Models;
using Logic.Models;
using Logic.Services.GetProfile.Interfaces;

namespace Logic.Services.GetProfile;

/// <inheritdoc/>
public class GetProfileUseCase : IGetProfileUseCase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetProfileUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> ExecuteAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("Пользователь не найден");
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            VkLink = user.VkLink,
            TgLink = user.TgLink,
            Description = user.Description,
            SkillLevel = user.SkillLevel
        };
    }
}