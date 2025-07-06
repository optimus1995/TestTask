
using UserDomain.Entities;
namespace UserDomain.Interface;
public interface IUserRepository
{
    Task<int> GetByEmailAsync(string email);
    Task<User> GetLoginAsync(string email);
    Task AddAsync(User user);
    Task<int> UpdateDetailsAsync(User user);
}
