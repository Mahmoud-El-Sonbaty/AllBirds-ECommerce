using AllBirds.Application.Contracts;
using AllBirds.Application.Mapper;
using AllBirds.Application.Services.AccountServices;
using AllBirds.Application.Services.CategoryProductServices;
using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ColorServices;
using AllBirds.Application.Services.CouponServices;
using AllBirds.Application.Services.OrderDetailServices;
using AllBirds.Application.Services.OrderMasterServices;
using AllBirds.Application.Services.OrderStateServices;
using AllBirds.Application.Services.ProductColorImageServices;
using AllBirds.Application.Services.ProductColorServices;
using AllBirds.Application.Services.ProductColorSizeServices;
using AllBirds.Application.Services.ProductDetailService;
using AllBirds.Application.Services.ProductServices;
using AllBirds.Application.Services.ProductSpecificationServices;
using AllBirds.Application.Services.SizeServices;
using AllBirds.Application.Services.SpecificationServices;
using AllBirds.Context;
using AllBirds.Infrastructure;
using AllBirds.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

//namespace AllBirds.ClientWebsiteAPI
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {

//        }
//    }
//}
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AllBirdsContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Account
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
// Category
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// CategoryProduct
builder.Services.AddScoped<ICategoryProductService, CategoryProductService>();
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
// ClientFavorite
//builder.Services.AddScoped<IClientFavoriteService, ClientFavoriteService>();
//builder.Services.AddScoped<IClientFavoriteRepository, ClientFavoriteRepository>();
// Color
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
// Coupon
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
// OrderDetail
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
// OrderMaster
builder.Services.AddScoped<IOrderMasterService, OrderMasterService>();
builder.Services.AddScoped<IOrderMasterRepository, OrderMasterRepository>();
// OrderState
builder.Services.AddScoped<IOrderStateService, OrderStateService>();
builder.Services.AddScoped<IOrderStateRepository, OrderStateRepository>();
// Product
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// ProductColor
builder.Services.AddScoped<IProductColorService, ProductColorService>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
// ProductColorImage
builder.Services.AddScoped<IProductColorImageRepository, ProductColorImageRepository>();
builder.Services.AddScoped<IProductColotImageService, ProductColorImageService>();
// ProductColorSize
builder.Services.AddScoped<IProductColorSizeService, ProductColorSizeService>();
builder.Services.AddScoped<IProductColorSizeRepository, ProductColorSizeRepository>();
// ProductDetail
builder.Services.AddScoped<IProductDetailsService, ProductDetailsService>();
builder.Services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
// ProductReview
//builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
//builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
// ProductSpecification
builder.Services.AddScoped<IProductSpecificationService, ProductSpecificationService>();
builder.Services.AddScoped<IProductSpecificationRepository, ProductSpecificationRepository>();
// Size
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
// Specification
builder.Services.AddScoped<ISpecificationService, SpecificationService>();
builder.Services.AddScoped<ISpecificationRepository, SpecificationRepository>();

builder.Services.AddIdentity<CustomUser, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AllBirdsContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sa =>
{
    sa.SwaggerDoc("v1", new OpenApiInfo //here the name (v1) must be (v1) small ant there is no relation with the version below
    {
        Title = "Client API",
        Version = "v1"
    });
    sa.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Insert JWT with Bearer Into Field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    sa.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(op =>
{
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
    };
});
builder.Services.AddCors(op =>
{
    op.AddPolicy("Default", policy =>
    {
        //policy.WithOrigins("http://localhost:4200", "http://anydomain:domainport", "null")
        //.WithHeaders("Authorization")
        //.WithMethods("Post", "Get");
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
    op.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://allbirds-orcin.vercel.app")
        .WithHeaders("Authorization")
        .AllowAnyMethod();
    });
});


builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("twoLetterLang", typeof(TwoLetterLangConstraint));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwagger();

    //// Ensure Swagger UI is configured with the correct endpoint
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/V1/swagger.json", "Client API V1");
    //    //c.RoutePrefix = ""; // Ensures Swagger loads at root (http://localhost:5120)
    //});
}

app.UseHttpsRedirection();

app.UseCors("Production");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
