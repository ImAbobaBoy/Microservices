using System.Net;
using System.Text;
using System.Text.Json;
using MassTransit;
using Contracts;
using Orchestrator.Services.Interfaces;

namespace OrchestratorService.Services;

/// <inheritdoc/>
public class OrchestratorUseCase : IOrchestratorUseCase
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly IPublishEndpoint _publish;

    /// <summary>
    /// Конструктор
    /// </summary>
    public OrchestratorUseCase(IHttpClientFactory httpFactory, IPublishEndpoint publish)
    {
        _httpFactory = httpFactory;
        _publish = publish;
    }

    /// <inheritdoc/>
    public async Task<bool> ExecuteAsync(Guid teamId, Guid userId)
    {
        try
        {
            var identityClient = _httpFactory.CreateClient("Identity");
            var checkResp = await identityClient.GetAsync($"/v1/profiles/{userId}");
            if (checkResp.StatusCode == HttpStatusCode.NotFound)
            {
                await _publish.Publish(new UserAddedToTeamEvent(teamId, userId));
                return false;
            }
            checkResp.EnsureSuccessStatusCode();

            var teamClient = _httpFactory.CreateClient("TeamManager");
            var addBody = JsonSerializer.Serialize(new { UserId = userId, Role = "Member" });
            var addResp = await teamClient.PostAsync(
                $"/api/teams/{teamId}/teammembers",
                new StringContent(addBody, Encoding.UTF8, "application/json")
            );
            addResp.EnsureSuccessStatusCode();

            var attachResp = await identityClient.PostAsync($"/v1/teams/{teamId}/users/{userId}", null);
            attachResp.EnsureSuccessStatusCode();

            await _publish.Publish(new UserAddedToTeamEvent(teamId, userId));

            return true;
        }
        catch (Exception ex)
        {
            await _publish.Publish(new UserAttachedToProfileEvent(teamId, userId));
            throw;
        }
    }
}
