using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RetailSystem.Models;

namespace RetailSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority
            //    });

            services.AddAutoMapper();

            // Inject Repository Implementations
            services.AddScoped<IRepository<Item>, Repository<Item>>();
            services.AddScoped<IRepository<Business>, Repository<Business>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IRepository<Location>, Repository<Location>>();
            services.AddScoped<IRepository<Manufacturer>, Repository<Manufacturer>>();
            services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
            services.AddScoped<IRepository<ReportGroup>, Repository<ReportGroup>>();
            services.AddScoped<IRepository<Unit>, Repository<Unit>>();
            services.AddScoped<IRepository<SubCategory>, Repository<SubCategory>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Sale>, Repository<Sale>>();
            services.AddScoped<IRepository<Purchase>, Repository<Purchase>>();
            services.AddScoped<IRepository<Transfer>, Repository<Transfer>>();
            services.AddScoped<IRepository<Invoice>, Repository<Invoice>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IRepository<Item>, ItemRepository>();
            //services.AddScoped<IRepository<Business>, BusinessRepository>();
            //services.AddScoped<IRepository<Category>, CategoryRepository>();
            //services.AddScoped<IRepository<Customer>, CustomerRepository>();
            //services.AddScoped<IRepository<Location>, LocationRepository>();
            //services.AddScoped<IRepository<Manufacturer>, ManufacturerRepository>();
            //services.AddScoped<IRepository<Supplier>, SupplierRepository>();
            //services.AddScoped<IRepository<ReportGroup>, ReportGroupRepository>();
            //services.AddScoped<IRepository<Unit>, UnitRepository>();
            //services.AddScoped<IRepository<SubCategory>, SubCategoryRepository>();
            //services.AddScoped<IRepository<Order>, OrderRepository>();
            //services.AddScoped<IRepository<Sale>, SaleRepository>();
            //services.AddScoped<IRepository<Purchase>, PurchaseRepository>();
            //services.AddScoped<IRepository<Transfer>, TransferRepository>();
            //services.AddScoped<IRepository<Invoice>, InvoiceRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddSwaggerDocument(config =>
            //{
            //    config.PostProcess = document =>
            //    {
            //        document.Info.Title = "Retail System Api";
            //        document.Info.Description = "An Api for daily retail activities";
            //        document.Info.Contact = new NSwag.SwaggerContact
            //        {
            //            Name = "Sunrise Ezekikwu",
            //            Email = "ezesunrise@yahoo.com",
            //            Url = "https://linkedin.com/in/ezesunrise"
            //        };
            //    };
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            //app.UseSwagger();
            //app.UseSwaggerUi3();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "api_default",
                    template: "api/{controller}/{action}/{id?}");

                //routes.MapSpaFallbackRoute(
                //    name: "spa-fallback",
                //    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
