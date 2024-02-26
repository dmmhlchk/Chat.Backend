using Chat.Domain.Entities;

namespace Chat.Application.Dependencies.IServices;

public interface IMessageService
{
    Task<Message> GetInformationAsync(int page);
    Task ReadMessage(string participantId, string channelId, string messageId);
    
    Task<bool> SendAsync(string channelId, Message message);
    Task<bool> UpdateAsync(string participantId, string channelId, string messageId, string textContent);
    Task<bool> DeleteAsync(string participantId, string channelId, string messageId);

}