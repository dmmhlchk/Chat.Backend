using Chat.Domain.Entities;

namespace Chat.Application.Dependencies.IServices;

public interface IServerService
{
    Task<Server> GetAsync(string serverId);
    Task<List<Server>?> GetListAsync(string userId);
    
    Task<string> CreateInvite(string participantId, string serverId);
    
    Task<bool> CreateAsync(Server server);
    Task<bool> UpdateAsync(string participantId, string serverId, Server server);
    Task<bool> DeleteAsync(string participantId, string serverId);
    
    Task<bool> AddUsersAsync(string participantId, string serverId, List<Participant> participants);
    Task<bool> RemoveUsersAsync(string participantId, string serverId, List<string> participants);
    
    Task<bool> LeaveAsync(string serverId, string userId);
}