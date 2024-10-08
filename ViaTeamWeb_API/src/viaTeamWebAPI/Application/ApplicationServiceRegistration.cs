using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Services.ContactPages;
using Application.Services.HeroSectionWrites;
using Application.Services.TeamMemberPresentations;
using Application.Services.FeaturedSectionEntities;
using Application.Services.FeaturedArticleCards;
using Application.Services.Members;
using Application.Services.BusinessAreas;
using Application.Services.BusinessAreaMembers;
using Application.Services.SoftwareSkills;
using Application.Services.SoftwareSkillMembers;
using Application.Services.Teams;
using Application.Services.TeamAbouts;
using Application.Services.TeamMembers;
using Application.Services.ExternalLinks;



namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        
        services.AddScoped<IContactPagesService, ContactPagesManager>();
        services.AddScoped<IHeroSectionWritesService, HeroSectionWritesManager>();
        services.AddScoped<ITeamMemberPresentationsService, TeamMemberPresentationsManager>();
        services.AddScoped<IFeaturedSectionEntitiesService, FeaturedSectionEntitiesManager>();
        services.AddScoped<IFeaturedArticleCardsService, FeaturedArticleCardsManager>();
        services.AddScoped<IMembersService, MembersManager>();
        services.AddScoped<IBusinessAreasService, BusinessAreasManager>();
        services.AddScoped<IBusinessAreaMembersService, BusinessAreaMembersManager>();
        services.AddScoped<ISoftwareSkillsService, SoftwareSkillsManager>();
        services.AddScoped<ISoftwareSkillMembersService, SoftwareSkillMembersManager>();
        services.AddScoped<ITeamsService, TeamsManager>();
        services.AddScoped<ITeamAboutsService, TeamAboutsManager>();
        services.AddScoped<ITeamMembersService, TeamMembersManager>();
        services.AddScoped<IExternalLinksService, ExternalLinksManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
