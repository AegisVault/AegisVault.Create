using System;
using System.IO;
using AegisVault.Create.Helpers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AegisVault.Startup))]

namespace AegisVault
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();


            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
        
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:CosmosDB");
            string connectionStringBackup = "AccountEndpoint=https://aegisvault-db.documents.azure.com:443/;AccountKey=wqeKYoWFGrjcSZ35Xs1xhfjhbaQFSZlIeurEbvHZo7jkQdEYmGHeKjMky3jhz6MlRCK2pk5Jq99jACDb6oPiFw==;";
            builder.Services.AddDbContext<AegisVaultContext>(options => options.UseCosmos(connectionString ?? connectionStringBackup, "AegisVault"));
        }
    }
}