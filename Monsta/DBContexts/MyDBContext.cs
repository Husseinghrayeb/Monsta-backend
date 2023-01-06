using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monsta_backend.Entities;

namespace Monsta_backend.DBContexts
{
    public class MyDBContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
    }
}
