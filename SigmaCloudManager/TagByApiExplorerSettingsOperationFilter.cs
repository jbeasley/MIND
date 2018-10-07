using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind
{
    public class TagByApiExplorerSettingsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiExplorerAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
           .Union(context.MethodInfo.GetCustomAttributes(true))
           .OfType<ApiExplorerSettingsAttribute>();

            if (apiExplorerAttributes.Any())
            {
                var tags = apiExplorerAttributes
                            .Where(
                                x => 
                                !x.IgnoreApi)
                            .Select(
                                x => 
                                x.GroupName);

                operation.Tags = tags.ToList();
            }
        }
    }
}
