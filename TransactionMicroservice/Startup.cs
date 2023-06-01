using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddSwaggerGen();


            // Configure database connection
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

      

            services.AddDbContext<AccountDbContext>(options =>
                           options.UseSqlServer(connectionString));

            services.AddScoped<TransactionService>();
            services.AddScoped<AccountService>();


         
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}