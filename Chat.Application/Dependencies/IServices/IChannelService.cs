using Chat.Domain.Bases;

namespace Chat.Application.Dependencies.IServices;

public interface IChannelService<in TChannel> 
    where TChannel : BaseChannel
{
    Task<bool> CreateAsync(string participantId, string serverId,  TChannel channel);
    Task<bool> UpdateAsync(string participantId, string channelId, TChannel channel);
    Task<bool> DeleteAsync(string participantId, string serverId, string channelId);

    // private settings
    Task<bool> AddParticipants(string participantId, string channelId, List<string> participants);
    Task<bool> RemoveParticipants(string participantId, string channelId, List<string> participants);
    
    // TODO: restricts
}