using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStage2.Models;

namespace MyStage2.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<MyStage2.Models.Announsment> Announsment { get; set; }
        public DbSet<MyStage2.Models.User> Users { get;  set; }
    }
}
