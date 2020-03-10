using Microsoft.EntityFrameworkCore;
using MyStage2.Models;

namespace MyStage2.Data
{
    public class Context : DbContext 
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Announsment> Announsment { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
