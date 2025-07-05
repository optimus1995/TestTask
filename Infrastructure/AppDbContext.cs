
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserDomain.Entities;

namespace Infrastructure;
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
    }


