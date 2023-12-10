using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGHServer.Application.Interfaces;

namespace SGHServer.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection service,
        IConfiguration configuration)
    {
        var dbConnectionString = configuration["SGHSERVER_DB:ConnectionString"];

        service.AddDbContext<DataStore>(options =>
        {
            options.UseNpgsql(dbConnectionString);
        });

        service.AddScoped<IDataStore>(provider =>
            provider.GetService<DataStore>());

        return service;
    }
}