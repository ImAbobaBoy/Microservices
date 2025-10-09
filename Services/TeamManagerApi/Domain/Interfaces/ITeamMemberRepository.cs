using Domain.Models;

namespace Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с участниками команды
/// </summary>
public interface ITeamMemberRepository
{
    Task<TeamMember?> GetByIdAsync(Guid id);
    
    Task AddAsync(TeamMember member);
    
    Task RemoveAsync(Guid id);
    
    Task<IEnumerable<TeamMember>> GetByTeamIdAsync(Guid teamId);
}