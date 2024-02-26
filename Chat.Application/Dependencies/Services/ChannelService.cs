using Chat.Application.Dependencies.IServices;
using Chat.Domain.Bases;

namespace Chat.Application.Dependencies.Services;

public class ChannelService<TChannel>() : 
    IChannelService<TChannel> where TChannel : BaseChannel
{
    public Task<bool> CreateAsync(string participantId, string serverId, TChannel channel)
    {
        // only members with special permission can create a channel
        // get a permission by participantId
        
        // check it
        
        // create channel
        // add channel to server
        //return true
        
        // return false
        
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(string participantId, string channelId, TChannel channel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string participantId, string serverId, string channelId)
    {
        throw new NotImplementedException();
    }
    
    
    
    public Task<bool> AddParticipants(string participantId, string channelId, List<string> participants)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveParticipants(string participantId, string channelId, List<string> participants)
    {
        throw new NotImplementedException();
    }
}