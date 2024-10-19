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
        //public AllBirdsContext(DbContextOptions options) : base(options)
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-52U8U14;Initial Catalog=FinalProject1;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");//.UseLazyLoadingProxies(true); ;
        //    base.OnConfiguring(optionsBuilder);
        //}

        //public DbSet<CategorySize> CategorySizes { get; set; }///Hossam add this

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<ClientFavorite> ClientFavorites { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderDetail> OrdersDetail { get; set; }
        public DbSet<OrderMaster> OrdersMaster { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductColorImage> ProductColorImages { get; set; }
        public DbSet<ProductColorSize> ProductColorSizes { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductSpecification> ProductSpecification { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed Roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "SuperUser", NormalizedName = "SUPERUSER"},
                new IdentityRole<int> { Id = 2, Name = "Manager", NormalizedName = "MANAGER"},
                new IdentityRole<int> { Id = 3, Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole<int> { Id = 4, Name = "Client", NormalizedName = "CLIENT"}
            );
            // Seed OrderStates
            modelBuilder.Entity<OrderState>().HasData(
                new OrderState { Id = 1, StateAr = "في العربة", StateEn = "In Cart", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 2, StateAr = "قيد الإنتظار", StateEn = "Pending", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 3, StateAr = "تمت الموافقة", StateEn = "Approved", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 4, StateAr = "تم الإلغاء", StateEn = "Cancelled", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
                //new OrderState { Id = 4, StateAr = "خرج للتوصيل", StateEn = "Out For Delivery", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                //new OrderState { Id = 5, StateAr = "تم التوصيل", StateEn = "Deliverd", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Sizes
            modelBuilder.Entity<Size>().HasData(                
                new Size { Id = 1, SizeNumber = "1", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 2, SizeNumber = "2", CreatedBy = 1, Created = new DateTime(2024, 10, 19)},
                new Size { Id = 3, SizeNumber = "3", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 4, SizeNumber = "4", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 5, SizeNumber = "5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 6, SizeNumber = "5.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 7, SizeNumber = "6", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 8, SizeNumber = "6.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 9, SizeNumber = "7", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 10, SizeNumber = "7.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 11, SizeNumber = "8", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 12, SizeNumber = "8.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 13, SizeNumber = "9", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 14, SizeNumber = "9.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 15, SizeNumber = "10", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 16, SizeNumber = "10.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 17, SizeNumber = "11", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 18, SizeNumber = "12", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 19, SizeNumber = "13", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 20, SizeNumber = "14", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Colors
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, NameEn = "color1", NameAr = "اللون الاول", Code = "31243", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 2, NameEn = "color2",NameAr="اللون الثاني", Code = "31243", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, NameAr = "رجالي", NameEn = "Men", ParentCategoryId = 0, Level = 0, IsParentCategory = true, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 2, NameAr = "أحذية", NameEn = "Shoes", ParentCategoryId = 1, Level = 1, IsParentCategory = true, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 3, NameAr = "أحذية رياضية يومية", NameEn = "Everyday Sneakers", ParentCategoryId = 2, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 4, NameAr = "أحذية نشطة", NameEn = "Active Shoes", ParentCategoryId = 2, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 5, NameAr = "أحذية مقاومة للماء", NameEn = "Water-Repellent Shoes", ParentCategoryId = 2, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 6, NameAr = "أحذية بدون أربطة", NameEn = "Slip-Ons", ParentCategoryId = 2, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 7, NameAr = "عروض الأحذية", NameEn = "Sale Shoes", ParentCategoryId = 2, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 8, NameAr = "الأكثر مبيعًا", NameEn = "Bestsellers", ParentCategoryId = 1, Level = 1, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 9, NameAr = "ملابس والمزيد", NameEn = "Apparel & More", ParentCategoryId = 1, Level = 1, IsParentCategory = true, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 10, NameAr = "ملابس داخلية", NameEn = "Underwear", ParentCategoryId = 9, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 11, NameAr = "تي شيرتات", NameEn = "Tees", ParentCategoryId = 9, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 12, NameAr = "نعال", NameEn = "Insoles", ParentCategoryId = 9, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 13, NameAr = "بطاقات هدايا", NameEn = "Gift Cards", ParentCategoryId = 9, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 14, NameAr = "عروض الملابس والجوارب", NameEn = "Sale Apparel & Socks", ParentCategoryId = 9, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 15, NameAr = "وصل حديثًا", NameEn = "New Arrivals", ParentCategoryId = 1, Level = 1, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 16, NameAr = "نساء", NameEn = "Women", ParentCategoryId = 0, Level = 0, IsParentCategory = true, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 17, NameAr = "أحذية", NameEn = "Shoes", ParentCategoryId = 16, Level = 1, IsParentCategory = true, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 18, NameAr = "أحذية رياضية يومية", NameEn = "Everyday Sneakers", ParentCategoryId = 17, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 19, NameAr = "أحذية نشطة", NameEn = "Active Shoes", ParentCategoryId = 17, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 20, NameAr = "أحذية مقاومة للماء", NameEn = "Water-Repellent Sneakers", ParentCategoryId = 17, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 21, NameAr = "أحذية مسطحة", NameEn = "Flats", ParentCategoryId = 17, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 22, NameAr = "أحذية بدون أربطة", NameEn = "Slip-Ons", ParentCategoryId = 17, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 23, NameAr = "عروض الأحذية", NameEn = "Sale Shoes", ParentCategoryId = 17, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 24, NameAr = "الأكثر مبيعًا", NameEn = "Bestsellers", ParentCategoryId = 16, Level = 1, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 25, NameAr = "ملابس والمزيد", NameEn = "Apparel & More", ParentCategoryId = 16, Level = 1, IsParentCategory = true, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 26, NameAr = "ملابس داخلية", NameEn = "Underwear", ParentCategoryId = 25, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 27, NameAr = "تي شيرتات", NameEn = "Tees", ParentCategoryId = 25, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 28, NameAr = "نعال", NameEn = "Insoles", ParentCategoryId = 25, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 29, NameAr = "بطاقات هدايا", NameEn = "Gift Cards", ParentCategoryId = 25, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 30, NameAr = "عروض الملابس والجوارب", NameEn = "Sale Apparel & Socks", ParentCategoryId = 25, Level = 2, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 31, NameAr = "وصل حديثًا", NameEn = "New Arrivals", ParentCategoryId = 16, Level = 1, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 32, NameAr = "جوارب", NameEn = "Socks", ParentCategoryId = 0, Level = 0, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Category { Id = 33, NameAr = "عروض", NameEn = "Sale", ParentCategoryId = 0, Level = 0, IsParentCategory = false, CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Specifications
            modelBuilder.Entity<Specification>().HasData(
                new Specification { Id = 1, NameAr = "التفاصيل", NameEn = "Details", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 2, NameAr = "الأفضل من أجل", NameEn = "Best For", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 3, NameAr = "المواد", NameEn = "Material", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 4, NameAr = "المقاس", NameEn = "Fit", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 5, NameAr = "مكان الصنع", NameEn = "Where It’s Made", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 6, NameAr = "المواد فائقة النعومة", NameEn = "Super Soft Material", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 7, NameAr = "الابتكار الطبيعي", NameEn = "Natural Innovation", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 8, NameAr = "المواد المنعشة", NameEn = "Breezy Material", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 9, NameAr = "التصميم متعدد الاستخدامات", NameEn = "Versatile Design", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 10, NameAr = "الاستخدام", NameEn = "Use", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 11, NameAr = "المواد المنظمة للحرارة", NameEn = "Thermoregulating Material", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 12, NameAr = "الارتداد طوال اليوم", NameEn = "All-Day Bounce", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 13, NameAr = "الراحة أثناء التنقل", NameEn = "On-the-Go Convenience", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 14, NameAr = "ملاءمة كالجوارب", NameEn = "Sock-Like Fit", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 15, NameAr = "الراحة المبطنة", NameEn = "Cushioned Comfort", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 16, NameAr = "الجزء العلوي مصنوع من صوف ZQ", NameEn = "Upper made with ZQ Merino Wool", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 17, NameAr = "زيادة المتانة", NameEn = "Increased Durability", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 18, NameAr = "تحسين الشكل", NameEn = "Improved Shape", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 19, NameAr = "ملاءمة مطورة", NameEn = "Upgraded Fit", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 20, NameAr = "تعزيز داخلي", NameEn = "Internal Reinforcement", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 21, NameAr = "ثبات متعدد الاتجاهات", NameEn = "Multi-Directional Traction", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 22, NameAr = "راحة أثناء اللعب", NameEn = "On-Course Comfort", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 23, NameAr = "ثبات منتصف القدم", NameEn = "Midfoot Stability", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 24, NameAr = "ثبات في جميع الظروف", NameEn = "All-Condition Traction", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 25, NameAr = "تصميم يسهل الانزلاق", NameEn = "Easy Slip-On Design", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 26, NameAr = "تصميم مستقر ومستدام", NameEn = "Stabilizing, Sustainable Design", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 27, NameAr = "دعم إضافي", NameEn = "Extra Support", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 28, NameAr = "تصميم حديث", NameEn = "Modern Design", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 29, NameAr = "المواد الطبيعية", NameEn = "Natural Material", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 30, NameAr = "النعل الداخلي المدمج", NameEn = "Integrated Insole", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 31, NameAr = "صوف ZQRX المتجدد", NameEn = "ZQRX Regenerative Wool", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 32, NameAr = "تصميم خالد", NameEn = "Timeless Design", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 33, NameAr = "المواد", NameEn = "Materials", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 34, NameAr = "خليط المواد", NameEn = "Material Blend", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Specification { Id = 35, NameAr = "الوصف", NameEn = "Description", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
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
