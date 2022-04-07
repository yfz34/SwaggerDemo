using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerDemo.Swagger
{
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "產品相關" }
            };
        }
    }
}