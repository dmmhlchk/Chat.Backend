using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Chat.Persistence.Dependencies.Services;

public class MongoService
{
    private readonly IMongoDatabase _database;

    public MongoService(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["Databases:MongoSettings:ConnectionString"]);
        _database = client.GetDatabase(configuration["Databases:MongoSettings:Database"]);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}