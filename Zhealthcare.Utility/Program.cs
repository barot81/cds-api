﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zhealthcare.Service.Configurations;

namespace Zhealthcare.Utility
{
    internal class Program
    {
        public async static Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddPatientModule(hostContext.Configuration);
                    services.AddHostedService<ZhcMigrationService>();
                })
                .Build();
            await host.StartAsync().ConfigureAwait(false);
            await host.StopAsync().ConfigureAwait(false);
        }
    }
}