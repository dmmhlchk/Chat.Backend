using Chat.Domain.Entities;
using Chat.Persistence.Dependencies.IServices;
using MongoDB.Driver;

namespace Chat.Persistence.Dependencies.Services;

public class ServerService(MongoService service) : IServerService
{
    public async Task<Server> GetAsync(string serverId)
    {
        var filter = Builders<Server>.Filter.Eq(e => e.id, serverId);

        var serverCollection = service.GetCollection<Server>("Servers");


        return await serverCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Server>?> GetListAsync(string userId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.id, userId);
        var projection = Builders<User>.Projection.Include(u => u.servers);

        var userCollection = service.GetCollection<User>("Users");
        var user = await userCollection
            .Find(filter)
            .Project<User>(projection)
            .FirstOrDefaultAsync();

        if (user != null && user.servers != null)
        {
            var serverCollection = service.GetCollection<Server>("Servers");
            var filterServers = Builders<Server>.Filter.In(s => s.id, user.servers);
            var servers = await serverCollection
                .Find(filterServers)
                .ToListAsync();

            return servers;
        }

        return null; 
    }
    
    

    public Task<string> CreateInvite(string serverId)
    {
        throw new NotImplementedException();
    }
    
    
    
    public async Task<bool> CreateAsync(Server server)
    {
        var serverCollection = service.GetCollection<Server>("Servers");
        await serverCollection.InsertOneAsync(server);

        if (server.participants != null)
        {
            var userFilter = Builders<User>.Filter.In(e => e.id, server.participants.Select(e => e.id));
            var userUpdate = Builders<User>.Update.AddToSet(e => e.servers, server.id);

            var userCollection = service.GetCollection<User>("Users");
            await userCollection.UpdateManyAsync(userFilter, userUpdate);
        }

        
        return true;
    }

    public async Task<bool> UpdateAsync(string serverId, Server server)
    {
        var filter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var update = Builders<Server>.Update
            .Set(e => e.title, server.title)
            .Set(e => e.profileImage, server.profileImage);
            
        var serverCollection = service.GetCollection<Server>("Servers");
        var result = await serverCollection.UpdateOneAsync(filter, update);


        if (result.ModifiedCount > 0) 
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync(string serverId)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);

        var serverCollection = service.GetCollection<Server>("Servers");
        var server = await serverCollection
            .Find(serverFilter)
            .FirstOrDefaultAsync();

        if (server.participants != null)
        {
            var userFilter = Builders<User>.Filter.In(e => e.id, server.participants.Select(e => e.id));
            var userUpdate = Builders<User>.Update.Pull(e => e.servers, server.id);

            var userCollection = service.GetCollection<User>("Users");
            await userCollection.UpdateManyAsync(userFilter, userUpdate);
        }
        
        if (server.textChannels != null && server.textChannels.Any())
        {
            var textChannelFilter = Builders<TextChannel>.Filter.In(e => e.id, server.textChannels);
            var textChannelCollection = service.GetCollection<TextChannel>("TextChannels");
            await textChannelCollection.DeleteManyAsync(textChannelFilter);
        }
        
        if (server.voiceChannels != null && server.voiceChannels.Any())
        {
            var voiceChannelFilter = Builders<VoiceChannel>.Filter.In(e => e.id, server.voiceChannels);
            var voiceChannelCollection = service.GetCollection<VoiceChannel>("VoiceChannels");
            await voiceChannelCollection.DeleteManyAsync(voiceChannelFilter);
        }

        var result = await serverCollection.DeleteOneAsync(serverFilter);
        
            
        if (result.DeletedCount > 0)
            return true;

        return false;
    }

    
    
    public async Task<bool> AddUsersAsync(string serverId, List<Participant> participants)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var serverUpdate = Builders<Server>.Update.AddToSetEach(e => e.participants, participants);

        var serverCollection = service.GetCollection<Server>("Servers");
        await serverCollection.UpdateOneAsync(serverFilter, serverUpdate);
        
        
        var userFilter = Builders<User>.Filter.In(e => e.id,participants.Select(e => e.id));
        var userUpdate = Builders<User>.Update.AddToSet(e => e.servers, serverId);

        var userCollection = service.GetCollection<User>("Users");
        await userCollection.UpdateManyAsync(userFilter, userUpdate);

        return true;
    }

    public async Task<bool> RemoveUsersAsync(string serverId, List<string> participants)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var serverUpdate = Builders<Server>.Update.PullFilter(
            e => e.participants, Builders<Participant>.Filter.In(participant => participant.id, participants));
        
        var serverCollection = service.GetCollection<Server>("Servers");
        await serverCollection.UpdateOneAsync(serverFilter, serverUpdate);
        

        var userFilter = Builders<User>.Filter.In(e => e.id, participants);
        var userUpdate = Builders<User>.Update.Pull(e => e.servers, serverId);

        var userCollection = service.GetCollection<User>("Users");
        await userCollection.UpdateManyAsync(userFilter, userUpdate);
        

        return true;
    }
    
    

    public async Task<bool> LeaveAsync(string serverId, string userId)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var serverUpdate = Builders<Server>.Update.PullFilter(
            e => e.participants, Builders<Participant>.Filter.Eq(participant => participant.id, userId));
        
        var serverCollection = service.GetCollection<Server>("Servers");
        await serverCollection.UpdateOneAsync(serverFilter, serverUpdate);
        

        var userFilter = Builders<User>.Filter.Eq(e => e.id, userId);
        var userUpdate = Builders<User>.Update.Pull(e => e.servers, serverId);

        var userCollection = service.GetCollection<User>("Users");
        await userCollection.UpdateManyAsync(userFilter, userUpdate);
        

        return true;
    }
}