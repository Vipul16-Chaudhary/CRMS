using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Common
{
    public class ApiSchemasVisibility : IDocumentFilter
    {
        //private readonly string[] VisibleSchemas = { "Schema1"}; // Open once more than one entity created and add here.

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //// remove controller as per requirement
            //foreach (ApiDescription apiDescription in context.ApiDescriptions)
            //{
            //    var actionDescriptor = (ControllerActionDescriptor)apiDescription.ActionDescriptor;
            //    if (actionDescriptor.ControllerName == "RCMS")
            //    {
            //        swaggerDoc.Paths.Remove($"/{apiDescription.RelativePath}");
            //    }
            //}

            // remove schemas
            foreach ((string key, _) in swaggerDoc.Components.Schemas)
            {
                if (key.Contains("StudentEntity") || key.Contains("OData"))
                {
                    swaggerDoc.Components.Schemas.Remove(key);
                }
            }
        }
        
    }
}
