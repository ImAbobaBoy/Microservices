namespace Api.Models;

/// <summary>
/// Запрос на регистрацию
/// </summary>
public record RegisterRequest(string Email, string Username, string Password);