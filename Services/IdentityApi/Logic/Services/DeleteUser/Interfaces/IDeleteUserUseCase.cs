namespace Logic.Services.DeleteUser.Interfaces;

/// <summary>
/// Удаление пользователя
/// </summary>
public interface IDeleteUserUseCase
{
    Task ExecuteAsync(Guid userId);
}