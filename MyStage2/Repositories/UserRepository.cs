using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStage2.Interfaces;
using MyStage2.Models;
using MyStage2.Data;

namespace MyStage2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public DbSet<User> Users => _context.Users;

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }


        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
