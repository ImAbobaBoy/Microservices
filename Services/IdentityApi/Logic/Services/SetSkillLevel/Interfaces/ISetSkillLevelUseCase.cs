namespace Logic.Services.SetSkillLevel.Interfaces;

/// <summary>
/// Установка уровня игры
/// </summary>
public interface ISetSkillLevelUseCase
{
    Task ExecuteAsync(Guid userId, string skillLevel);
}