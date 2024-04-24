using DotNetCoreApp1.Models.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetCoreApp1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }

        public DbSet<DataDto> Data { get; set; }
        public DbSet<DocumentDto> Documents { get; set; }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<PasswordDto> Passwords { get; set; }
    }
}
