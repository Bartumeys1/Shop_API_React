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
        public DbSet<ProductImagesEntity> ProductImages { get; set; }
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


            builder.Entity<CategoryEntity>(u => { 
                //properties
                u.Property<string>("Name").HasMaxLength(50).IsRequired(); 
                u.Property<string>("Image").HasMaxLength(255); 
                u.Property<string>("Slug").HasMaxLength(255); 
            });

            builder.Entity<ProductEntity>(p =>{
                //properties
                p.Property<string>("Name").HasMaxLength(100).IsRequired();
                p.Property<float>("Price").IsRequired();
                p.Property<string>("Description").IsRequired();
                p.Property<string>("Slug").HasMaxLength(255);


                //relationship
                p.HasOne<CategoryEntity>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();
            });

            builder.Entity<ProductImagesEntity>(p =>
            {
                p.Property<string>("Name").HasMaxLength(50).IsRequired();
                p.Property<int>("Priority").IsRequired();

                p.HasOne(img => img.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(img => img.ProductId)
                .IsRequired();
            });

        }
    }
}
