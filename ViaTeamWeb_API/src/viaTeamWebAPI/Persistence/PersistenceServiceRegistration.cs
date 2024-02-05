using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("nArchitecture"));
        services.AddDbContext<BaseDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BaseDb")));

        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
        
        services.AddScoped<IContactPageRepository, ContactPageRepository>();
        services.AddScoped<IHeroSectionWriteRepository, HeroSectionWriteRepository>();
        services.AddScoped<ITeamMemberPresentationRepository, TeamMemberPresentationRepository>();
        services.AddScoped<IFeaturedSectionEntitieRepository, FeaturedSectionEntitieRepository>();
        services.AddScoped<IFeaturedArticleCardRepository, FeaturedArticleCardRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        return services;
    }
}
