using Api.Models;
using Logic.Models;
using Logic.Services.LoginUser.Interfaces;
using Logic.Services.LogoutUser.Interfaces;
using Logic.Services.RefreshToken.Interfaces;
using Logic.Services.RegisterUser.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Logic.Models.RegisterRequest;

/// <summary>
/// Управление авторизацией и аутентификацией
/// </summary>
[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUserUseCase _registerUseCase;
    private readonly ILoginUserUseCase _loginUseCase;
    private readonly IRefreshTokenUseCase _refreshUseCase;
    private readonly ILogoutUseCase _logoutUseCase;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AuthController(
        IRegisterUserUseCase registerUseCase,
        ILoginUserUseCase loginUseCase,
        IRefreshTokenUseCase refreshUseCase,
        ILogoutUseCase logoutUseCase)
    {
        _registerUseCase = registerUseCase;
        _loginUseCase = loginUseCase;
        _refreshUseCase = refreshUseCase;
        _logoutUseCase = logoutUseCase;
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest request)
    {
        var user = await _registerUseCase.ExecuteAsync(request.Email, request.Username, request.Password);
        return Ok(user);
    }

    /// <summary>
    /// Вход в аккаунт
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var (user, accessToken, refreshToken) = await _loginUseCase.ExecuteAsync(request.EmailOrUsername, request.Password);
        return Ok(new LoginResponse(user, accessToken, refreshToken));
    }

    /// <summary>
    /// Обновление токена доступа
    /// </summary>
    [HttpPost("refresh")]
    public async Task<ActionResult<string>> Refresh([FromBody] string refreshToken)
    {
        var newAccessToken = await _refreshUseCase.ExecuteAsync(refreshToken);
        return Ok(newAccessToken);
    }

    /// <summary>
    /// Выход из аккаунта
    /// </summary>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        await _logoutUseCase.ExecuteAsync(refreshToken);
        return NoContent();
    }
}