using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TasksManagementApp.Infrastructure;
using TasksManagementApp.Infrastructure.EmailService;
using TasksManagementApp.Utils;

namespace TasksManagementApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddControllers();
            services.AddDbContext<TasksManagementContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.ASCII.GetBytes(Configuration.GetSection("SecurityConfiguration:Secret").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddScoped<UnitOfWork>();            
            services.AddScoped<TokenGenerator>();
            services.AddScoped<EmailSender>();
            services.Configure<SecurityConfiguration>(Configuration.GetSection(nameof(SecurityConfiguration)));
            services.Configure<SmtpConfiguration>(Configuration.GetSection(nameof(SmtpConfiguration)));

            var a = Configuration.GetSection(nameof(SmtpConfiguration)).Value;
            var e = Configuration.GetSection(nameof(SecurityConfiguration));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TasksManagementContext>();
                context.ResetDataBase();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
