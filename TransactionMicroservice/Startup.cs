using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TransactionMicroservice.DataAccess;
using TransactionMicroservice.Services;
using AccountMicroservice.Services;
using AccountMicroservice;

namespace TransactionMicroservice
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add other dependencies if needed

            services.AddControllers();

            // Configure database connection
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TransactionDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register services
            services.AddScoped<TransactionService>();

         
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}