using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SwaggerDemo.Swagger
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Swagger範例", 
                    Version = "v1",
                    Description = "swagger範例文件",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.EnableAnnotations();
                // options.DocumentFilter<TagDescriptionsDocumentFilter>();
            });

            return services;
        }
    }
}