using System.Text;
using dotnet_rpg.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using dotnet_rpg.Api.Middleware;
using dotnet_rpg.Api.Service;
using dotnet_rpg.Data;
using dotnet_rpg.Service.Core;
using dotnet_rpg.Service.Core.Auth;
using dotnet_rpg.Service.Core.Character;
using dotnet_rpg.Service.Core.Weapon;

namespace dotnet_rpg.Api
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
            services.AddDbContext<DataContext>(options => 
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => 
                    {
                        npgsqlOptions.MigrationsAssembly("Data");
                    });
            });

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                    {
                        options.TokenValidationParameters = new TokenValidationParameters {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IServiceContext, ServiceContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IWeaponService, WeaponService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(EgressHandler));

            //app.UseHttpsRedirection();

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
