﻿using System.Linq;
using Typewriter.CodeModel;

namespace Typewriter.Extensions.WebApi
{
    /// <summary>
    /// Extension methods for extracting Web API parameters.
    /// </summary>
    public static class ParameterExtensions
    {
        /// <summary>
        /// Creates an object literal containing the parameters that should be sent in the request body of a Web API request.
        /// If no parameters are required the literal "null" is returned.
        /// </summary>
        public static string PostData(this Method method)
        {
            return PostData(method, UrlExtensions.DefaultRoute);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string PostData(this Method method, string route)
        {
            var url = method.Url(route);
            var dataParameters = method.Parameters.Where(p => url.Contains($"${{{p.name}}}") == false).ToList();

            if (dataParameters.Any())
            {
                return $"{{ {string.Join(", ", dataParameters.Select(p => $"{p.name}: {p.name}"))} }}";
            }

            return "null";
        }
    }
}
