using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);

        Task<int> AddUserAsync(User user);

        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
    }
}
