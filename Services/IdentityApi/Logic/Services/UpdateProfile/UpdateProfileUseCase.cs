using CoreLib.Exceptions;
using Dal.Interfaces;
using Logic.Models;
using Logic.Services.UpdateProfile.Interfaces;

namespace Logic.Services.UpdateProfile;

/// <inheritdoc/>
public class UpdateProfileUseCase : IUpdateProfileUseCase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UpdateProfileUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> ExecuteAsync(Guid userId, string? username, string? vkLink, string? tgLink, string? description)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("Пользователь не найден");
        }

        user.Username = username ?? user.Username;
        user.VkLink = vkLink ?? user.VkLink;
        user.TgLink = tgLink ?? user.TgLink;
        user.Description = description ?? user.Description;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _userRepository.UpdateAsync(user);

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