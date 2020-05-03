using System;
using Microsoft.AspNetCore.Http;

namespace ScenarioUI.ViewGenerators
{
    internal class WebViewGenerator : RazorViewGenerator
    {
        public  HttpContext HttpContext { get; set; }
        public HttpResponse Response { get { return HttpContext.Response; } }
        public HttpRequest Request { get { return HttpContext.Request; } }
        
        public string Root
        {
            get => Request.PathBase + "/scenario";
        }
        
        public IHtmlString Html(string html)
        {
            return new HtmlString(html);
        }

        public string AttributeEncode(string text)
        {
            return string.IsNullOrEmpty(text)
                 ? string.Empty
                 : System.Net.WebUtility.HtmlEncode(text);
        }

        public string Encode(string text)
        {
            return string.IsNullOrEmpty(text)
                 ? string.Empty
                 : HtmlHelper.Encode(text).ToHtmlString();
        }

        public override void Write(object value)
        {
            if (value == null)
                return;
            base.Write(HtmlHelper.Encode(value).ToHtmlString());
        }

        public override object RenderBody()
        {
            return new HtmlString(base.RenderBody().ToString());
        }

        public override string TransformText()
        {
            if (HttpContext == null)
                throw new InvalidOperationException("The _httpContext property has not been initialzed with an instance.");
            return base.TransformText();
        }
    }
}
