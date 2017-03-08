using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StickerApp.Misc
{
    public class JsonResponseSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Consumes = new string[] {"application/json"};
            operation.Produces = new string[] {"application/json"};
        }
    }
}