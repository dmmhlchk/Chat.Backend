using Chat.Domain.Entities;

namespace Chat.Application.Dependencies.IServices;

public interface IRoleService
{
    Task<Role?> GetAsync(string participantId, string serverId, string roleId);
    Task<List<Role>?> GetListAsync(string participantId, string serverId);
    
    Task<bool> CreateAsync(string participantId, string serverId, Role role);
    Task<bool> UpdateAsync(string participantId, string serverId, string roleId, Role role);
    Task<bool> DeleteAsync(string participantId, string serverId, string roleId);
}