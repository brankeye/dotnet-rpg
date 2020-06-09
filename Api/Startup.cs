using System;
using System.Linq;
using System.Text;
using dotnet_rpg.Api.Context;
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
using dotnet_rpg.Data;
using dotnet_rpg.Infrastructure.Repository.Factory;
using dotnet_rpg.Infrastructure.Repository.Persister;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Core.Character;
using dotnet_rpg.Service.Core.Character.Validator;
using dotnet_rpg.Service.Core.Skill;
using dotnet_rpg.Service.Core.Skill.Validator;
using dotnet_rpg.Service.Core.Weapon;
using dotnet_rpg.Service.Core.Weapon.Validator;
using dotnet_rpg.Service.Decorators;
using dotnet_rpg.Service.Operations.Auth;
using dotnet_rpg.Service.Utility.AuthUtility;
using SimpleInjector;

namespace dotnet_rpg.Api
{
    public class Startup
    {
        private readonly Container _container = new SimpleInjector.Container();
        
        public Startup(IConfiguration configuration)
        {
            _container.Options.ResolveUnregisteredConcreteTypes = false;
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
            
            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
            });

            InitializeContainer();
            InitializeDecorators();
        }
        
        private void InitializeContainer()
        {
            var serviceAssembly = typeof(IQueryHandler<,>).Assembly;
            
            _container.Register<IHttpContextAccessor, HttpContextAccessor>(Lifestyle.Singleton);

            _container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Scoped);
            _container.Register<IRepositoryPersister, RepositoryPersister>(Lifestyle.Scoped);
            _container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            _container.Register<IAuthUtility, AuthUtility>(Lifestyle.Scoped);

            _container.Register<ICharacterValidator, CharacterValidator>(Lifestyle.Scoped);
            _container.Register<IWeaponValidator, WeaponValidator>(Lifestyle.Scoped);
            _container.Register<ISkillValidator, SkillValidator>(Lifestyle.Scoped);
            
            _container.Register<IAuthContext, AuthContext>(Lifestyle.Scoped);
            _container.Register<ICharacterService, CharacterService>(Lifestyle.Scoped);
            _container.Register<IWeaponService, WeaponService>(Lifestyle.Scoped);
            _container.Register<ISkillService, SkillService>(Lifestyle.Scoped);

            _container.Register(typeof(IQueryHandler<,>), serviceAssembly, Lifestyle.Scoped);
            _container.Register(typeof(ICommandHandler<>), serviceAssembly, Lifestyle.Scoped);
            _container.Register(typeof(IValidator<>), serviceAssembly, Lifestyle.Scoped);
            _container.Register(typeof(IMapper<,>), serviceAssembly, Lifestyle.Scoped);
            
            _container.RegisterSingleton<IMediator>(() => 
                new Mediator(type => _container.GetInstance(type)));
        }

        public void InitializeDecorators()
        {
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>), 
                typeof(AuthorizedQueryHandlerDecorator<,>),
                c => DecorateByQueryType(c, typeof(IAuthorizedQuery<>)));
            
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>), 
                typeof(ValidationQueryHandlerDecorator<,>));
            
            _container.RegisterDecorator(
                typeof(ICommandHandler<>), 
                typeof(ValidationCommandHandlerDecorator<>));
            
            _container.RegisterDecorator(
                typeof(ICommandHandler<>), 
                typeof(TransactionCommandHandlerDecorator<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();
            
            app.UseMiddleware<EgressHandler>(_container);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static bool DecorateByQueryType(DecoratorPredicateContext context, Type type)
        {
            return context.ServiceType.GetGenericArguments()[0].GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == type);
        }
    }
}
