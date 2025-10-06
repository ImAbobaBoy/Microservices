namespace Logic.Services.GetSkillLevel.Interfaces;

/// <summary>
/// Получение уровня игры
/// </summary>
public interface IGetSkillLevelUseCase
{
    Task<string?> ExecuteAsync(Guid userId);
}