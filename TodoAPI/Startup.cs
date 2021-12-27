using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TodoAPI.Data;
using TodoAPI.Repositories;
using TodoAPI.Services;

namespace TodoAPI
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

            services.AddControllers()
                .AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoAPI", Version = "v1" });
            });

            //add db context
            var server = Configuration["DatabaseServer"] ?? "DESKTOP-8B8P6T2";
            var port = Configuration["DatabasePort"] ?? "1433";
            var database = Configuration["DatabaseName"] ?? "TodoDB";
            var user = Configuration["DBUser"] ?? "jasmin";
            var password = Configuration["DbPassword"] ?? "Jasmin123";
            var connectionString = $"Server={server},{port};Initial Catalog={database};User={user};Password={password};";
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //add repositories
            services.AddScoped<ITodoCategoryRepo, TodoCategoryRepo>();
            services.AddScoped<ITodoRepo, TodoRepo>();
            services.AddScoped<IStatusRepo, StatusRepo>();

            //add services
            services.AddScoped<ITodoCategoryService, TodoCategoryService>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IStatusService, StatusService>();

            //add newtonsoft.json

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //add automapper

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI v1"));
            }else{
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
