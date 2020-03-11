using System.Collections.Generic;
using System.Threading.Tasks;
using MyStage2.Models;

namespace MyStage2.Interfaces
{
    public interface IUserRepository
    {
        IAsyncEnumerable<User> Users { get; }

        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersAsync();

        User GetUser(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        void SaveChanges();
    }
}
