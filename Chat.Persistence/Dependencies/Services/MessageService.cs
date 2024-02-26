using Chat.Domain.Entities;
using Chat.Persistence.Dependencies.IServices;
using MongoDB.Driver;

namespace Chat.Persistence.Dependencies.Services;

public class MessageService(MongoService service) : IMessageService
{
    public Task<Message> GetInformationAsync(int page)
    {
        throw new NotImplementedException();
    }
    
    

    public async Task<bool> SendAsync(string channelId, Message message)
    {
        var filter = Builders<TextChannel>.Filter.Eq(e => e.id, channelId);
        var update = Builders<TextChannel>.Update.AddToSet(e => e.messages, message);
        
        var textChannelCollection =  service.GetCollection<TextChannel>("TextChannels");
        
        var result = await textChannelCollection.UpdateOneAsync(filter, update);
        
        
        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }

    public async Task<bool> UpdateAsync(string channelId, string messageId, string textContent)
    {
        var filter = Builders<TextChannel>.Filter.And( // TODO: i have to test this one
            Builders<TextChannel>.Filter.Eq(e => e.id, channelId),
            Builders<TextChannel>.Filter.ElemMatch(e => e.messages, msg => msg.id == messageId));
        var update = Builders<TextChannel>.Update.Set("messages.$.textContent", textContent);
        
        var textChannelCollection =  service.GetCollection<TextChannel>("TextChannels");
        
        var result = await textChannelCollection.UpdateOneAsync(filter, update);
        
        
        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync(string channelId, string messageId)
    {
        var filter = Builders<TextChannel>.Filter.Eq(e => e.id, channelId);
        var update = Builders<TextChannel>.Update.PullFilter(e => e.messages, msg => msg.id == messageId);
        
        var textChannelCollection =  service.GetCollection<TextChannel>("TextChannels");
        
        var result = await textChannelCollection.UpdateOneAsync(filter, update);
        
        
        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }
}