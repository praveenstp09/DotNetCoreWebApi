using Microsoft.EntityFrameworkCore;
using FirstWebAPI.Models;
namespace FirstWebAPI.Config
{
    public class FirstWebAPIDbContext:DbContext
    {
        public FirstWebAPIDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleModel>()
                .HasKey(ur=>new {ur.RoleId,ur.UserId});

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur=>ur.User)
                .WithMany(u=>u.UserRoles)
                .HasForeignKey(ur=>ur.UserId);

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

        }
    }
}
