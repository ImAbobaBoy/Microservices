using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Models;
using Logic.Models;
using Logic.Services.GetProfile.Interfaces;
using Logic.Services.GetSkillLevel.Interfaces;
using Logic.Services.SetSkillLevel.Interfaces;
using Logic.Services.UpdateProfile.Interfaces;

/// <summary>
/// Управление профилями пользователей
/// </summary>
[ApiController]
[Route("v1/profiles")]
public class ProfilesController : ControllerBase
{
    private readonly IGetProfileUseCase _getProfile;
    private readonly IUpdateProfileUseCase _updateProfile;
    private readonly ISetSkillLevelUseCase _setSkillLevel;
    private readonly IGetSkillLevelUseCase _getSkillLevel;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ProfilesController(
        IGetProfileUseCase getProfile,
        IUpdateProfileUseCase updateProfile,
        ISetSkillLevelUseCase setSkillLevel,
        IGetSkillLevelUseCase getSkillLevel)
    {
        _getProfile = getProfile;
        _updateProfile = updateProfile;
        _setSkillLevel = setSkillLevel;
        _getSkillLevel = getSkillLevel;
    }

    /// <summary>
    /// Получить профиль определенного пользователя
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetProfile(Guid id)
    {
        var user = await _getProfile.ExecuteAsync(id);
        return Ok(user);
    }

    /// <summary>
    /// Обновить профиль пользователя
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateProfile(Guid id, [FromBody] UpdateProfileRequest request)
    {
        var updated = await _updateProfile.ExecuteAsync(id, request.Username, request.VkLink, request.TgLink, request.Description);
        return Ok(updated);
    }

    /// <summary>
    /// Получить профиль пользователя, от кого выполнен вход в аккаунт
    /// </summary>
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMyProfile()
    {
        var userId = GetUserIdFromClaims();
        var user = await _getProfile.ExecuteAsync(userId);
        return Ok(user);
    }

    /// <summary>
    /// Обновление собственного профиля пользователя
    /// </summary>
    [HttpPatch("me")]
    public async Task<ActionResult<UserDto>> UpdateMyProfile([FromBody] UpdateProfileRequest request)
    {
        var userId = GetUserIdFromClaims();
        var updated = await _updateProfile.ExecuteAsync(userId, request.Username, request.VkLink, request.TgLink, request.Description);
        return Ok(updated);
    }

    /// <summary>
    /// Установка уровня игры пользователя
    /// </summary>
    [HttpPatch("me/skill-level")]
    public async Task<IActionResult> SetMySkillLevel([FromBody] SetSkillLevelRequest request)
    {
        var userId = GetUserIdFromClaims();
        await _setSkillLevel.ExecuteAsync(userId, request.SkillLevel);
        return NoContent();
    }

    /// <summary>
    /// Получение уровня игры пользователя
    /// </summary>
    [HttpGet("{id}/skill-level")]
    public async Task<ActionResult<string?>> GetSkillLevel(Guid id)
    {
        var skillLevel = await _getSkillLevel.ExecuteAsync(id);
        return Ok(skillLevel);
    }

    /// <summary>
    /// Получение айди пользователя из клаймсов
    /// </summary>
    private Guid GetUserIdFromClaims()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            throw new Exception("User not authenticated");
        }
        return Guid.Parse(userIdClaim);
    }
}
