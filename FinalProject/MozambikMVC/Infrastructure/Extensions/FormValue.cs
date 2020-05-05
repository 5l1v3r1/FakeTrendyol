using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Infrastructure.Extensions
{

    public static class FormValue
    {
        /// <summary>
        /// Get HTMl input values by name
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string[] GetFormValueByName(this HttpContext httpContext, string name)
        {
            try
            {
                var array = httpContext.Request.Form.ToList().Where(x => x.Key == name)
                .Select(x => x.Value).ToList()[0].ToString().Split(",");
                return array;
            }
            catch (Exception)
            {
                return new string[0];
            }

        }
        public static IFormFile GetFormFileByName(this HttpContext httpContext, string name)
        {
            try
            {
                var array = httpContext.Request.Form.Files.GetFile(name);
                return array;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
