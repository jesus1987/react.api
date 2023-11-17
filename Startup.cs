using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using react.api.DBContext;
using react.api.Repositories;
using System.IO;
using System.Reflection;

namespace react.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<ApiDdContext>(option => option.UseSqlServer(connectionString));
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(option => option.RegisterServicesFromAssemblies(assembly));
            services.AddCors();

            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Make sure you call this before calling app.UseMvc()
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
            });


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
