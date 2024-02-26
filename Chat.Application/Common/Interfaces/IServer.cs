using Chat.Domain.Entities;

namespace Chat.Application.Common.Interfaces;

public interface IServer
{
    Task<Server> GetAsync(string serverId);
    Task<List<Server>?> GetListAsync(string userId);
    
    Task<string> CreateInvite(string serverId);
    
    Task<bool> CreateAsync(Server server);
    Task<bool> UpdateAsync(string serverId, Server server);
    Task<bool> DeleteAsync(string serverId);
    
    Task<bool> AddUsersAsync(string serverId, List<Participant> participants);
    Task<bool> RemoveUsersAsync(string serverId, List<string> participants);
    
    Task<bool> LeaveAsync(string serverId, string userId);

}
