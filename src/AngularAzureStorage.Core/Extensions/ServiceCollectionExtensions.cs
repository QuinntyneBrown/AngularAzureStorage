﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace AngularAzureStorage.Core.Extensions
{
    public static class CorsDefaults
    {
        public static readonly string Policy = "CorsPolicy";
    }

    public static class ServiceCollectionExtensions
    {        
        public static IServiceCollection AddCustomSignalR(this IServiceCollection services)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new SignalRContractResolver()
            };

            var serializer = JsonSerializer.Create(settings);

            services.Add(new ServiceDescriptor(typeof(JsonSerializer),
                                               provider => serializer,
                                               ServiceLifetime.Transient));
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Angular Azure Storage",
                    Version = "v1",
                    Description = "Angular Azure Storage REST API",
                });
                options.CustomSchemaIds(x => x.FullName);
            });

            services.ConfigureSwaggerGen();

            return services;
        }
        
    }
}
