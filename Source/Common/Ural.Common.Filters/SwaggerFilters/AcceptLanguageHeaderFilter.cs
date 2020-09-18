using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace Ural.Common.Filters.SwaggerFilters
{
    /// <summary>
    /// The AcceptLanguageHeaderFilter Class 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    public class AcceptLanguageHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">operation</exception>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name            = "Accept-Language",
                AllowEmptyValue = true,
                In              = ParameterLocation.Header,
                Required        = false,
                Description     = "en-GB"
            });
        }
    }
}
