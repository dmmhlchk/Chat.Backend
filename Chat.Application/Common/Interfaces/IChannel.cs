using Chat.Domain.Bases;

namespace Chat.Application.Common.Interfaces;

public interface IChannel
{
    Task<bool> CreateAsync<TChannel>(string serverId, TChannel channel) where TChannel : BaseChannel;
    Task<bool> UpdateAsync<TChannel>(string channelId, TChannel channel) where TChannel : BaseChannel;
    Task<bool> DeleteAsync<TChannel>(string serverId, string channelId) where TChannel : BaseChannel;

    // private settings
    Task<bool> AddParticipants<TChannel>(string channelId, List<string> participants) where TChannel : BaseChannel;
    Task<bool> RemoveParticipants<TChannel>(string channelId, List<string> participants) where TChannel : BaseChannel;
    
    // TODO: restricts
}