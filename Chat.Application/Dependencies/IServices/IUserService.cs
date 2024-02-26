using Chat.Domain.Entities;

namespace Chat.Application.Dependencies.IServices;

public interface IUserService
{
    Task<User> GetAsync(string userId);
    Task<List<User>> GetListAsync(List<string> userIds);
    Task<List<User>> GetListAsync(string username, string userId);
}