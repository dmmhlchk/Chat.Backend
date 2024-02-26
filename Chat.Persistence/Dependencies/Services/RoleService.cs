using Chat.Domain.Entities;
using Chat.Persistence.Dependencies.IServices;
using MongoDB.Driver;

namespace Chat.Persistence.Dependencies.Services;

public class RoleService(MongoService service) : IRoleService
{
    public async Task<Role?> GetAsync(string serverId, string roleId)
    {
        // TODO: i have to test this one
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        
        var serverCollection = service.GetCollection<Server>("Servers");
        var server = await serverCollection
            .Find(serverFilter)
            .FirstOrDefaultAsync();

        
        if (server != null)
            return server.roles.Find(e => e.id == roleId);

        return null;
    }

    public async Task<List<Role>?> GetListAsync(string serverId)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        
        var serverCollection = service.GetCollection<Server>("Servers");
        var server = await serverCollection
            .Find(serverFilter)
            .FirstOrDefaultAsync();


        if (server != null)
            return server.roles;

        return null;
    }

    public async Task<bool> CreateAsync(string serverId, Role role)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var updateServer = Builders<Server>.Update.AddToSet(e => e.roles, role);

        var serverCollection = service.GetCollection<Server>("Servers");
        var result = await serverCollection.UpdateOneAsync(serverFilter, updateServer);


        if (result.ModifiedCount > 0)
            return true;

        return false;
    }

    public async Task<bool> UpdateAsync(string serverId, string roleId, Role role)
    {
        // // TODO: i have to test this one
        var serverFilter = Builders<Server>.Filter.And(
            Builders<Server>.Filter.Eq(e => e.id, serverId),
            Builders<Server>.Filter.ElemMatch(e => e.roles, r => r.id == roleId)
        );

        var updateServer = Builders<Server>.Update.Set(e => e.roles[-1], role);

        var serverCollection = service.GetCollection<Server>("Servers");
        var result = await serverCollection.UpdateOneAsync(serverFilter, updateServer);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string serverId, string roleId)
    {
        var serverFilter = Builders<Server>.Filter.Eq(e => e.id, serverId);
        var update = Builders<Server>.Update.PullFilter(e => e.roles, r => r.id == roleId);

        var serverCollection = service.GetCollection<Server>("Servers");
        var result = await serverCollection.UpdateOneAsync(serverFilter, update);

        return result.ModifiedCount > 0;
    }
}