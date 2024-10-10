using AllBirds.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllBirds.Context
{
    //public class AllBirdContext : IdentityDbContext<CustomUser, IdentityRole<int>, int>
    //{
    //    public AllBirdContext(DbContextOptions<AllBirdContext> dbContextOptions) : base(dbContextOptions) { }
    //}
    public class AllBirdsContext(DbContextOptions<AllBirdsContext> dbContextOptions) : IdentityDbContext<CustomUser, IdentityRole<int>, int>(dbContextOptions)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        //public DbSet<Client> Clients { get; set; }
        public DbSet<ClientFavorite> ClientFavorites { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderDetail> OrdersDetail { get; set; }
        public DbSet<OrderMaster> OrdersMaster { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductColorImage> ProductColorImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed OrderStates
            modelBuilder.Entity<OrderState>().HasData(
                new OrderState
                {
                    Id = 1,
                    StateAr = "في العربة",
                    StateEn = "In Cart",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new OrderState
                {
                    Id = 2,
                    StateAr = "جاري التجهيز",
                    StateEn = "Processing",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new OrderState
                {
                    Id = 3,
                    StateAr = "خرج للتوصيل",
                    StateEn = "Out For Delivery",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new OrderState
                {
                    Id = 4,
                    StateAr = "تم التوصيل",
                    StateEn = "Deliverd",
                    CreatedBy = 1,
                    Created = DateTime.Now
                }
            );
            // Seed Sizes
            modelBuilder.Entity<Size>().HasData(
                new Size
                {
                    Id = 1,
                    SizeNumber = "1",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 2,
                    SizeNumber = "2",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 3,
                    SizeNumber = "3",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 4,
                    SizeNumber = "4",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 5,
                    SizeNumber = "5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 6,
                    SizeNumber = "5.5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 7,
                    SizeNumber = "6",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 8,
                    SizeNumber = "6.5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 9,
                    SizeNumber = "7",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 10,
                    SizeNumber = "7.5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 11,
                    SizeNumber = "8",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 12,
                    SizeNumber = "8.5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 13,
                    SizeNumber = "9",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 14,
                    SizeNumber = "9.5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 15,
                    SizeNumber = "10",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 16,
                    SizeNumber = "10.5",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 17,
                    SizeNumber = "11",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 18,
                    SizeNumber = "12",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 19,
                    SizeNumber = "13",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Size
                {
                    Id = 20,
                    SizeNumber = "14",
                    CreatedBy = 1,
                    Created = DateTime.Now
                }
                );
            // Seed Colors
            modelBuilder.Entity<Color>().HasData(
                new Color
                {
                    Id = 1,
                    ColorCode = "color1",
                    CreatedBy = 1,
                    Created = DateTime.Now
                },
                new Color
                {
                    Id = 2,
                    ColorCode = "color2",
                    CreatedBy = 1,
                    Created = DateTime.Now
                }
            );
        }
        //public int SaveChanges(bool acceptAllChangesOnSuccess, int userId)
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = DateTime.Now;
                    entity.Entity.CreatedBy = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity.IsDeleted == true)
                    {
                        entity.Entity.Deleted = DateTime.Now;
                        entity.Entity.DeletedBy = 1;
                    }
                    else
                    {
                        entity.Entity.Updated = DateTime.Now;
                        entity.Entity.UpdatedBy = 1;
                    }
                }
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = DateTime.Now;
                    entity.Entity.CreatedBy = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity.IsDeleted == true)
                    {
                        entity.Entity.Deleted = DateTime.Now;
                        entity.Entity.DeletedBy = 1;
                    }
                    else
                    {
                        entity.Entity.Updated = DateTime.Now;
                        entity.Entity.UpdatedBy = 1;
                    }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
