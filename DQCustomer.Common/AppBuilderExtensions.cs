using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DQCustomer.Common
{
    public static class AppBuilderExtensions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var consul = configuration.GetSection("Consul");
                string ip = consul["IP"];
                string port = consul["Port"];
                consulConfig.Address = new Uri($"http://{ip}:{port}");
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var service = configuration.GetSection("Service");
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                Interval = TimeSpan.FromSeconds(10),
                HTTP = $"http://{service["IP"]}:{service["Port"]}/api/health",
                Timeout = TimeSpan.FromSeconds(5)
            };

            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = $"{service["Name"]} - {service["Port"]}",
                Name = service["Name"],
                Address = $"{service["IP"]}",
                Port = Convert.ToInt32(service["Port"]),
                Tags = new[] { $"urlprefix-/{service["Name"]}" }
            };

            logger.LogInformation("Registering with Consul");
            //consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    logger.LogInformation("Unregistering from Consul");
            //    consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            //});

            return app;
        }
    }
}
