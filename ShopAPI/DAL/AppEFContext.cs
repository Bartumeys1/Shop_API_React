using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>,UserRoleEntity,IdentityUserLogin<int>,
        IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options) :base(options){}

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRoleEntity>(ur => {
                ur.HasKey(u => new { u.UserId, u.RoleId });

                ur.HasOne(u=> u.Role)
                    .WithMany(r=>r.UserRoles)
                    .HasForeignKey(r=>r.RoleId)
                    .IsRequired();

                ur.HasOne(u=> u.User)
                    .WithMany(u=>u.UserRoles)
                    .HasForeignKey(r=>r.UserId)
                    .IsRequired();
            });

            builder.Entity<ProductEntity>(p =>{
                p.HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();
            });

        }
    }
}
