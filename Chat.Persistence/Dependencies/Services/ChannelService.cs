using Chat.Domain.Bases;
using Chat.Domain.Entities;
using Chat.Persistence.Dependencies.IServices;
using MongoDB.Driver;

namespace Chat.Persistence.Dependencies.Services;

public class ChannelService(MongoService service) : IChannelService
{
    public async Task<bool> CreateAsync<TChannel>(string serverId, TChannel channel) where TChannel : BaseChannel
    {
        var channelCollection = service.GetCollection<TChannel>(nameof(TChannel) + "s");
        await channelCollection.InsertOneAsync(channel);


        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var serverUpdate = Builders<Server>.Update;
        
        var serverCollection = service.GetCollection<Server>("Servers");

        if (channel is TextChannel)
            await serverCollection
                .UpdateOneAsync(
                    serverFilter,
                    serverUpdate.AddToSet(e => e.textChannels, channel.id));
        
        else if (channel is VoiceChannel)
            await serverCollection
                .UpdateOneAsync(
                    serverFilter,
                    serverUpdate.AddToSet(e => e.voiceChannels, channel.id));

        
        return true;
    }

    public async Task<bool> UpdateAsync<TChannel>(string channelId, TChannel channel) where TChannel : BaseChannel
    {
        var filter = Builders<TChannel>.Filter.Eq(e => e.id, channelId);
        var update = Builders<TChannel>.Update
            .Set(e => e.title, channel.title)
            .Set(e => e.isPrivate, channel.isPrivate);
        
        var channelCollection =  service.GetCollection<TChannel>(nameof(TChannel) + "s");
        
        var result = await channelCollection.UpdateOneAsync(filter, update);
        
        
        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync<TChannel>(string serverId, string channelId) where TChannel : BaseChannel
    {
        var filter = Builders<TChannel>.Filter.Eq(e => e.id, channelId);
        var channelCollection = service.GetCollection<TChannel>(nameof(TChannel) + "s");

        var result = await channelCollection.DeleteOneAsync(filter);
        
        
        if (result.DeletedCount > 0) 
            return true;

        return false;
        
    }
    

    public async Task<bool> AddParticipants<TChannel>(string channelId, List<string> participants) where TChannel : BaseChannel
    {
        var filter = Builders<TChannel>.Filter.Eq(e => e.id, channelId);
        var update = Builders<TChannel>.Update.AddToSetEach(e => e.participants, participants);
        
        var channelCollection = service.GetCollection<TChannel>(nameof(TChannel) + "s");
        var result = await channelCollection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }

    public async Task<bool> RemoveParticipants<TChannel>(string channelId, List<string> participants) where TChannel : BaseChannel
    {
        var filter = Builders<TChannel>.Filter.Eq(e => e.id, channelId);
        var update = Builders<TChannel>.Update.PullAll(e => e.participants, participants);
        
        var channelCollection = service.GetCollection<TChannel>(nameof(TChannel) + "s");
        var result = await channelCollection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }
}