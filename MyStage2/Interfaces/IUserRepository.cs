using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStage2.Models;

namespace MyStage2.Interfaces
{
    public interface IUserRepository
    {
        DbSet<User> Users { get; }
        User GetUser(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        void SaveChanges();
    }
}
