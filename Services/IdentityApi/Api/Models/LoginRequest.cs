namespace Api.Models;

/// <summary>
/// Запрос на логин
/// </summary>
public record LoginRequest(string EmailOrUsername, string Password);