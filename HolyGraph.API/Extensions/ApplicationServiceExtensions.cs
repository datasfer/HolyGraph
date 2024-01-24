using HolyGraph.API.Configs;
using HolyGraph.API.Data;
using HolyGraph.API.Interfaces;
using HolyGraph.API.Services;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace HolyGraph.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        var neo4jConfig = new Neo4jConfig();
        config.GetSection(nameof(Neo4jConfig)).Bind(neo4jConfig);
        services.AddSingleton(neo4jConfig);

        // services.AddSingleton(GraphDatabase.Driver(neo4jConfig.InstanceUri, AuthTokens.None));
        services.AddSingleton(GraphDatabase.Driver(neo4jConfig.InstanceUri, AuthTokens.Basic(neo4jConfig.UserName, neo4jConfig.Password)));

        services.AddSingleton<INeo4jGraphRepository, Neo4jGraphRepository>();
        services.AddSingleton<IBookRepository, BookRepository>();
        services.AddSingleton<ITranslationRepository, TranslationRepository>();
   
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnectionString"));
        });

        services.AddScoped<ITokenService, TokenService>();
        services.AddCors();

        return services;
    }
}