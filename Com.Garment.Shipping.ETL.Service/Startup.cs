using System;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Com.Garment.Shipping.ETL.Service.Services;

[assembly: FunctionsStartup(typeof(Com.Garment.Shipping.ETL.Service.Startup))]
namespace Com.Garment.Shipping.ETL.Service
{
    public class Startup : FunctionsStartup
    {        
        private readonly string connectionStringOrigin = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionStringOrigin", EnvironmentVariableTarget.Process);
        private readonly string connectionStringDestination = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionStringDestination", EnvironmentVariableTarget.Process);

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddSingleton<ISqlDataContext<GShippingExportModel>>((s) =>
                {
                    return new SqlDataContext<GShippingExportModel>(connectionStringOrigin, connectionStringDestination);
                })
                .AddSingleton<ISqlDataContext<GShippingLocalModel>>((s) =>
                {
                    return new SqlDataContext<GShippingLocalModel>(connectionStringOrigin, connectionStringDestination);
                });
            
            builder.Services.AddTransient<IGShippingLocalAdapter, GShippingLocalAdapter>()
                .AddTransient<IGShippingExportAdapter, GShippingExportAdapter>()
                .AddTransient<IGShippingExportService, GShippingExportService>()
                .AddTransient<IGShippingLocalService, GShippingLocalService>();
        }
    }
}