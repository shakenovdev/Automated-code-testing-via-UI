using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioUI.ViewGenerators
{
    internal static class ResourceHelper
    {
        private static readonly byte[] _newLine = Encoding.ASCII.GetBytes(Environment.NewLine);

        public static async Task LoadStyleSheets(HttpContext httpContext, IEnumerable<string> sources)
        {
            await LoadResourcesAsync(httpContext, sources, UIConstants.CSSMediaType);
        }

        public static async Task LoadJavaScripts(HttpContext httpContext, IEnumerable<string> sources)
        {
            await LoadResourcesAsync(httpContext, sources, UIConstants.JavaScriptMediaType);
        }

        private static async Task LoadResourcesAsync(HttpContext httpContext, IEnumerable<string> sources, string contentType)
        {
            var response = httpContext.Response;
            response.ContentType = contentType;
            response.Headers.Append("Cache-Control", "public, max-age=31536000");

            foreach (var source in sources)
                await WriteResourceToStreamAsync(response.Body, source);
        }

        public static async Task WriteResourceToStreamAsync(Stream outputStream, string resourceName)
        {
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
            if (resourceName == null) throw new ArgumentNullException(nameof(resourceName));
            if (resourceName.Length == 0) throw new ArgumentException(null, nameof(resourceName));
            
            var assembly = Assembly.GetExecutingAssembly();

            using (var inputStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (inputStream == null)
                {
                    throw new Exception($@"Resource name {resourceName} not found in assembly {assembly}.");
                }

                var buffer = new byte[Math.Min(inputStream.Length, 4096)];
                var readLength = await inputStream.ReadAsync(buffer, 0, buffer.Length);
                while (readLength > 0)
                {
                    await outputStream.WriteAsync(buffer, 0, readLength);
                    readLength = await inputStream.ReadAsync(buffer, 0, buffer.Length);
                }
                await outputStream.WriteAsync(_newLine, 0, _newLine.Length);
            }
        }
    }
}
