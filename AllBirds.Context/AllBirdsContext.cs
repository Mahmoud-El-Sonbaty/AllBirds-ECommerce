using AllBirds.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Buffers.Text;
using static Azure.Core.HttpHeader;
using System.Text.RegularExpressions;
using Azure;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Sockets;

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

        public DbSet<IdentityUserRole<int>> AccountRoles { get; set; }
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
            // Seed Order States
            modelBuilder.Entity<OrderState>().HasData(
                new OrderState { Id = 1, StateAr = "في العربة", StateEn = "In Cart", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 2, StateAr = "قيد الإنتظار", StateEn = "Pending", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 3, StateAr = "تمت الموافقة", StateEn = "Approved", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 4, StateAr = "جاري التجهيز", StateEn = "Processing", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 5, StateAr = "خرج للتوصيل", StateEn = "Out For Delivery", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 6, StateAr = "تم التوصيل", StateEn = "Deliverd", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new OrderState { Id = 7, StateAr = "تم الإلغاء", StateEn = "Cancelled", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
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
                new Size { Id = 18, SizeNumber = "11.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 19, SizeNumber = "12", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 20, SizeNumber = "12.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 21, SizeNumber = "13", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 22, SizeNumber = "13.5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Size { Id = 23, SizeNumber = "14", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Colors
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, NameEn = "Black", NameAr = "أسود", Code = "#3b3b3b", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 2, NameEn = "Light Grey", NameAr = "رمادي فاتح", Code = "#D3D3D3", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 3, NameEn = "Grey", NameAr = "رمادي", Code = "#8c8c8c", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 4, NameEn = "Dark Grey", NameAr = "رمادي داكن", Code = "#A9A9A9", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 5, NameEn = "Biege", NameAr = "بيج", Code = "#b9afa1", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 6, NameEn = "Blue", NameAr = "أزرق", Code = "#49607c", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 7, NameEn = "Red", NameAr = "أحمر", Code = "#b14754", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 8, NameEn = "Green", NameAr = "أخضر", Code = "#69715e", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 9, NameEn = "White", NameAr = "أبيض", Code = "#f5f4f0", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 10, NameEn = "Purple", NameAr = "بنفسجي", Code = "#bbb9d5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 11, NameEn = "Yellow", NameAr = "أصفر", Code = "#ead99a", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 12, NameEn = "Brown", NameAr = "بني", Code = "#bd9474", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new Color { Id = 13, NameEn = "Pink", NameAr = "وردي", Code = "#dfabb5", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
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
            // Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductNo = "P001",
                    NameAr = "منتج 1",
                    NameEn = "Men's Wool Runner Go",
                    Price = 110.00m,
                    HighlightsAr = "راحة معززة بفضل 14٪ من الرغوة الإضافية تحت القدم وصندوق أصابع أوسع~@#$%&متانة محسنة بفضل الجزء العلوي الذي يوفر تعزيزات لمنع اختراق الأصابع~@#$%&مظهر عصري بفضل الجزء العلوي الأنيق وتصميم جديد بالكامل",
                    HighlightsEn = @"Enhanced comfort thanks to 14% more foam under your foot and a roomier toe-box~@#$%&Improved durability with an upper that provides reinforcements to help prevent toe poke-through~@#$%&Modern aesthetic delivered by a sleek upper and completely reimagined fit",
                    SustainabilityAr = "البصمة الكربونية لحذاء Wool Runner Go هي 6.85 كجم CO2e. تعرف على المزيد حول ملصقات البصمة الكربونية والتزامنا بتقليل تأثيرنا.",
                    SustainabilityEn = "Our Wool Runner Go has a carbon footprint of 6.85 kg CO2e. Learn more about carbon footprint labeling and our commitments to reduce our impact.",
                    SustainableMaterialsAr = "الجزء العلوي مصنوع من صوف ميرينو معتمد يوفر الراحة ويحافظ على دفء القدمين~@#$%&نعل SweetFoam® مصنوع من EVA الأخضر المستمد من قصب السكر~@#$%&أربطة الحذاء مصنوعة من زجاجات بلاستيكية معاد تدويرها~@#$%&النعل الداخلي مصنوع من قماش مزيج الصوف ورغوة مشتقة من الذرة لتوسيد أكثر ملاءمة للبيئة~@#$%&بطانة الكعب مصنوعة من صوف ميرينو المعتمد لتوفير النعومة حيث يكون ذلك أكثر أهمية",
                    SustainableMaterialsEn = "Upper made with ZQ Certified Merino wool that provides supreme comfort and keeps feet warm whether you wear them with or without socks~@#$%&SweetFoam® midsole made with sugarcane-based green EVA provides enhanced underfoot comfort~@#$%&Shoe laces made with recycled plastic bottles~@#$%&Insole made with a wool blend fabric and a corn-derived foam for more planet-friendly cushioning~@#$%&Heel lining made with ZQ Certified Merino wool offers softness where it matters most",
                    ShippingAndReturnsAr = "الشحن مجاني للطلبات التي تزيد عن 75 دولارًا، والمرتجعات مجانية في غضون 30 يومًا. يتم التبرع بالأحذية الملبوسة قليلاً إلى Soles4Souls. تفضل بزيارة بوابة المرتجعات الخاصة بنا.* لا يمكن إرجاع أو استبدال العناصر التي تباع نهائيًا، بما في ذلك: بطاقات الهدايا والنعل والعناصر المحددة على أنها نهائية في موقعنا الإلكتروني ومتاجرنا.",
                    ShippingAndReturnsEn = @"Free shipping on orders over $75, and free returns accepted within 30 days. Lightly worn shoes get donated to Soles4Souls. Visit our Returns Portal.* Final Sale items cannot be returned or exchanged. Final Sale items include: gift cards, insoles, and items tagged final sale on our website and in Allbirds retail stores. Need it sooner? See if the style you want is available at a store near you.",
                    CareGuideAr = "1. إزالة الأربطة والنعل.~@#$%&2. ضع الحذاء في حقيبة مخصصة (يمكن استخدام وسادة).~@#$%&3. اختر دورة لطيفة بالماء البارد ومنظف خفيف.~@#$%&4. رج الماء الزائد واتركه ليجف.~@#$%&5. سيستعيد الحذاء شكله الأصلي بعد ارتدائه مرة أو مرتين.",
                    CareGuideEn = @"1. Remove the laces and insoles.~@#$%&2. Place shoes in a delicates bag (pro tip: a pillowcase works too).~@#$%&3. Choose a gentle cycle with cold water & mild detergent.~@#$%&4. Shake out any excess water & set aside to air dry.~@#$%&5. Shoes will regain their original shape with one or two wears.",
                    Discount = 30,
                    FreeShipping = true,
                    MainColorId = 1 // Assuming MainColorId references an existing color
                },
                new Product
                {
                    Id = 2,
                    ProductNo = "P002",
                    NameAr = "منتج 2",
                    NameEn = "Men's Wool Piper Go",
                    Price = 120.00m,
                    HighlightsAr = "خليط من الصوف الفاخر يحافظ على الدفء والراحة~@#$%&تحسين بناء المواد يعزز الراحة والمتانة~@#$%&تصميم علوي مصقول لأناقة نهائية",
                    HighlightsEn = @"Luxe wool blend keeps things warm and cozy~@#$%&Improved material construction levels up fit and durability~@#$%&Refined wool blend upper for a polished finish",
                    SustainabilityAr = "البصمة الكربونية لحذاء Wool Piper Go هي 6.98 كجم CO2e. تعرف على المزيد حول ملصقات البصمة الكربونية والتزامنا بتقليل تأثيرنا.",
                    SustainabilityEn = "Our Wool Piper Go has a carbon footprint of 6.98 kg CO2e. Learn more about carbon footprint labeling and our commitments to reduce our impact.",
                    SustainableMaterialsAr = "الجزء العلوي مصنوع من مزيج صوف معتمد عالي المعايير~@#$%&رباط صناعي من ألياف دقيقة معاد تدويرها~@#$%&نعل SweetFoam® مصنوع من EVA الأخضر المستمد من قصب السكر~@#$%&أربطة الحذاء مصنوعة من بوليستر معاد تدويره",
                    SustainableMaterialsEn = "Upper blend made with RWS-certified wool, reflecting the highest ethical and environmental standards~@#$%&Recycled polyester microfiber synthetic suede rand~@#$%&SweetFoam® midsole made with sugarcane-based green EVA~@#$%&Shoelaces made with recycled polyester",
                    ShippingAndReturnsAr = "الشحن مجاني للطلبات التي تزيد عن 75 دولارًا، والمرتجعات مجانية في غضون 30 يومًا.",
                    ShippingAndReturnsEn = @"Free shipping on orders over $75, and free returns accepted within 30 days. Lightly worn shoes get donated to Soles4Souls. Visit our Returns Portal.",
                    CareGuideAr = "دليل العناية 1",
                    CareGuideEn = @"1. Remove the laces and insoles.~@#$%&2. Place shoes in a delicates bag (pro tip: a pillowcase works too).~@#$%&3. Choose a gentle cycle with cold water & mild detergent.~@#$%&4. Shake out any excess water & set aside to air dry.~@#$%&5. Shoes will regain their original shape with one or two wears.",
                    Discount = 0,
                    FreeShipping = true,
                    MainColorId = 12
                },
                new Product
                {
                    Id = 3,
                    ProductNo = "P003",
                    NameAr = "منتج 3",
                    NameEn = "Men's SuperLight Tree Runners",
                    Price = 110.00m,
                    HighlightsAr = "واحدة من أخف البصمات الكربونية وأقلها حتى الآن~@#$%&الجزء العلوي المصنوع من ألياف شجرة الكينا يساعد في توفير التهوية الفائقة~@#$%&وسادة فائقة الخفة مصنوعة من SuperLight Foam تدعم كل خطوة",
                    HighlightsEn = @"One of our lightest and lowest carbon footprints to date~@#$%&Eucalyptus tree fiber upper helps add next-level breathability~@#$%&Ultralight bio-based SuperLight Foam cushions every step",
                    SustainabilityAr = "بصمة الكربون لحذاء SuperLight Tree Runner الخاص بنا هي 2.9 كجم من ثاني أكسيد الكربون (للمقارنة، الحذاء العادي حوالي 14 كجم من ثاني أكسيد الكربون، بناءً على حساباتنا). تعرف على المزيد حول ملصقات بصمة الكربون والتزاماتنا لتقليل تأثيرنا.",
                    SustainabilityEn = "Our SuperLight Tree Runner has a carbon footprint of 2.9 kg CO2e (for comparison, a standard sneaker is about 14 kg CO2e, based on our calculations.) Learn more about carbon footprint labeling and our commitments to reduce our impact.",
                    SustainableMaterialsAr = "الجزء العلوي مصنوع من ألياف شجرة الكينا TENCEL™ Lyocell المعتمدة من FSC~@#$%&وسادة SuperLight Foam مصنوعة من EVA الأخضر المستخلص من قصب السكر~@#$%&عروات الأحذية مصنوعة من TPU القائم على المصادر الحيوية~@#$%&أربطة الأحذية مصنوعة من زجاجات بلاستيكية معاد تدويرها~@#$%&وسادة القدم SweetFoam®",
                    SustainableMaterialsEn = "FSC-certified TENCEL™ Lyocell (eucalyptus tree fiber) upper~@#$%&SuperLight Foam made with sugarcane-based green EVA~@#$%&Bio-based TPU eyelets~@#$%&Shoe laces made from recycled plastic bottles~@#$%&SweetFoam® insole foam",
                    ShippingAndReturnsAr = @"شحن مجاني للطلبات التي تزيد عن 75 دولارًا، ومرتجعات مجانية مقبولة في غضون 30 يومًا. الأحذية المستعملة قليلًا يتم التبرع بها لـ Soles4Souls. قم بزيارة بوابة الإرجاع الخاصة بنا.
                    المنتجات التي يتم بيعها على أنها نهائية لا يمكن إرجاعها أو استبدالها. تشمل العناصر النهائية: بطاقات الهدايا، والنعل، والعناصر الموسومة على أنها بيع نهائي في موقعنا الإلكتروني وفي متاجر Allbirds.
                    هل تحتاج إليه في وقت أقرب؟ تحقق مما إذا كان النمط الذي تريده متاحًا في متجر قريب منك.",
                    ShippingAndReturnsEn = @"Free shipping on orders over $75, and free returns accepted within 30 days. Lightly worn shoes get donated to Soles4Souls. Visit our Returns Portal.*
                    Final Sale items cannot be returned or exchanged. Final Sale items include: gift cards, insoles, and items tagged final sale on our website and in Allbirds retail stores.
                    Need it sooner? See if the style you want is available at a store near you.",
                    CareGuideAr = @"1. إزالة الأربطة.
                    2. ضع الحذاء في حقيبة للعناية (نصيحة: يمكن استخدام غطاء وسادة أيضًا).
                    3. اختر دورة غسيل لطيفة بالماء البارد ومنظف خفيف.
                    4. رج الماء الزائد واتركه يجف.
                    5. سيستعيد الحذاء شكله الأصلي بعد ارتدائه مرة أو مرتين.",
                    CareGuideEn = @"1. Remove the laces.
                    2. Place shoes in a delicates bag (pro tip: a pillowcase works too).
                    3. Choose a gentle cycle with cold water & mild detergent.
                    4. Shake out any excess water & set aside to air dry.
                    5. Shoes will regain their original shape with one or two wears.",
                    Discount = 40,
                    FreeShipping = false,
                    MainColorId = 16 // Assuming MainColorId references an existing color
                },
                new Product
                {
                    Id = 4,
                    ProductNo = "P004",
                    NameAr = "منتج 4",
                    NameEn = "Men's Couriers",
                    Price = 100.00m,
                    HighlightsAr = "جزء علوي متين مصنوع من القطن العضوي وألياف شجرة الكينا~@#$%&بطانة ناعمة ومبطنة لراحة فائقة (سواء مع أو بدون جوارب)~@#$%&وسادة منتصف القدم من SweetFoam® تساعد في دعم القدم طوال اليوم",
                    HighlightsEn = @"Durable upper made with organic cotton and eucalyptus fiber~@#$%&Soft, padded lining for ultimate comfort (with or without socks)~@#$%&Cushioned SweetFoam® midsole helps give wear-all-day support",
                    SustainabilityAr = "بصمة الكربون لحذاء Courier الخاص بنا هي 5.01 كجم من ثاني أكسيد الكربون (للمقارنة، الحذاء العادي حوالي 14 كجم من ثاني أكسيد الكربون، بناءً على حساباتنا). تعرف على المزيد حول ملصقات بصمة الكربون والتزاماتنا لتقليل تأثيرنا.",
                    SustainabilityEn = "Our Courier has a carbon footprint of 5.01kg CO2e (for comparison, a standard sneaker is about 14 kg CO2e, based on our calculations.) Learn more about carbon footprint labeling and our commitments to reduce our impact.",
                    SustainableMaterialsAr = "الجزء العلوي: مصنوع من 100% قطن عضوي، TENCEL™ Lyocell Ripstop، والنايلون البيولوجي~@#$%&منتصف القدم: SweetFoam® المصنوع من قصب السكر~@#$%&الجزء الخارجي: مصنوع من المطاط الطبيعي",
                    SustainableMaterialsEn = "Upper: Made with 100% Organic Cotton, TENCEL™ Lyocell Ripstop, and bio-based nylon~@#$%&Midsole: Sugarcane-based SweetFoam®~@#$%&Outsole: Made with Natural Rubber",
                    ShippingAndReturnsAr = @"شحن مجاني للطلبات التي تزيد عن 75 دولارًا، ومرتجعات مجانية مقبولة في غضون 30 يومًا. الأحذية المستعملة قليلًا يتم التبرع بها لـ Soles4Souls. قم بزيارة بوابة الإرجاع الخاصة بنا.
                    المنتجات التي يتم بيعها على أنها نهائية لا يمكن إرجاعها أو استبدالها. تشمل العناصر النهائية: بطاقات الهدايا، والنعل، والعناصر الموسومة على أنها بيع نهائي في موقعنا الإلكتروني وفي متاجر Allbirds.
                    هل تحتاج إليه في وقت أقرب؟ تحقق مما إذا كان النمط الذي تريده متاحًا في متجر قريب منك.",
                    ShippingAndReturnsEn = @"Free shipping on orders over $75, and free returns accepted within 30 days. Lightly worn shoes get donated to Soles4Souls. Visit our Returns Portal.*
                    Final Sale items cannot be returned or exchanged. Final Sale items include: gift cards, insoles, and items tagged final sale on our website and in Allbirds retail stores.
                    Need it sooner? See if the style you want is available at a store near you.",
                    CareGuideAr = @"1. إزالة الأربطة والنعل الداخلي.
                    2. ضع الحذاء في حقيبة للعناية (نصيحة: يمكن استخدام غطاء وسادة أيضًا).
                    3. اختر دورة غسيل لطيفة بالماء البارد ومنظف خفيف.
                    4. رج الماء الزائد واتركه يجف.
                    5. سيستعيد الحذاء شكله الأصلي بعد ارتدائه مرة أو مرتين.",
                    CareGuideEn = @"1. Remove the laces and insoles.
                    2. Place shoes in a delicates bag (pro tip: a pillowcase works too).
                    3. Choose a gentle cycle with cold water & mild detergent.
                    4. Shake out any excess water & set aside to air dry.
                    5. Shoes will regain their original shape with one or two wears.",
                    Discount = 30,
                    FreeShipping = false,
                    MainColorId = 23 // Assuming MainColorId references an existing color
                }
            );
            // Seed Categories Product
            modelBuilder.Entity<CategoryProduct>().HasData(
                new CategoryProduct { Id = 1, ProductId = 1, CategoryId = 2, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 2, ProductId = 1, CategoryId = 3, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 3, ProductId = 2, CategoryId = 2, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 4, ProductId = 2, CategoryId = 3, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 5, ProductId = 3, CategoryId = 2, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 6, ProductId = 3, CategoryId = 3, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 7, ProductId = 4, CategoryId = 2, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new CategoryProduct { Id = 8, ProductId = 4, CategoryId = 3, CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Product Colors
            modelBuilder.Entity<ProductColor>().HasData(
                new ProductColor { Id = 1, ProductId = 1, ColorId = 4, MainImageId = 1, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 2, ProductId = 1, ColorId = 3, MainImageId = 8, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 3, ProductId = 1, ColorId = 11, MainImageId = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 4, ProductId = 1, ColorId = 5, MainImageId = 22, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 5, ProductId = 1, ColorId = 2, MainImageId = 29, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 6, ProductId = 1, ColorId = 1, MainImageId = 36, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 7, ProductId = 1, ColorId = 8, MainImageId = 43, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 8, ProductId = 1, ColorId = 9, MainImageId = 50, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 9, ProductId = 1, ColorId = 6, MainImageId = 57, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 10, ProductId = 1, ColorId = 10, MainImageId = 64, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 11, ProductId = 1, ColorId = 7, MainImageId = 71, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 12, ProductId = 2, ColorId = 3, MainImageId = 78, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 13, ProductId = 2, ColorId = 1, MainImageId = 85, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 14, ProductId = 2, ColorId = 6, MainImageId = 92, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 15, ProductId = 2, ColorId = 5, MainImageId = 99, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 16, ProductId = 3, ColorId = 2, MainImageId = 106, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 17, ProductId = 3, ColorId = 6, MainImageId = 113, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 18, ProductId = 3, ColorId = 3, MainImageId = 120, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 19, ProductId = 3, ColorId = 7, MainImageId = 127, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 20, ProductId = 3, ColorId = 5, MainImageId = 134, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 21, ProductId = 3, ColorId = 1, MainImageId = 141, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 22, ProductId = 3, ColorId = 9, MainImageId = 148, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 23, ProductId = 4, ColorId = 2, MainImageId = 155, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 24, ProductId = 4, ColorId = 1, MainImageId = 162, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 25, ProductId = 4, ColorId = 9, MainImageId = 169, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColor { Id = 26, ProductId = 4, ColorId = 6, MainImageId = 176, CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            modelBuilder.Entity<ProductColorImage>().HasData(
                new ProductColorImage { Id = 1, ProductColorId = 1, ImagePath = "/images/product-color-images/A10604M080_Natural_Black_Blizzard_ANGLE-192.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 2, ProductColorId = 1, ImagePath = "/images/product-color-images/A10604M080_Natural_Black_Blizzard_LEFT-271.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 3, ProductColorId = 1, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 4, ProductColorId = 1, ImagePath = "/images/product-color-images/A10604M080_Natural_Black_Blizzard_BACK-234.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 5, ProductColorId = 1, ImagePath = "/images/product-color-images/A10604M080_Natural_Black_Blizzard_TOP-579.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 6, ProductColorId = 1, ImagePath = "/images/product-color-images/A10604M080_Natural_Black_Blizzard_BOTTOM-409.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 7, ProductColorId = 1, ImagePath = "/images/product-color-images/A10604M080_Natural_Black_Blizzard_PAIR-715.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 8, ProductColorId = 2, ImagePath = "/images/product-color-images/WR2-FRONT-WOOL-RUNNER2-DARK_GREY_LIGHT_GREY-581.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 9, ProductColorId = 2, ImagePath = "/images/product-color-images/WR2-EYELET-WOOL-RUNNER2-DARK_GREY_LIGHT_GREY-586.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 10, ProductColorId = 2, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 11, ProductColorId = 2, ImagePath = "/images/product-color-images/WR2-TOP-WOOL-RUNNER2-DARK_GREY_LIGHT_GREY-483.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 12, ProductColorId = 2, ImagePath = "/images/product-color-images/WR2-PAIR-WOOL-RUNNER2-DARK_GREY_LIGHT_GREY-585.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 13, ProductColorId = 2, ImagePath = "/images/product-color-images/WR2-BACK-WOOL-RUNNER2-DARK_GREY_LIGHT_GREY_1-823.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 14, ProductColorId = 2, ImagePath = "/images/product-color-images/WR2-HEEL-WOOL-RUNNER2-DARK_GREY_LIGHT_GREY-2.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 15, ProductColorId = 3, ImagePath = "/images/product-color-images/A11002_24Q4_Wool_Runner_2_Stony_Beige_Stony_Cream_PDP_SINGLE_3Q-2000x2000-165.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 16, ProductColorId = 3, ImagePath = "/images/product-color-images/A11002_24Q4_Wool_Runner_2_Stony_Beige_Stony_Cream_PDP_LEFT-2000x2000-608.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 17, ProductColorId = 3, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 18, ProductColorId = 3, ImagePath = "/images/product-color-images/A11002_24Q4_Wool_Runner_2_Stony_Beige_Stony_Cream_PDP_BACK-2000x2000-482.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 19, ProductColorId = 3, ImagePath = "/images/product-color-images/A11002_24Q4_Wool_Runner_2_Stony_Beige_Stony_Cream_PDP_TD-2000x2000-920.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 20, ProductColorId = 3, ImagePath = "/images/product-color-images/A11002_24Q4_Wool_Runner_2_Stony_Beige_Stony_Cream_PDP_SOLE-2000x2000-864.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 21, ProductColorId = 3, ImagePath = "/images/product-color-images/A11002_24Q4_Wool_Runner_2_Stony_Beige_Stony_Cream_PDP_PAIR_3Q-2000x2000-314.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 22, ProductColorId = 4, ImagePath = "/images/product-color-images/A10649_S24Q2_Wool_Runner_2_Rugged_Beige_Rugged_Beige_PDP_SINGLE_3Q_c3e5b7a0-ab98-40a6-8555-1d6fc45e25eb-128.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 23, ProductColorId = 4, ImagePath = "/images/product-color-images/A10649_S24Q2_Wool_Runner_2_Rugged_Beige_Rugged_Beige_PDP_LEFT_1b1e4717-d33b-413f-896e-4efc84fe41ba-515.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 24, ProductColorId = 4, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 25, ProductColorId = 4, ImagePath = "/images/product-color-images/A10649_S24Q2_Wool_Runner_2_Rugged_Beige_Rugged_Beige_PDP_BACK_9a032f63-d294-4cdd-baf9-7c4cf4303729-687.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 26, ProductColorId = 4, ImagePath = "/images/product-color-images/A10649_S24Q2_Wool_Runner_2_Rugged_Beige_Rugged_Beige_PDP_TD_2d99f3b2-c482-4fed-860f-3a948ac181ec-952.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 27, ProductColorId = 4, ImagePath = "/images/product-color-images/A10649_S24Q2_Wool_Runner_2_Rugged_Beige_Rugged_Beige_PDP_SOLE_57e9cf51-a6e2-4e61-b44e-ef697e4d39ac-931.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 28, ProductColorId = 4, ImagePath = "/images/product-color-images/A10649_S24Q2_Wool_Runner_2_Rugged_Beige_Rugged_Beige_PDP_PAIR_3Q_ea52e9ce-4161-417e-ba75-1d465e16f9cb-466.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 29, ProductColorId = 5, ImagePath = "/images/product-color-images/A10479W080_Medium_Grey__Blizzard_ANGLE_4a090b1a-66f3-43b9-99db-be6f70d9de6c-576.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 30, ProductColorId = 5, ImagePath = "/images/product-color-images/A10479W080_Medium_Grey__Blizzard_LEFT_63b2636e-8bd1-4cc8-8b6e-691a12f81f6d-858.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 31, ProductColorId = 5, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 32, ProductColorId = 5, ImagePath = "/images/product-color-images/A10479W080_Medium_Grey__Blizzard_BACK_79b4344e-f93f-4b92-8097-d627ffdbbaa0-282.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 33, ProductColorId = 5, ImagePath = "/images/product-color-images/A10479W080_Medium_Grey__Blizzard_TOP_4ec940c0-9752-4aaa-aa1e-4f6624269bca-30.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 34, ProductColorId = 5, ImagePath = "/images/product-color-images/A10479W080_Medium_Grey__Blizzard_BOTTOM_97b4d3f7-4f04-49ac-8427-f10f4247adb0-790.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 35, ProductColorId = 5, ImagePath = "/images/product-color-images/A10479W080_Medium_Grey__Blizzard_PAIR_2a49b21d-9b2c-4d40-abea-cb560abbb8b8-824.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 36, ProductColorId = 6, ImagePath = "/images/product-color-images/A10596_Natural_Black_Natural_Black_ANGLE_a3ccbb37-c8e2-4e94-975f-467e544f4717-316.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 37, ProductColorId = 6, ImagePath = "/images/product-color-images/A10596_Natural_Black_Natural_Black_LEFT_c6ffe215-d204-4def-9bdf-26607a51e422-379.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 38, ProductColorId = 6, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 39, ProductColorId = 6, ImagePath = "/images/product-color-images/A10596_Natural_Black_Natural_Black_BACK_552cddb9-fd84-4ebc-aa7e-10e25b6d4b9e-534.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 40, ProductColorId = 6, ImagePath = "/images/product-color-images/A10596_Natural_Black_Natural_Black_TOP_7eaad72d-49b4-441f-aad8-a57ed71d05b8-210.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 41, ProductColorId = 6, ImagePath = "/images/product-color-images/A10596_Natural_Black_Natural_Black_BOTTOM_46e40151-1b0d-497c-affd-f2c8f4776441-52.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 42, ProductColorId = 6, ImagePath = "/images/product-color-images/A10596_Natural_Black_Natural_Black_PAIR_e97ba6f3-7774-4c74-ac61-32326e824a13-427.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 43, ProductColorId = 7, ImagePath = "/images/product-color-images/A10650_S24Q2_Wool_Runner_2_Rugged_Green_Stony_Cream_PDP_SINGLE_3Q-760.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 44, ProductColorId = 7, ImagePath = "/images/product-color-images/A10650_S24Q2_Wool_Runner_2_Rugged_Green_Stony_Cream_PDP_LEFT-321.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 45, ProductColorId = 7, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 46, ProductColorId = 7, ImagePath = "/images/product-color-images/A10650_S24Q2_Wool_Runner_2_Rugged_Green_Stony_Cream_PDP_BACK-446.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 47, ProductColorId = 7, ImagePath = "/images/product-color-images/A10650_S24Q2_Wool_Runner_2_Rugged_Green_Stony_Cream_PDP_TD-276.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 48, ProductColorId = 7, ImagePath = "/images/product-color-images/A10650_S24Q2_Wool_Runner_2_Rugged_Green_Stony_Cream_PDP_SOLE-575.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 49, ProductColorId = 7, ImagePath = "/images/product-color-images/A10650_S24Q2_Wool_Runner_2_Rugged_Green_Stony_Cream_PDP_PAIR_3Q-594.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 50, ProductColorId = 8, ImagePath = "/images/product-color-images/A10593_Natural_White_Natural_White_ANGLE_02a61af4-3a65-4df7-a189-148aa237d9e1-560.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 51, ProductColorId = 8, ImagePath = "/images/product-color-images/A10593_Natural_White_Natural_White_LEFT_6e7851c2-644d-4a11-bea6-24e31b9ab3f6-566.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 52, ProductColorId = 8, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 53, ProductColorId = 8, ImagePath = "/images/product-color-images/A10593_Natural_White_Natural_White_BACK_63ad5207-3901-4fd1-8870-e5c9f452d116-806.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 54, ProductColorId = 8, ImagePath = "/images/product-color-images/A10593_Natural_White_Natural_White_TOP_1d2d7a63-cb5a-4f20-a098-05b1980cfe70-63.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 55, ProductColorId = 8, ImagePath = "/images/product-color-images/A10593_Natural_White_Natural_White_BOTTOM_19907566-87f0-405a-b1b6-3ddc179f54e9-340.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 56, ProductColorId = 8, ImagePath = "/images/product-color-images/A10593_Natural_White_Natural_White_PAIR_c448037f-36a4-43a1-999a-88e79c4195bd-217.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 57, ProductColorId = 9, ImagePath = "/images/product-color-images/A10989_24Q3_Wool_Runner_2_Deep_Navy_True_Navy_PDP_SINGLE_3Q-2000x2000-980.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 58, ProductColorId = 9, ImagePath = "/images/product-color-images/A10989_24Q3_Wool_Runner_2_Deep_Navy_True_Navy_PDP_LEFT-2000x2000-791.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 59, ProductColorId = 9, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 60, ProductColorId = 9, ImagePath = "/images/product-color-images/A10989_24Q3_Wool_Runner_2_Deep_Navy_True_Navy_PDP_BACK-2000x2000-424.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 61, ProductColorId = 9, ImagePath = "/images/product-color-images/A10989_24Q3_Wool_Runner_2_Deep_Navy_True_Navy_PDP_TD-2000x2000-196.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 62, ProductColorId = 9, ImagePath = "/images/product-color-images/A10989_24Q3_Wool_Runner_2_Deep_Navy_True_Navy_PDP_SOLE-2000x2000-695.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 63, ProductColorId = 9, ImagePath = "/images/product-color-images/A10989_24Q3_Wool_Runner_2_Deep_Navy_True_Navy_PDP_PAIR_3Q-2000x2000-449.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 64, ProductColorId = 10, ImagePath = "/images/product-color-images/WR2-FRONT-WOOL-RUNNER2-HAZY_INDIGO_BLIZZARD-657.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 65, ProductColorId = 10, ImagePath = "/images/product-color-images/WR2-EYELET-WOOL-RUNNER2-HAZY_INDIGO_BLIZZARD-830.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 66, ProductColorId = 10, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 67, ProductColorId = 10, ImagePath = "/images/product-color-images/WR2-TOP-WOOL-RUNNER2-HAZY_INDIGO_BLIZZARD-497.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 68, ProductColorId = 10, ImagePath = "/images/product-color-images/WR2-PAIR-WOOL-RUNNER2-HAZY_INDIGO_BLIZZARD-265.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 69, ProductColorId = 10, ImagePath = "/images/product-color-images/WR2-BACK-WOOL-RUNNER2-HAZY_INDIGO_BLIZZARD-734.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 70, ProductColorId = 10, ImagePath = "/images/product-color-images/WR2-HEEL-WOOL-RUNNER2-HAZY_INDIGO_BLIZZARD-356.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 71, ProductColorId = 11, ImagePath = "/images/product-color-images/WR2-FRONT-WOOL-RUNNER2-NATURAL_BLACK_KEA_RED_NATURAL_WHITE-241.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 72, ProductColorId = 11, ImagePath = "/images/product-color-images/WR2-EYELET-WOOL-RUNNER2-NATURAL_BLACK_KEA_RED_NATURAL_WHITE-724.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 73, ProductColorId = 11, ImagePath = "/images/product-color-images/23Q4-WR2-PDP-NaturalBlack-1600x1600-27.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 74, ProductColorId = 11, ImagePath = "/images/product-color-images/WR2-TOP-WOOL-RUNNER2-NATURAL_BLACK_KEA_RED_NATURAL_WHITE_v2__1-612.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 75, ProductColorId = 11, ImagePath = "/images/product-color-images/WR2-PAIR-WOOL-RUNNER2-NATURAL_BLACK_KEA_RED_NATURAL_WHITE_v2-777.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 76, ProductColorId = 11, ImagePath = "/images/product-color-images/WR2-BACK-WOOL-RUNNER2-NATURAL_BLACK_KEA_RED_NATURAL_WHITE-167.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 77, ProductColorId = 11, ImagePath = "/images/product-color-images/WR2-HEEL-WOOL-RUNNER2-NATURAL_BLACK_KEA_RED_NATURAL_WHITE-318.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 78, ProductColorId = 12, ImagePath = "/images/product-color-images/A10977_24Q3_Wool_Piper_2_Medium_Grey_Natural_White_PDP_SINGLE_3Q-2000x2000-654.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 79, ProductColorId = 12, ImagePath = "/images/product-color-images/A10977_24Q3_Wool_Piper_2_Medium_Grey_Natural_White_PDP_LEFT-2000x2000-315.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 80, ProductColorId = 12, ImagePath = "/images/product-color-images/24Q3_WoolPiperGo_Site_PDP_Desktop_1600x1600_Men-59.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 81, ProductColorId = 12, ImagePath = "/images/product-color-images/A10977_24Q3_Wool_Piper_2_Medium_Grey_Natural_White_PDP_BACK-2000x2000-272.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 82, ProductColorId = 12, ImagePath = "/images/product-color-images/A10977_24Q3_Wool_Piper_2_Medium_Grey_Natural_White_PDP_TD-2000x2000-18.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 83, ProductColorId = 12, ImagePath = "/images/product-color-images/A10977_24Q3_Wool_Piper_2_Medium_Grey_Natural_White_PDP_SOLE-2000x2000-563.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 84, ProductColorId = 12, ImagePath = "/images/product-color-images/A10977_24Q3_Wool_Piper_2_Medium_Grey_Natural_White_PDP_PAIR_3Q-2000x2000-31.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 85, ProductColorId = 13, ImagePath = "/images/product-color-images/A10976_24Q3_Wool_Piper_2_Natural_Black_Natural_White_PDP_SINGLE_3Q-2000x2000_3572abf1-3e82-49e5-b4d7-a3a4caf0d87e-46.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 86, ProductColorId = 13, ImagePath = "/images/product-color-images/A10976_24Q3_Wool_Piper_2_Natural_Black_Natural_White_PDP_LEFT-2000x2000-459.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 87, ProductColorId = 13, ImagePath = "/images/product-color-images/24Q3_WoolPiperGo_Site_PDP_Desktop_1600x1600_Men-59.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 88, ProductColorId = 13, ImagePath = "/images/product-color-images/A10976_24Q3_Wool_Piper_2_Natural_Black_Natural_White_PDP_BACK-2000x2000-283.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 89, ProductColorId = 13, ImagePath = "/images/product-color-images/A10976_24Q3_Wool_Piper_2_Natural_Black_Natural_White_PDP_TD-2000x2000-466.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 90, ProductColorId = 13, ImagePath = "/images/product-color-images/A10976_24Q3_Wool_Piper_2_Natural_Black_Natural_White_PDP_SOLE-2000x2000-499.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 91, ProductColorId = 13, ImagePath = "/images/product-color-images/A10976_24Q3_Wool_Piper_2_Natural_Black_Natural_White_PDP_PAIR_3Q-2000x2000-803.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 92, ProductColorId = 14, ImagePath = "/images/product-color-images/A10979_24Q3_Wool_Piper_2_Deep_Navy_Natural_White_PDP_SINGLE_3Q-2000x2000_55e869ae-e294-462d-85a9-247a6e2e26b7-933.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 93, ProductColorId = 14, ImagePath = "/images/product-color-images/A10979_24Q3_Wool_Piper_2_Deep_Navy_Natural_White_PDP_LEFT-2000x2000_72a7ef7d-22c4-40c3-a203-93fb1cd833bc-611.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 94, ProductColorId = 14, ImagePath = "/images/product-color-images/24Q3_WoolPiperGo_Site_PDP_Desktop_1600x1600_Men-59.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 95, ProductColorId = 14, ImagePath = "/images/product-color-images/A10979_24Q3_Wool_Piper_2_Deep_Navy_Natural_White_PDP_BACK-2000x2000_cbc4952d-e6ff-4a58-9c2c-5f65bc15d086-298.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 96, ProductColorId = 14, ImagePath = "/images/product-color-images/A10979_24Q3_Wool_Piper_2_Deep_Navy_Natural_White_PDP_TD-2000x2000_679af855-1975-46ec-9e7b-4808125297c8-547.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 97, ProductColorId = 14, ImagePath = "/images/product-color-images/A10979_24Q3_Wool_Piper_2_Deep_Navy_Natural_White_PDP_SOLE-2000x2000_01427d76-cb7b-4f5c-9dd7-e03e73aea984-813.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 98, ProductColorId = 14, ImagePath = "/images/product-color-images/A10979_24Q3_Wool_Piper_2_Deep_Navy_Natural_White_PDP_PAIR_3Q-2000x2000_c878530a-6244-48e1-abd3-0f97acfcf545-585.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 99, ProductColorId = 15, ImagePath = "/images/product-color-images/A10975_24Q3_Wool_Piper_2_Stony_Beige_Stony_Cream_PDP_SINGLE_3Q-2000x2000-657.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 100, ProductColorId = 15, ImagePath = "/images/product-color-images/A10975_24Q3_Wool_Piper_2_Stony_Beige_Stony_Cream_PDP_LEFT-2000x2000-265.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 101, ProductColorId = 15, ImagePath = "/images/product-color-images/24Q3_WoolPiperGo_Site_PDP_Desktop_1600x1600_Men-59.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 102, ProductColorId = 15, ImagePath = "/images/product-color-images/A10975_24Q3_Wool_Piper_2_Stony_Beige_Stony_Cream_PDP_BACK-2000x2000_628f07fe-4f89-4878-a2fa-78e17432003e-341.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 103, ProductColorId = 15, ImagePath = "/images/product-color-images/A10975_24Q3_Wool_Piper_2_Stony_Beige_Stony_Cream_PDP_TD-2000x2000_65b30662-a5b1-41c1-82ad-9039736c8729-650.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 104, ProductColorId = 15, ImagePath = "/images/product-color-images/A10975_24Q3_Wool_Piper_2_Stony_Beige_Stony_Cream_PDP_SOLE-2000x2000_13c8d498-57df-419e-99e8-c7ffb24caf9d-250.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 105, ProductColorId = 15, ImagePath = "/images/product-color-images/A10975_24Q3_Wool_Piper_2_Stony_Beige_Stony_Cream_PDP_PAIR_3Q-2000x2000_d6213365-b8d5-4718-83ce-4b415293ae5c-534.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 106, ProductColorId = 16, ImagePath = "/images/product-color-images/A10598M080_Light_Grey_Medium_Grey_Blizzard_ANGLE-1200x1200-899.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 107, ProductColorId = 16, ImagePath = "/images/product-color-images/A10598M080_Light_Grey_Medium_Grey_Blizzard_LEFT-1200x1200-633.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 108, ProductColorId = 16, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 109, ProductColorId = 16, ImagePath = "/images/product-color-images/A10598M080_Light_Grey_Medium_Grey_Blizzard_BACK-1200x1200-156.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 110, ProductColorId = 16, ImagePath = "/images/product-color-images/A10598M080_Light_Grey_Medium_Grey_Blizzard_TOP-1200x1200-508.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 111, ProductColorId = 16, ImagePath = "/images/product-color-images/A10598M080_Light_Grey_Medium_Grey_Blizzard_BOTTOM-1200x1200-65.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 112, ProductColorId = 16, ImagePath = "/images/product-color-images/A10598M080_Light_Grey_Medium_Grey_Blizzard_PAIR-1200x1200-115.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 113, ProductColorId = 17, ImagePath = "/images/product-color-images/A10675_S24Q2_SuperLight_TR_Basin_Blue_Light_Grey_PDP_SINGLE_3Q_04db9d12-3826-4d7e-848a-41faaf948de8-836.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 114, ProductColorId = 17, ImagePath = "/images/product-color-images/A10675_S24Q2_SuperLight_TR_Basin_Blue_Light_Grey_PDP_LEFT_53604223-2fd8-43f0-9578-a5b27a45bf7f-519.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 115, ProductColorId = 17, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 116, ProductColorId = 17, ImagePath = "/images/product-color-images/A10675_S24Q2_SuperLight_TR_Basin_Blue_Light_Grey_PDP_BACK_fc256794-e7d1-4696-8fa1-ee7468345ba1-85.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 117, ProductColorId = 17, ImagePath = "/images/product-color-images/A10675_S24Q2_SuperLight_TR_Basin_Blue_Light_Grey_PDP_TD_769a34a2-1f04-415f-90ff-8e6be382191c-625.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 118, ProductColorId = 17, ImagePath = "/images/product-color-images/A10675_S24Q2_SuperLight_TR_Basin_Blue_Light_Grey_PDP_SOLE_f9d45a3e-ff15-4d20-a3de-2c0b6738d05e-635.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 119, ProductColorId = 17, ImagePath = "/images/product-color-images/A10675_S24Q2_SuperLight_TR_Basin_Blue_Light_Grey_PDP_PAIR_3Q_63780f17-9879-4fe2-ae34-7ef61ad79c46-674.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 120, ProductColorId = 18, ImagePath = "/images/product-color-images/A10673_SUPERLIGHT_TR_SVP_GLOBAL_MENS_SUPERLIGHT_BLIZZARD_NATURAL_BLACK_BLIZZARD-344.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 121, ProductColorId = 18, ImagePath = "/images/product-color-images/A10673_SUPERLIGHT_TR_ALT1_GLOBAL_MENS_SUPERLIGHT_BLIZZARD_NATURAL_BLACK_BLIZZARD-807.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 122, ProductColorId = 18, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 123, ProductColorId = 18, ImagePath = "/images/product-color-images/A10673_SUPERLIGHT_TR_TVP_GLOBAL_MENS_SUPERLIGHT_BLIZZARD_NATURAL_BLACK_BLIZZARD-450.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 124, ProductColorId = 18, ImagePath = "/images/product-color-images/A10673_SUPERLIGHT_TR_BVP_GLOBAL_MENS_SUPERLIGHT_BLIZZARD_NATURAL_BLACK_BLIZZARD-256.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 125, ProductColorId = 18, ImagePath = "/images/product-color-images/A10673_SUPERLIGHT_TR_ALT_GLOBAL_MENS_SUPERLIGHT_BLIZZARD_NATURAL_BLACK_BLIZZARD-99.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 126, ProductColorId = 18, ImagePath = "/images/product-color-images/A10673_SUPERLIGHT_TR_TVP2_GLOBAL_MENS_SUPERLIGHT_BLIZZARD_NATURAL_BLACK_BLIZZARD-435.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 127, ProductColorId = 19, ImagePath = "/images/product-color-images/A10917_24Q3_SuperLight_TR_Blizzard_Multi_Blizzard_PDP_SINGLE_3Q_6893c889-2ad0-4b4c-9dcd-05d0b21f1174-588.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 128, ProductColorId = 19, ImagePath = "/images/product-color-images/A10917_24Q3_SuperLight_TR_Blizzard_Multi_Blizzard_PDP_LEFT_5056e486-75f0-40fd-bb4a-8e6411f58cb1-691.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 129, ProductColorId = 19, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 130, ProductColorId = 19, ImagePath = "/images/product-color-images/A10917_24Q3_SuperLight_TR_Blizzard_Multi_Blizzard_PDP_BACK_f3dc09b4-0d4e-45a6-93cd-c26423e356d5-987.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 131, ProductColorId = 19, ImagePath = "/images/product-color-images/A10917_24Q3_SuperLight_TR_Blizzard_Multi_Blizzard_PDP_TD_3074c1fe-691f-4ee8-90c4-ff2ab146cd46-783.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 132, ProductColorId = 19, ImagePath = "/images/product-color-images/A10917_24Q3_SuperLight_TR_Blizzard_Multi_Blizzard_PDP_SOLE_306d15fe-ea1f-4242-a61d-4592b15f7009-267.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 133, ProductColorId = 19, ImagePath = "/images/product-color-images/A10917_24Q3_SuperLight_TR_Blizzard_Multi_Blizzard_PDP_PAIR_3Q_8b849da4-b739-4eca-8515-d12d6933a73d-134.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 134, ProductColorId = 20, ImagePath = "/images/product-color-images/A10634_S24Q2_SuperLight_TR_Rugged_Beige_Natural_White_PDP_SINGLE_3Q-1200x1200-279.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 135, ProductColorId = 20, ImagePath = "/images/product-color-images/A10634_S24Q2_SuperLight_TR_Rugged_Beige_Natural_White_PDP_LEFT-1200x1200-624.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 136, ProductColorId = 20, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 137, ProductColorId = 20, ImagePath = "/images/product-color-images/A10634_S24Q2_SuperLight_TR_Rugged_Beige_Natural_White_PDP_BACK-1200x1200-286.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 138, ProductColorId = 20, ImagePath = "/images/product-color-images/A10634_S24Q2_SuperLight_TR_Rugged_Beige_Natural_White_PDP_TD-1200x1200-849.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 139, ProductColorId = 20, ImagePath = "/images/product-color-images/A10634_S24Q2_SuperLight_TR_Rugged_Beige_Natural_White_PDP_SOLE-1200x1200-191.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 140, ProductColorId = 20, ImagePath = "/images/product-color-images/A10634_S24Q2_SuperLight_TR_Rugged_Beige_Natural_White_PDP_PAIR_3Q-1200x1200-933.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 141, ProductColorId = 21, ImagePath = "/images/product-color-images/A10619M080_Natural_Black_Dark_Grey_Blizzard_ANGLE-1200x1200-775.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 142, ProductColorId = 21, ImagePath = "/images/product-color-images/A10619M080_Natural_Black_Dark_Grey_Blizzard_LEFT-1200x1200-752.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 143, ProductColorId = 21, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 144, ProductColorId = 21, ImagePath = "/images/product-color-images/A10619M080_Natural_Black_Dark_Grey_Blizzard_BACK-1200x1200-523.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 145, ProductColorId = 21, ImagePath = "/images/product-color-images/A10619M080_Natural_Black_Dark_Grey_Blizzard_TOP-1200x1200-680.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 146, ProductColorId = 21, ImagePath = "/images/product-color-images/A10619M080_Natural_Black_Dark_Grey_Blizzard_BOTTOM-1200x1200-356.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 147, ProductColorId = 21, ImagePath = "/images/product-color-images/A10619M080_Natural_Black_Dark_Grey_Blizzard_PAIR-1200x1200-706.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 148, ProductColorId = 22, ImagePath = "/images/product-color-images/AB00F7M100_SHOE_45_GLOBAL_MENS_SUPERLIGHT_TR_BLIZZARD_BLIZZARD_bc2f67e1-1054-405f-ad17-7dbca608be91-974.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 149, ProductColorId = 22, ImagePath = "/images/product-color-images/AB00F7M100_SHOE_PROFILE_GLOBAL_MENS_SUPERLIGHT_TR_BLIZZARD_BLIZZARD_62651fdc-f735-432c-94d8-2d2cb1583781-381.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 150, ProductColorId = 22, ImagePath = "/images/product-color-images/Allbirds_SpringShoot_Home_MW_Shot43_4058.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 151, ProductColorId = 22, ImagePath = "/images/product-color-images/AB00F7M100_SHOE_HEEL_GLOBAL_MENS_SUPERLIGHT_TR_BLIZZARD_BLIZZARD_b40aa617-6bfc-41a0-858a-b76d4eb7186f-916.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 152, ProductColorId = 22, ImagePath = "/images/product-color-images/AB00F7M100_SHOE_OVERHEAD_GLOBAL_MENS_SUPERLIGHT_TR_BLIZZARD_BLIZZARD_7950e2b1-9cf4-432f-9f70-6bb32f2c4613-810.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 153, ProductColorId = 22, ImagePath = "/images/product-color-images/AB00F7M100_SHOE_BOTTOM_GLOBAL_MENS_SUPERLIGHT_TR_BLIZZARD_BLIZZARD_aaa93888-fc11-44d9-b55c-b594857911d8-963.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 154, ProductColorId = 22, ImagePath = "/images/product-color-images/AB00F7M100_SHOE_PAIR_GLOBAL_MENS_SUPERLIGHT_TR_BLIZZARD_BLIZZARD_361bfe60-a133-4efe-881b-210d2d25bfda-349.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 155, ProductColorId = 23, ImagePath = "/images/product-color-images/A10196W080-Courier-45-Global-Womens-Medium_Grey-Light_Grey-Natural_White-CF1_40ed9af8-5218-4864-9dd6-e05f6ed1eedb-275.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 156, ProductColorId = 23, ImagePath = "/images/product-color-images/A10196W080-Courier-Side-Global-Womens-Medium_Grey-Light_Grey-Natural_White-CF1_f5bbfd73-ffe9-4838-9464-e8cc6de92882-148.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 157, ProductColorId = 23, ImagePath = "/images/product-color-images/23Q3_SNS_PDP-GRID-01_Courier-M-340.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 158, ProductColorId = 23, ImagePath = "/images/product-color-images/A10196W080-Courier-Heel-Global-Womens-Medium_Grey-Light_Grey-Natural_White-CF1_95afb15f-d375-431e-b222-5a6cd7f1e36b-741.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 159, ProductColorId = 23, ImagePath = "/images/product-color-images/A10196W080-Courier-Top-Global-Womens-Medium_Grey-Light_Grey-Natural_White-CF1_45a5d2d7-1443-41d8-819d-6335a0303cb8-3.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 160, ProductColorId = 23, ImagePath = "/images/product-color-images/A10196W080-Courier-Bottom-Global-Womens-Medium_Grey-Light_Grey-Natural_White-CF1_0c8ae6ab-339e-4a6a-a90d-5379e60e0ad4-623.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 161, ProductColorId = 23, ImagePath = "/images/product-color-images/A10196W080-Courier-Pair-Global-Womens-Medium_Grey-Light_Grey-Natural_White-CF1_795274d9-f008-4345-9905-96937a79955a-244.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 162, ProductColorId = 24, ImagePath = "/images/product-color-images/A10796_S24Q2_Courier_Dark_Grey_Natural_Black_Natural_White_PDP_SINGLE_3Q-1200x1200-861.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 163, ProductColorId = 24, ImagePath = "/images/product-color-images/A10796_S24Q2_Courier_Dark_Grey_Natural_Black_Natural_White_PDP_LEFT-1200x1200-860.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 164, ProductColorId = 24, ImagePath = "/images/product-color-images/23Q3_SNS_PDP-GRID-01_Courier-M-340.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 165, ProductColorId = 24, ImagePath = "/images/product-color-images/A10796_S24Q2_Courier_Dark_Grey_Natural_Black_Natural_White_PDP_BACK-1200x1200-238.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 166, ProductColorId = 24, ImagePath = "/images/product-color-images/A10796_S24Q2_Courier_Dark_Grey_Natural_Black_Natural_White_PDP_TD-1200x1200-90.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 167, ProductColorId = 24, ImagePath = "/images/product-color-images/A10796_S24Q2_Courier_Dark_Grey_Natural_Black_Natural_White_PDP_SOLE-1200x1200-532.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 168, ProductColorId = 24, ImagePath = "/images/product-color-images/A10796_S24Q2_Courier_Dark_Grey_Natural_Black_Natural_White_PDP_PAIR_3Q-1200x1200-870.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 169, ProductColorId = 25, ImagePath = "/images/product-color-images/A10795_S24Q2_Courier_Blizzard_Light_Grey_Natural_White_PDP_SINGLE_3Q-1200x1200-234.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 170, ProductColorId = 25, ImagePath = "/images/product-color-images/A10795_S24Q2_Courier_Blizzard_Light_Grey_Natural_White_PDP_LEFT-1200x1200-487.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 171, ProductColorId = 25, ImagePath = "/images/product-color-images/23Q3_SNS_PDP-GRID-01_Courier-M-340.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 172, ProductColorId = 25, ImagePath = "/images/product-color-images/A10795_S24Q2_Courier_Blizzard_Light_Grey_Natural_White_PDP_BACK-1200x1200-773.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 173, ProductColorId = 25, ImagePath = "/images/product-color-images/A10795_S24Q2_Courier_Blizzard_Light_Grey_Natural_White_PDP_TD-1200x1200-844.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 174, ProductColorId = 25, ImagePath = "/images/product-color-images/A10795_S24Q2_Courier_Blizzard_Light_Grey_Natural_White_PDP_SOLE-1200x1200-38.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 175, ProductColorId = 25, ImagePath = "/images/product-color-images/A10795_S24Q2_Courier_Blizzard_Light_Grey_Natural_White_PDP_PAIR_3Q-1200x1200-258.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 176, ProductColorId = 26, ImagePath = "/images/product-color-images/A10449W090-Courie-45-Global-Womans-Hazy_Indigo-True_Navy-Natural_White-CF1-181.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 177, ProductColorId = 26, ImagePath = "/images/product-color-images/A10449W090-Courie-Side-Global-Womans-Hazy_Indigo-True_Navy-Natural_White-CF1-773.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 178, ProductColorId = 26, ImagePath = "/images/product-color-images/23Q3_SNS_PDP-GRID-01_Courier-M-340.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 179, ProductColorId = 26, ImagePath = "/images/product-color-images/A10449W090-Courie-Heel-Global-Womans-Hazy_Indigo-True_Navy-Natural_White-CF1-581.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 180, ProductColorId = 26, ImagePath = "/images/product-color-images/A10449W090-Courie-Top-Global-Womans-Hazy_Indigo-True_Navy-Natural_White-CF1-139.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 181, ProductColorId = 26, ImagePath = "/images/product-color-images/A10449W090-Courie-Bottom-Global-Womans-Hazy_Indigo-True_Navy-Natural_White-CF1-421.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorImage { Id = 182, ProductColorId = 26, ImagePath = "/images/product-color-images/A10449W090-Courie-Pair-Global-Womans-Hazy_Indigo-True_Navy-Natural_White-CF1-874.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
            );
            // Seed Product Color Sizes
            modelBuilder.Entity<ProductColorSize>().HasData(
                new ProductColorSize { Id = 1, ProductColorId = 1, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 2, ProductColorId = 1, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 3, ProductColorId = 1, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 4, ProductColorId = 1, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 5, ProductColorId = 1, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 6, ProductColorId = 1, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 7, ProductColorId = 1, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 8, ProductColorId = 1, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 9, ProductColorId = 1, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 10, ProductColorId = 1, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 11, ProductColorId = 1, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 12, ProductColorId = 1, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 13, ProductColorId = 1, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 14, ProductColorId = 2, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 15, ProductColorId = 2, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 16, ProductColorId = 2, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 17, ProductColorId = 2, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 18, ProductColorId = 2, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 19, ProductColorId = 2, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 20, ProductColorId = 2, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 21, ProductColorId = 2, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 22, ProductColorId = 2, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 23, ProductColorId = 2, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 24, ProductColorId = 2, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 25, ProductColorId = 2, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 26, ProductColorId = 2, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 27, ProductColorId = 3, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 28, ProductColorId = 3, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 29, ProductColorId = 3, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 30, ProductColorId = 3, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 31, ProductColorId = 3, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 32, ProductColorId = 3, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 33, ProductColorId = 3, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 34, ProductColorId = 3, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 35, ProductColorId = 3, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 36, ProductColorId = 3, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 37, ProductColorId = 3, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 38, ProductColorId = 3, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 39, ProductColorId = 3, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 40, ProductColorId = 4, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 41, ProductColorId = 4, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 42, ProductColorId = 4, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 43, ProductColorId = 4, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 44, ProductColorId = 4, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 45, ProductColorId = 4, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 46, ProductColorId = 4, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 47, ProductColorId = 4, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 48, ProductColorId = 4, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 49, ProductColorId = 4, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 50, ProductColorId = 4, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 51, ProductColorId = 4, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 52, ProductColorId = 4, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 53, ProductColorId = 5, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 54, ProductColorId = 5, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 55, ProductColorId = 5, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 56, ProductColorId = 5, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 57, ProductColorId = 5, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 58, ProductColorId = 5, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 59, ProductColorId = 5, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 60, ProductColorId = 5, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 61, ProductColorId = 5, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 62, ProductColorId = 5, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 63, ProductColorId = 5, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 64, ProductColorId = 5, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 65, ProductColorId = 5, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 66, ProductColorId = 6, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 67, ProductColorId = 6, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 68, ProductColorId = 6, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 69, ProductColorId = 6, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 70, ProductColorId = 6, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 71, ProductColorId = 6, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 72, ProductColorId = 6, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 73, ProductColorId = 6, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 74, ProductColorId = 6, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 75, ProductColorId = 6, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 76, ProductColorId = 6, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 77, ProductColorId = 6, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 78, ProductColorId = 6, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 79, ProductColorId = 7, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 80, ProductColorId = 7, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 81, ProductColorId = 7, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 82, ProductColorId = 7, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 83, ProductColorId = 7, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 84, ProductColorId = 7, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 85, ProductColorId = 7, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 86, ProductColorId = 7, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 87, ProductColorId = 7, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 88, ProductColorId = 7, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 89, ProductColorId = 7, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 90, ProductColorId = 7, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 91, ProductColorId = 7, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 92, ProductColorId = 8, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 93, ProductColorId = 8, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 94, ProductColorId = 8, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 95, ProductColorId = 8, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 96, ProductColorId = 8, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 97, ProductColorId = 8, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 98, ProductColorId = 8, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 99, ProductColorId = 8, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 100, ProductColorId = 8, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 101, ProductColorId = 8, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 102, ProductColorId = 8, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 103, ProductColorId = 8, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 104, ProductColorId = 8, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 105, ProductColorId = 9, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 106, ProductColorId = 9, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 107, ProductColorId = 9, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 108, ProductColorId = 9, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 109, ProductColorId = 9, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 110, ProductColorId = 9, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 111, ProductColorId = 9, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 112, ProductColorId = 9, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 113, ProductColorId = 9, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 114, ProductColorId = 9, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 115, ProductColorId = 9, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 116, ProductColorId = 9, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 117, ProductColorId = 9, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 118, ProductColorId = 10, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 119, ProductColorId = 10, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 120, ProductColorId = 10, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 121, ProductColorId = 10, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 122, ProductColorId = 10, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 123, ProductColorId = 10, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 124, ProductColorId = 10, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 125, ProductColorId = 10, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 126, ProductColorId = 10, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 127, ProductColorId = 10, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 128, ProductColorId = 10, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 129, ProductColorId = 10, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 130, ProductColorId = 10, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 131, ProductColorId = 11, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 132, ProductColorId = 11, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 133, ProductColorId = 11, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 134, ProductColorId = 11, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 135, ProductColorId = 11, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 136, ProductColorId = 11, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 137, ProductColorId = 11, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 138, ProductColorId = 11, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 139, ProductColorId = 11, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 140, ProductColorId = 11, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 141, ProductColorId = 11, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 142, ProductColorId = 11, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 143, ProductColorId = 11, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 144, ProductColorId = 12, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 145, ProductColorId = 12, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 146, ProductColorId = 12, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 147, ProductColorId = 12, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 148, ProductColorId = 12, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 149, ProductColorId = 12, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 150, ProductColorId = 12, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 151, ProductColorId = 12, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 152, ProductColorId = 12, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 153, ProductColorId = 12, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 154, ProductColorId = 12, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 155, ProductColorId = 12, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 156, ProductColorId = 12, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 157, ProductColorId = 13, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 158, ProductColorId = 13, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 159, ProductColorId = 13, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 160, ProductColorId = 13, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 161, ProductColorId = 13, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 162, ProductColorId = 13, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 163, ProductColorId = 13, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 164, ProductColorId = 13, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 165, ProductColorId = 13, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 166, ProductColorId = 13, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 167, ProductColorId = 13, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 168, ProductColorId = 13, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 169, ProductColorId = 13, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 170, ProductColorId = 14, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 171, ProductColorId = 14, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 172, ProductColorId = 14, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 173, ProductColorId = 14, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 174, ProductColorId = 14, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 175, ProductColorId = 14, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 176, ProductColorId = 14, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 177, ProductColorId = 14, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 178, ProductColorId = 14, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 179, ProductColorId = 14, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 180, ProductColorId = 14, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 181, ProductColorId = 14, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 182, ProductColorId = 14, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 183, ProductColorId = 15, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 184, ProductColorId = 15, SizeId = 12, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 185, ProductColorId = 15, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 186, ProductColorId = 15, SizeId = 14, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 187, ProductColorId = 15, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 188, ProductColorId = 15, SizeId = 16, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 189, ProductColorId = 15, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 190, ProductColorId = 15, SizeId = 18, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 191, ProductColorId = 15, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 192, ProductColorId = 15, SizeId = 20, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 193, ProductColorId = 15, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 194, ProductColorId = 15, SizeId = 22, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 195, ProductColorId = 15, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 196, ProductColorId = 16, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 197, ProductColorId = 16, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 198, ProductColorId = 16, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 199, ProductColorId = 16, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 200, ProductColorId = 16, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 201, ProductColorId = 16, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 202, ProductColorId = 16, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 203, ProductColorId = 17, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 204, ProductColorId = 17, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 205, ProductColorId = 17, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 206, ProductColorId = 17, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 207, ProductColorId = 17, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 208, ProductColorId = 17, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 209, ProductColorId = 17, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 210, ProductColorId = 18, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 211, ProductColorId = 18, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 212, ProductColorId = 18, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 213, ProductColorId = 18, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 214, ProductColorId = 18, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 215, ProductColorId = 18, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 216, ProductColorId = 18, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 217, ProductColorId = 19, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 218, ProductColorId = 19, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 219, ProductColorId = 19, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 220, ProductColorId = 19, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 221, ProductColorId = 19, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 222, ProductColorId = 19, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 223, ProductColorId = 19, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 224, ProductColorId = 20, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 225, ProductColorId = 20, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 226, ProductColorId = 20, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 227, ProductColorId = 20, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 228, ProductColorId = 20, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 229, ProductColorId = 20, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 230, ProductColorId = 20, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 231, ProductColorId = 21, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 232, ProductColorId = 21, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 233, ProductColorId = 21, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 234, ProductColorId = 21, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 235, ProductColorId = 21, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 236, ProductColorId = 21, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 237, ProductColorId = 21, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 238, ProductColorId = 22, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 239, ProductColorId = 22, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 240, ProductColorId = 22, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 241, ProductColorId = 22, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 242, ProductColorId = 22, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 243, ProductColorId = 22, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 244, ProductColorId = 22, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 245, ProductColorId = 23, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 246, ProductColorId = 23, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 247, ProductColorId = 23, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 248, ProductColorId = 23, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 249, ProductColorId = 23, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 250, ProductColorId = 23, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 251, ProductColorId = 23, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 252, ProductColorId = 24, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 253, ProductColorId = 24, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 254, ProductColorId = 24, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 255, ProductColorId = 24, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 256, ProductColorId = 24, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 257, ProductColorId = 24, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 258, ProductColorId = 24, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 259, ProductColorId = 25, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 260, ProductColorId = 25, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 261, ProductColorId = 25, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 262, ProductColorId = 25, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 263, ProductColorId = 25, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 264, ProductColorId = 25, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 265, ProductColorId = 25, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 266, ProductColorId = 26, SizeId = 11, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 267, ProductColorId = 26, SizeId = 13, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 268, ProductColorId = 26, SizeId = 15, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 269, ProductColorId = 26, SizeId = 17, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 270, ProductColorId = 26, SizeId = 19, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 271, ProductColorId = 26, SizeId = 21, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductColorSize { Id = 272, ProductColorId = 26, SizeId = 23, UnitsInStock = 15, CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
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
            // Seed Product Specifications
            modelBuilder.Entity<ProductSpecification>().HasData(
                new ProductSpecification { Id = 1, ProductId = 1, SpecificationId = 1, ContentAr = "مدفوعًا بملاحظاتك ، يقدم Gener Go GOLENT GLEEN GOOL تنفيذًا دقيقًا دون التضحية بأفضل راحة في فئتها.", ContentEn = "Driven by your feedback, the next generation Wool Runner Go delivers on a refined execution without sacrificing its best-in-class comfort.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 2, ProductId = 1, SpecificationId = 2, ContentAr = "ارتداء كل يوم ، والتجول إلى المكتب أو بعد الحفلة ، والسفر حول العالم", ContentEn = "Everyday wear, strolling to the office or the after party, and traveling the world", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 3, ProductId = 1, SpecificationId = 16, ContentAr = "الجزء العلوي الناعم والمريح للغاية ، يمكنك ارتداءها طوال اليوم وما زلت لا ترغب في خلعها عندما تصل إلى المنزل.", ContentEn = "An upper that’s so soft and cozy, you can wear them all day and still not want to take them off when you get home.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 4, ProductId = 1, SpecificationId = 17, ContentAr = "بنية أكثر اتساقًا شاملة من شأنها أن تساعد في الحفاظ على شكل حذائك مع مرور الوقت.", ContentEn = "A more uniform all-around structure that will help maintain your shoe’s shape over time.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 5, ProductId = 1, SpecificationId = 18, ContentAr = "يتميز عداء الصوف القادم بتصميم تم إعادة تصميمه بالكامل لتناسب الغرفة وجمالية أكثر أناقة.", ContentEn = "The next generation Wool Runner Go boasts a completely re-engineered design for a roomier fit and a sleeker aesthetic.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 6, ProductId = 1, SpecificationId = 19, ContentAr = "لقد صقلنا الحذاء بأكمله أخيرًا (وهو قلب عملية صناعة الأحذية) مع تشكيل تشريحي في الكعب ، بالإضافة إلى صندوق أخمص القدمين المحسّن لتحسين الراحة والملاءمة.يتم تقديم عداء Wool Go بأحجام نصف ويتناسب مع الحجم لمعظم العملاء.إذا كان لديك أقدام واسعة أو تفضل أن يكون مناسبة لأصابع القدم ، فإننا نقترح ارتداء نصف الحجم.للحصول على أقدام واسعة إضافية ، تصعد بحجم كامل.", ContentEn = "We refined the entire shoe last (which is the heart of the shoemaking process) with anatomical shaping in the heel, plus a roomier toe-box for improved comfort and fit. The Wool Runner Go is offered in half sizes and fits true to size for most customers. If you have wide feet or prefer a roomier fit to accommodate toe splay, we suggest going up a half size. For extra wide feet, go up a whole size.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 7, ProductId = 1, SpecificationId = 5, ContentAr = "فيتنام.تعرف على المزيد حول عملياتنا.", ContentEn = "Vietnam. Learn more about our operations.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 8, ProductId = 2, SpecificationId = 1, ContentAr = "نمط كلاسيكي مرتفع مع التفاصيل المكررة ودعمًا ملموسًا ليكون اللمسة النهائية الأكثر راحة لأي ملاءمة.", ContentEn = "A classic style elevated with refined details and cushy support to be the comfiest finishing touch to any fit.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 9, ProductId = 2, SpecificationId = 2, ContentAr = "كل يوم ، عندما تريد أن تنظر معًا", ContentEn = "Everyday, when you want to look put together", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 10, ProductId = 2, SpecificationId = 15, ContentAr = "Bouncy Sweetfoam® Midsole يوفر قابلية ارتداء طوال اليوم.", ContentEn = "Bouncy SweetFoam® midsole delivers all-day wearability.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 11, ProductId = 2, SpecificationId = 9, ContentAr = "ارتد مع كل نمط يلتقي باللحظة", ContentEn = "Wear-with-everything style that meets the moment", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 12, ProductId = 2, SpecificationId = 5, ContentAr = "صنع في فيتنام.تعرف على المزيد حول عملياتنا.", ContentEn = "Made in Vietnam. Learn more about our operations.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 13, ProductId = 3, SpecificationId = 1, ContentAr = "أخف بطبيعته.تعرف على Superlight Tree Runner-حذاء رياضة كل يوم مصمم مع الجزء العلوي المتجدد التهوية ، وتكنولوجيا الرغوة الفائقة الثورية الجديدة لدينا من أجل إحساس بالكاد ، وتناسب الضوء على الهواء ، وهو أحد أخف وأدنى آثار أقدام الكربون حتى الآن.ونحن بدأنا للتو ...", ContentEn = "Lighter by nature. Meet the SuperLight Tree Runner – an everyday sneaker engineered with an airy, breathable upper and our new revolutionary SuperLight Foam technology for a barely-there feel, and light-as-air fit that’s one of our lightest and lowest carbon footprints to date. And we’re just getting started….", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 14, ProductId = 3, SpecificationId = 2, ContentAr = "المشي ، طقس أكثر دفئًا ، ارتداء كل يوم", ContentEn = "Walking, warmer weather, everyday wear", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 15, ProductId = 3, SpecificationId = 30, ContentAr = "يساعد على تقليل كمية المواد المستخدمة ويضع قدمك أقرب إلى الوسادة.", ContentEn = "Helps reduce the amount of material used and puts your foot even closer to the cushioning.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 16, ProductId = 3, SpecificationId = 5, ContentAr = "فيتنام.تعرف على المزيد حول عملياتنا.", ContentEn = "Vietnam. Learn more about our operations.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 17, ProductId = 4, SpecificationId = 1, ContentAr = "إيماءاتنا إلى حذاء رياضة عتيق مصنوع من مواد طبيعية لمستقبل أفضل.مرتفعة الصور الظلية الرجعية بأزواج تفاصيل معقدة مع أي شيء خططت له.تعال لأسلوب الإرهاق ، والبقاء من أجل الرغبة طوال اليوم.", ContentEn = "Our nod to a vintage sneaker made with natural materials for a better future. The retro silhouette elevated with intricate details pairs with anything you have planned. Come for the throwback style, and stay for the cushy all-day-wearability.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 18, ProductId = 4, SpecificationId = 2, ContentAr = "ارتداء كل يوم ، والمشي ، ومغامرات مرتجلة", ContentEn = "Everyday wear, walking, impromptu adventures", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 19, ProductId = 4, SpecificationId = 12, ContentAr = "فريد من نوعه sweetfoam® sweetfoam® midsole للحصول على راحة إضافية", ContentEn = "Unique scoop style SweetFoam® midsole for extra comfort", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 20, ProductId = 4, SpecificationId = 19, ContentAr = "ارتداء مع كل شيء كلاسيكي", ContentEn = "Wear-with-everything classic style", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductSpecification { Id = 21, ProductId = 4, SpecificationId = 5, ContentAr = "فيتنام.تعرف على المزيد حول عملياتنا.", ContentEn = "Vietnam. Learn more about our operations.", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }

            );
            // Seed Product Details
            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail { Id = 1, ProductId = 1, TitleAr = "التصميم المكرر", TitleEn = "Refined Design", DescriptionAr = "كل الأشياء التي تحبها في عداء الصوف الأصلي ولكن تم تجديدها لتقديم مساحة أكبر في أصابع القدم وهيكل إضافي للحصول", DescriptionEn = "All of the things you love about the original Wool Runner but revamped to offer more room in the toes and added structure for an effortless fit and streamlined look", ImagePath = "/images/product-details/WRGO_-_M1.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 2, ProductId = 1, TitleAr = "الدعم الربيعي", TitleEn = "Springy Support", DescriptionAr = "مصنوع من المزيد من وسادة الرغوة لتوصيل أفخم وشعور يشعر أن ترقية رحلتك - كل يوم ، كل يوم", DescriptionEn = "Made with even more foam cushioning to deliver a plusher and bouncier feel that upgrades your ride—all day, everyday", ImagePath = "/images/product-details/WRGO-M2.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 3, ProductId = 1, TitleAr = "100 ٪ آلة قابلة للغسل", TitleEn = "100% Machine Washable", DescriptionAr = "أحذيتنا قابلة للغسل بالكامل ، مما يجعل كل خطوة نظيفة مثل الأول", DescriptionEn = "Our shoes are fully washable, making every step as clean as the first", ImagePath = "/images/product-details/WR2-M3.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 4, ProductId = 2, TitleAr = "مادة ناعمة فائقة", TitleEn = "Super Soft Material", DescriptionAr = "المزيج العلوي المصنوع من الصوف هو فاخر لدرجة أنه يمكنك الذهاب إلى الوراء ولا تنظر أبدًا إلى الوراء.", DescriptionEn = "Upper blend made with wool is so luxe you can go sockless and never look back.", ImagePath = "/images/product-details/24Q3_WoolPiperGo_Site_Desktop_BTF_PDP_830x1150_1.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 5, ProductId = 2, TitleAr = "وسادة الخفيفة فائقة", TitleEn = "Ultra-Light Cushioning", DescriptionAr = "دعم Sweetfoam المريح ، القائم على قصب السكر يحافظ على باطن مريحة ودعم طوال اليوم وحتى الليل.", DescriptionEn = "Cushy, sugarcane-based SweetFoam support keeps soles comfy and supported all day-and even all night.", ImagePath = "/images/product-details/24Q3_WoolPiperGo_Site_Desktop_BTF_PDP_830x1150_2.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 6, ProductId = 2, TitleAr = "الجزء العلوي من المستوى التالي", TitleEn = "Next-Level Upper", DescriptionAr = "يبدو البناء المتماسك المكرر باردًا ومتطورًا من الخارج ولكنه لا يزال يشعر بالدفء والدافئ من الداخل.", DescriptionEn = "Refined knit construction looks cool and sophisticated on the outside but still feels warm and cozy on the inside.", ImagePath = "/images/product-details/24Q3_WoolPiperGo_Site_Desktop_BTF_PDP_830x1150_3.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 7, ProductId = 2, TitleAr = "الكلاسيكية عارضة", TitleEn = "Classic Casual", DescriptionAr = "صورة ظلية هذا الخالدة تضع نفسها على كل نمط أو مزاج أو فيبي.", DescriptionEn = "A silhouette this timeless lends itself to just about every style, mood, or vibe.", ImagePath = "/images/product-details/24Q3_WoolPiperGo_Site_Desktop_BTF_PDP_830x1150_4.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 8, ProductId = 2, TitleAr = "أقل أكثر", TitleEn = "Less Is More", DescriptionAr = "تفاصيل مدروسة مثل الجزء العلوي والوسطى المحكم إعطاء طلاء خفي يقطع شوطًا طويلاً.", DescriptionEn = "Thoughtful details like the textured upper and midsole give a subtle polish that goes a long way.", ImagePath = "/images/product-details/24Q3_WoolPiperGo_Site_Desktop_BTF_PDP_830x1150_5.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 9, ProductId = 2, TitleAr = "المتانة من الدرجة الأولى", TitleEn = "First-Class Durability", DescriptionAr = "يتم صنع كل غرزة للتعامل مع مطالب الحياة ، بما في ذلك غسالة الغسالة-حتى تدوم حذائك لفترة أطول وتبدو رائعة في القيام بذلك.", DescriptionEn = "Every stitch is made to handle life's demands, including the washing machine-so your shoes last longer and look great doing it.", ImagePath = "/images/product-details/24Q3_WoolPiperGo_Site_Desktop_BTF_PDP_830x1150_6.png", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 10, ProductId = 3, TitleAr = "أخف بطبيعته", TitleEn = "Lighter By Nature", DescriptionAr = "حذاء رياضة كل يوم مصمم مع رغوة Superlight الجديدة القائمة على Bio ، والتي تخفف كل خطوة", DescriptionEn = "An everyday sneaker engineered with our new ultralight bio-based SuperLight Foam, which lightens every step", ImagePath = "/images/product-details/Allbirds_SpringShoot_Home_MW_Shot43_4058__1_.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 11, ProductId = 3, TitleAr = "بالكاد-هناك قابلية للتنفس", TitleEn = "Barely-There Breathability", DescriptionAr = "مصنوع من ألياف الأشجار الموروقة ، ويحتضن متماسكة التنفس قدمك ويبقي الهواء يتدفق للراحة طوال اليوم", DescriptionEn = "Made with breezy tree fiber, the breathable knit hugs your foot and keeps the air flowing for all-day comfort", ImagePath = "/images/product-details/Allbirds19374.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 12, ProductId = 3, TitleAr = "100 ٪ آلة قابلة للغسل", TitleEn = "100% Machine Washable", DescriptionAr = "أحذيتنا قابلة للغسل بالكامل ، مما يجعل كل خطوة نظيفة مثل الأول", DescriptionEn = "Our shoes are fully washable, making every step as clean as the first", ImagePath = "/images/product-details/Allbirds_SpringShoot_Home_MW_Shot24_0187.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 13, ProductId = 4, TitleAr = "مصنوع من الطبيعة", TitleEn = "Made From Nature", DescriptionAr = "إيماءتنا إلى حذاء رياضة عتيق مصنوع من مواد طبيعية متينة مثل القطن العضوي ومزيج من ألياف الأشجار", DescriptionEn = "Our nod to a vintage sneaker made with durable natural materials like organic cotton and a tree fiber blend", ImagePath = "/images/product-details/24Q2_DallolEnergyPack_Site_ParentCollection_Courier_Story_Cards-Desktop-630x864.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 14, ProductId = 4, TitleAr = "دعم كل يوم", TitleEn = "Wear-All-Day Support", DescriptionAr = "جرب الراحة من Sweetfoam® ، نعلنا الوسطي المذكور مصنوعًا من قصب السكر ، مما يوفر سعادة طوال اليوم لقدميك", DescriptionEn = "Experience the comfort of SweetFoam®, our cushioned midsole made from sugarcane, providing all-day happiness for your feet", ImagePath = "/images/product-details/CaitOppermann_041823_AllBirds_SU23_Shot_05_001745-CO.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) },
                new ProductDetail { Id = 15, ProductId = 4, TitleAr = "100 ٪ آلة قابلة للغسل", TitleEn = "100% Machine Washable", DescriptionAr = "أحذيتنا قابلة للغسل بالكامل ، مما يجعل كل خطوة نظيفة مثل الأول", DescriptionEn = "Our shoes are fully washable, making every step as clean as the first", ImagePath = "/images/product-details/Allbirds_HO23_Location_LM_05E_0326.jpg", CreatedBy = 1, Created = new DateTime(2024, 10, 19) }
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
