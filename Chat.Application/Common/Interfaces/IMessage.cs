using Chat.Domain.Entities;

namespace Chat.Application.Common.Interfaces;

public interface IMessage
{
    Task<Message> GetInformationAsync(int page);
    
    Task<bool> SendAsync( string channelId, Message message);
    Task<bool> UpdateAsync(string channelId, string messageId, string textContent);
    Task<bool> DeleteAsync(string channelId, string messageId);
    
}