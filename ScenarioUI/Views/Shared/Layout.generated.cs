﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScenarioUI.Views.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 2 "..\..\Views\Shared\Layout.cshtml"
    using ScenarioUI.ViewGenerators;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class Layout : WebViewGenerator
    {
#line hidden

        #line 5 "..\..\Views\Shared\Layout.cshtml"

public string Title { get; set; }

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");




WriteLiteral("\r\n\r\n<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n");


            
            #line 13 "..\..\Views\Shared\Layout.cshtml"
     if (!string.IsNullOrEmpty(Title))
    {

            
            #line default
            #line hidden
WriteLiteral("        <title>");


            
            #line 15 "..\..\Views\Shared\Layout.cshtml"
          Write(Title);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n");


            
            #line 16 "..\..\Views\Shared\Layout.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    <link href=\"");


            
            #line 17 "..\..\Views\Shared\Layout.cshtml"
           Write(Root);

            
            #line default
            #line hidden
WriteLiteral("/css\" rel=\"stylesheet\">\r\n    <script src=\"");


            
            #line 18 "..\..\Views\Shared\Layout.cshtml"
            Write(Root);

            
            #line default
            #line hidden
WriteLiteral("/javascript\"></script>\r\n</head>\r\n\r\n<body>\r\n    ");


            
            #line 22 "..\..\Views\Shared\Layout.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n</body>\r\n\r\n</html>");


        }
    }
}
#pragma warning restore 1591