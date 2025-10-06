namespace Logic.Models;

/// <summary>
/// Запрос на регистрацию
/// </summary>
public class RegisterRequest
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}