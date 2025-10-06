using Logic.Models;

namespace Logic.Services.GetProfile.Interfaces;

/// <summary>
/// Получение пользователя из БД
/// </summary>
public interface IGetProfileUseCase
{
    Task<UserDto> ExecuteAsync(Guid userId);
}