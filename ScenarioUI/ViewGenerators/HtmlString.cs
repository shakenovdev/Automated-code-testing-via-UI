using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioUI.ViewGenerators
{
    // http://msdn.microsoft.com/en-us/library/system.web.ihtmlstring.aspx

    interface IHtmlString
    {
        string ToHtmlString();
    }

    sealed class HtmlString : IHtmlString
    {
        readonly string _html;
        public HtmlString(string html) { _html = html ?? string.Empty; }
        public string ToHtmlString() { return _html; }
        public override string ToString() { return ToHtmlString(); }
    }

    static class HtmlHelper
    {
        public static readonly IHtmlString Empty = new HtmlString(string.Empty);

        public static IHtmlString Raw(string input)
        {
            return string.IsNullOrEmpty(input) ? Empty : new HtmlString(input);
        }

        public static IHtmlString Encode(object input)
        {
            IHtmlString html;
            return null != (html = input as IHtmlString)
                 ? html
                 : input == null
                 ? Empty
                 : Raw(System.Net.WebUtility.HtmlDecode(input.ToString()));
        }
    }
}
