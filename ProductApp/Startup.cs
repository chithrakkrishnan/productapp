using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperMarket.API.Controllers.Configuration;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Persistence.Contexts;
using SuperMarket.API.Persistence.Repositories;
using SuperMarket.API.Services;


namespace SuperMarket.API
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
            services.AddMemoryCache();
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            //The ApiBehaviorOptions will work on controllers only with ApiController attribute decorated
            //services.Configure<ApiBehaviorOptions>(o =>
            //{
            //    o.InvalidModelStateResponseFactory = actionContext =>
            //        new BadRequestObjectResult(actionContext.ModelState);
            //});
            //services.AddDbContext<AppDbContext>(options => {
            //    options.UseInMemoryDatabase("supermarket-api-in-memory");
            //});

            services.AddCors();
            services.AddDbContext<AppDbContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
app.UseCors(x =>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
