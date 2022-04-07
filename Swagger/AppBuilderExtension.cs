using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SwaggerDemo.Swagger
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseSwaggerCore(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemo v1");
                options.RoutePrefix = string.Empty;
                options.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}