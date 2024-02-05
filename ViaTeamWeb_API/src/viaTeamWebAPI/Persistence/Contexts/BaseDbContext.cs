using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public DbSet<BusinessArea> BusinessAreas { get; set; }
    public DbSet<ContactPage> ContactPages { get; set; }
    public DbSet<ExternalLink> ExternalLinks { get; set; }
    public DbSet<HeroSectionWrite> HeroSectionWrite { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<SoftwareSkill> SoftwareSkills { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamAbout> TeamAbouts { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamMemberPresentation> TeamMemberPresentations { get; set; }
    public DbSet<HeroSectionWrite> HeroSectionWrites { get; set; }
    public DbSet<FeaturedSectionEntitie> FeaturedSectionEntities { get; set; }
    public DbSet<FeaturedArticleCard> FeaturedArticleCards { get; set; }
    
    
    public BaseDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
