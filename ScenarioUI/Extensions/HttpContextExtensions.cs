using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ScenarioUI.Extensions
{
    internal static class HttpContextExtensions
    {
        public static T GetRequestBody<T>(this HttpContext httpContext)
        {
            using (var stream = new StreamReader(httpContext.Request.Body))
            {
                var requestBody = stream.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(requestBody);
            }
        }

        public static async Task WriteJsonResponseAsync(this HttpContext httpContext, object response)
        {
            var httpResponse = httpContext.Response;
            httpResponse.StatusCode = 200;
            httpResponse.ContentType = "application/json";
            var jsonResponse = JsonConvert.SerializeObject(response);
            await httpResponse.WriteAsync(jsonResponse);
        }
    }
}