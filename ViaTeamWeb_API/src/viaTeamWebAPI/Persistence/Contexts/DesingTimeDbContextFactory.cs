using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    public BaseDbContext CreateDbContext(string[] args)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebAPI");
        configurationBuilder.SetBasePath(basePath);

        // appsettings.json dosyasının olduğu yolu ekleyin
        configurationBuilder.AddJsonFile("appsettings.json");

        IConfiguration configuration = configurationBuilder.Build();

        // appsettings.json içindeki "BaseDb" bağlantı dizesini alın
        string connection = configuration.GetConnectionString("BaseDb");

        // DbContextOptionsBuilder kullanarak bağlantıyı belirtin
        DbContextOptionsBuilder<BaseDbContext> dbContextOptionsBuilder =
            new DbContextOptionsBuilder<BaseDbContext>();
        dbContextOptionsBuilder.UseSqlServer(connection);

        // BaseDbContext örneğini oluşturun ve bağlantıyı geçirin
        return new BaseDbContext(dbContextOptionsBuilder.Options);
    }
}