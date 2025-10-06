namespace Api.Models;

/// <summary>
/// Запрос на изменение пользователя
/// </summary>
public record UpdateProfileRequest(string? Username, string? VkLink, string? TgLink, string? Description);