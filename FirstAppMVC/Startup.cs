using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.EntityConfiguration;
using FirstAppMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirstAppMVC
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
            string connectionString = @"Server = localhost\SQLEXPRESS; Database = MVCDB; Trusted_Connection = True;";
            var optionBuilder = new DbContextOptionsBuilder();
            optionBuilder.UseSqlServer(connectionString);

            services.AddScoped<IEntityConfigurationContainer>(sp => new EntityConfigurationContainer());

            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            }
            );

            services.AddSingleton<IApplicationDbContextFactory>(
                sp => new ApplicationDbContextFactory(optionBuilder.Options, new EntityConfigurationContainer()));

            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IBrandService, BrandService>();
            services.AddSingleton<IOrderService, OrderService>();

            Mapper.Initialize(c => c.AddProfile(new MappingProfile()));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
