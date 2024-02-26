using Chat.Domain.Entities;
using Chat.Persistence.Dependencies.IServices;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Chat.Persistence.Dependencies.Services;

public class UserService(MongoService service) : IUserService
{
    public async Task<User> GetAsync(string userId)
    {
        var filter = Builders<User>.Filter.Eq(e => e.id, userId);

        var userCollection = service.GetCollection<User>("Users");


        return await userCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetListAsync(List<string> users)
    {
        var filter = Builders<User>.Filter.In(e => e.id, users);

        var userCollection = service.GetCollection<User>("Users");


        return await userCollection
            .Find(filter)
            .ToListAsync();
    }
    
    public async Task<List<User>> GetListAsync(string username, string userId)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Regex(u => u.username, new BsonRegularExpression(username, "i")),
            Builders<User>.Filter.Not(Builders<User>.Filter.Eq(u => u.id, userId)));

        var userCollection = service.GetCollection<User>("Users");


        return await userCollection
            .Find(filter)
            .ToListAsync();
    }
}