using Contracts;
using CoreLib.HttpLogic.HttpLogic.Interfaces;
using CoreLib.HttpLogic.HttpLogic.Models;
using Dal.Interfaces;
using Dal.Models;
using Logic.Services.DeleteUser.Interfaces;
using MassTransit;
using ContentType = System.Net.Mime.ContentType;

namespace Logic.Services.DeleteUser;

/// <inheritdoc/>
public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpRequestService _httpRequestService;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Конструктор
    /// </summary>
    public DeleteUserUseCase(IUserRepository userRepository, IHttpRequestService httpRequestService, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _httpRequestService = httpRequestService;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Удаляет пользователя по идентификатору и отвязывает его от всех команд в TeamManager.
    /// </summary>
    public async Task ExecuteAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new InvalidOperationException($"User with id {userId} not found");

        await NotifyTeamManagerAsync(user);

        await _userRepository.DeleteAsync(user);
    }

    /// <summary>
    /// Отправка запроса на TeamManager для удаления пользователя из всех команд
    /// </summary>
    private async Task NotifyTeamManagerAsync(User user)
    {
        await _publishEndpoint.Publish(new UserDeletedEvent(user.TeamId, user.Id));
    }
}