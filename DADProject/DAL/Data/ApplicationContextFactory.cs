using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace DAL.Data;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = GetDbContextOptionsBuilder();

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private DbContextOptionsBuilder<ApplicationDbContext> GetDbContextOptionsBuilder()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory());
        const string fileName = "appsettings.json";

        builder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));

        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString);

        return optionsBuilder;
    }
}
