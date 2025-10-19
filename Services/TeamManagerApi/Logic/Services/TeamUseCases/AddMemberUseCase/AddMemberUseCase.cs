﻿using System.Net;
using CoreLib.HttpLogic.HttpLogic.Interfaces;
using CoreLib.HttpLogic.HttpLogic.Models;
using Domain.Interfaces;
using Domain.Models;
using Logic.Services.TeamUseCases.AddMemberUseCase.Interfaces;

namespace Logic.Services.TeamUseCases.AddMemberUseCase;

/// <summary>
/// UseCase для добавления участника в команду.
/// Перед добавлением проверяет существование профиля пользователя через ProfileService.
/// </summary>
public class AddMemberUseCase : IAddMemberUseCase
{
    private readonly ITeamMemberRepository _memberRepo;
    private readonly IHttpRequestService _httpRequestService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AddMemberUseCase(
        ITeamMemberRepository memberRepo,
        IHttpRequestService httpRequestService)
    {
        _memberRepo = memberRepo;
        _httpRequestService = httpRequestService;
    }

    /// <summary>
    /// Добавляет участника в команду, если профиль пользователя существует.
    /// </summary>
    public async Task ExecuteAsync(Guid teamId, TeamMember member)
    {
        var profileExists = await CheckProfileExistsAsync(member.UserId);

        if (!profileExists)
        {
            throw new InvalidOperationException($"User with id {member.UserId} not found in ProfileService");
        }

        member.TeamId = teamId;
        await _memberRepo.AddAsync(member);
    }

    /// <summary>
    /// Проверяет существование профиля пользователя через ProfileService.
    /// </summary>
    private async Task<bool> CheckProfileExistsAsync(Guid userId)
    {
        var request = new HttpRequestData
        {
            Uri = new Uri($"http://localhost:31719/v1/profiles/{userId}"),
            Method = HttpMethod.Get,
            ContentType = ContentType.ApplicationJson
        };

        try
        {
            var response = await _httpRequestService.SendRequestAsync<UserDto>(request);
            return response.IsSuccessStatusCode && response.Body is not null;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    /// <summary>
    /// DTO для пользователя, получаемого из ProfileService.
    /// </summary>
    private record UserDto(Guid Id, string Username);
}
