using Chat.Domain.Entities;

namespace Chat.Application.Common.Interfaces;

public interface IUser
{
    Task<User> GetAsync(string userId);
    Task<List<User>> GetListAsync(List<string> userIds);
    Task<List<User>> GetListAsync(string username, string userId);
}