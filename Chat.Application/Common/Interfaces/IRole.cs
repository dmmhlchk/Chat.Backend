using Chat.Domain.Entities;

namespace Chat.Application.Common.Interfaces;

public interface IRole
{
    Task<Role?> GetAsync(string serverId, string roleId);
    Task<List<Role>?> GetListAsync(string serverId);
    
    Task<bool> CreateAsync(string serverId, Role role);
    Task<bool> UpdateAsync(string serverId, string roleId, Role role);
    Task<bool> DeleteAsync(string serverId, string roleId);
}