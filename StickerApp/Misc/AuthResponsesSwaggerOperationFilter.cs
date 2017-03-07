using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StickerApp.Services;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StickerApp.Misc
{
    public class AuthResponsesSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var authAttributes = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<CheckTokenAttribute>();

            if (!authAttributes.Any()) return;

            operation.Responses.Add("401", new Response { Description = "Unauthorized" });

            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "token",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}