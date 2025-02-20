using Microsoft.EntityFrameworkCore;
using Restaurant.Entities;
using System.Data;

namespace Restaurant
{
    class RestaurantDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=1327q;Database=restaurant_db");
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
    }
}
