using Logic.Models;

namespace Logic.Services.UpdateProfile.Interfaces;

/// <summary>
/// Обновление пользователя
/// </summary>
public interface IUpdateProfileUseCase
{
    Task<UserDto> ExecuteAsync(Guid userId, string? username, string? vkLink, string? tgLink, string? description);
}