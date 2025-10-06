using Logic.Models;

namespace Logic.Services.RegisterUser.Interfaces;

/// <summary>
/// Регистрация пользователя
/// </summary>
public interface IRegisterUserUseCase
{
    Task<UserDto> ExecuteAsync(string email, string username, string password);
}