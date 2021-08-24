namespace ConcreteProducts.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ConcreteProducts.Data;
    using ConcreteProducts.Web.Infrastructure;
    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Services.Shapes;
    using ConcreteProducts.Services.ProductColors;
    using ConcreteProducts.Services.WarehouseProducts;
    using ConcreteProducts.Services.Chats;
    using ConcreteProducts.Services.AutoMappingProfile;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ConcreteProductsDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ConcreteProductsDbContext>();

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });

            services
                .AddAutoMapper(options =>
                {
                    options.AddProfile<MappingProfile>();
                });

            services.AddMemoryCache();
            services.AddSignalR();

            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IShapeService, ShapeService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IProductColorsService, ProductColorsService>();
            services.AddTransient<IWarehouseProductService, WarehouseProductService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplyMigrations();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapChatHubRoute();
                    endpoints.MapAreaControllerRoute();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
