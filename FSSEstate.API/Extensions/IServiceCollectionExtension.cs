using FSSEstate.Business.Implementations.Authorization;
using FSSEstate.Business.Implementations.Helpers;
using FSSEstate.Business.Implementations;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Business.Interfaces.Helpers;
using FSSEstate.Business.Interfaces;
using FSSEstate.Repository.Implementations.Repositories;
using FSSEstate.Repository.Implementations;
using FSSEstate.Repository.Interfaces.Repositories;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IJwtUtils, JwtUtils>()
                .AddScoped<ISmsHelper, SmsHelper>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IFileService, FileService>();
        }
    }
}
