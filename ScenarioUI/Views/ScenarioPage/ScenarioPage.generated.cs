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

namespace ScenarioUI.Views.ScenarioPage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 3 "..\..\Views\ScenarioPage\ScenarioPage.cshtml"
    using ScenarioUI.ViewGenerators;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\ScenarioPage\ScenarioPage.cshtml"
    using ScenarioUI.Views.Shared;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class ScenarioPage : WebViewGenerator
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");




WriteLiteral("\r\n");


            
            #line 6 "..\..\Views\ScenarioPage\ScenarioPage.cshtml"
  
    Layout = new Layout
    {
        HttpContext = HttpContext,
        Title = "💉 scenario page",
        SpecificScript = "scenariocreator"
    };


            
            #line default
            #line hidden
WriteLiteral("\r\n<script type=\"text/x-template\" id=\"assign-template\">\r\n    <span>\r\n        <vari" +
"able v-bind:variable=\"action.Variable\"\r\n                  v-bind:placeholder=\"\'e" +
"nter variable name\'\">\r\n        </variable>\r\n        <span>\r\n            =\r\n     " +
"   </span>\r\n        <span>\r\n            <input type=\"text\"\r\n                   p" +
"laceholder=\"enter value\"\r\n                   v-autowidth=\"{maxWidth: \'12em\', min" +
"Width: \'1em\', comfortZone: 0}\"\r\n                   v-model=\"action.Variable.Cons" +
"tantValue\"/>\r\n        </span>\r\n    </span>\r\n</script>\r\n\r\n<script type=\"text/x-te" +
"mplate\" id=\"execute-template\">\r\n    <span>\r\n        <variable v-bind:variable=\"a" +
"ction.Variable\"\r\n                  v-bind:placeholder=\"\'enter variable name\'\">\r\n" +
"        </variable>\r\n        <span>\r\n            =\r\n        </span>\r\n        <sp" +
"an>\r\n            <input type=\"text\"\r\n                   list=\"existingMethods\"\r\n" +
"                   placeholder=\"choose method name\"\r\n                   v-autowi" +
"dth=\"{maxWidth: \'20em\', minWidth: \'1em\', comfortZone: 30}\"\r\n                   v" +
"-model=\"selectedMethod\"\r\n                   v-on:focus=\"$root.recalculateExistin" +
"gMethods\" />\r\n        </span>\r\n        <span>\r\n            (\r\n        </span>\r\n " +
"       <template v-if=\"action.Method.Arguments.length > 0\">\r\n            <variab" +
"le v-for=\"(argument, index) in action.Method.Arguments\"\r\n                      v" +
"-bind:key=\"_.uniqueId()\"\r\n                      v-bind:variable=\"argument.Variab" +
"le\"\r\n                      v-bind:placeholder=\"argumentsPlaceholders[index]\"\r\n  " +
"                    v-bind:include-comma=\"index > 0\"\r\n                      v-bi" +
"nd:is-readonly=\"true\">\r\n\r\n            </variable>\r\n        </template>\r\n        " +
"<span>\r\n            )\r\n        </span>\r\n    </span>\r\n</script>\r\n\r\n<script type=\"" +
"text/x-template\" id=\"assert-template\">\r\n    <span>\r\n        <span>Assert.</span>" +
"\r\n        <span>\r\n            <select v-model=\"action.Assert.Type\">\r\n           " +
"     <option v-for=\"assertType in assertTypes\" v-bind:value=\"assertType.value\">\r" +
"\n                    {{ assertType.text }}\r\n                </option>\r\n         " +
"   </select>\r\n        </span>\r\n        <span v-if=\"action.Assert.Type >= 0\">\r\n  " +
"          <span>\r\n                (\r\n            </span>\r\n            <variable " +
"v-bind:variable=\"action.Assert.ValueVariable\"\r\n                      v-bind:plac" +
"eholder=\"\'actual value\'\"\r\n                      v-bind:is-readonly=\"true\">\r\n    " +
"        </variable>\r\n            <variable v-bind:variable=\"action.Assert.Expect" +
"edVariable\"\r\n                      v-bind:placeholder=\"\'expected value\'\"\r\n      " +
"                v-bind:include-comma=\"true\"\r\n                      v-bind:is-rea" +
"donly=\"true\">\r\n            </variable>\r\n            <span>\r\n                )\r\n " +
"           </span>\r\n        </span>\r\n    </span>\r\n</script>\r\n\r\n<script type=\"tex" +
"t/x-template\" id=\"variable-template\">\r\n    <span v-if=\"variable\">\r\n        <span" +
" v-if=\"includeComma\">, </span>\r\n        <input type=\"text\"\r\n               list=" +
"\"existingVariables\"\r\n               v-autowidth=\"{maxWidth: \'12em\', minWidth: \'1" +
"em\', comfortZone: 30}\"\r\n               v-model=\"variableValue\"\r\n               v" +
"-on:focus=\"$root.recalculateExistingVariables\"\r\n               v-bind:placeholde" +
"r=\"placeholder\" />\r\n        <span v-if=\"availableProperties.length > 0\">\r\n      " +
"      <span>.</span>\r\n            <select v-model=\"propertyValue\">\r\n            " +
"    <option v-for=\"property in availableProperties\">\r\n                    {{ pro" +
"perty }}\r\n                </option>\r\n            </select>\r\n        </span>\r\n   " +
" </span>\r\n</script>\r\n\r\n<div id=\"scenario-creator\" class=\"scenario-creator\">\r\n   " +
" <div class=\"top-name\">\r\n        <span>Scenario name:</span>\r\n        <input v-m" +
"odel=\"scenarioPage.Name\" />\r\n    </div>\r\n    <div>\r\n        <button type=\"button" +
"\" class=\"btn btn-primary btn-sm\" v-on:click=\"createNewAction(actionTypes.assign)" +
"\">\r\n            Assign\r\n        </button>\r\n        <button type=\"button\" class=\"" +
"btn btn-primary btn-sm\" v-on:click=\"createNewAction(actionTypes.execute)\">\r\n    " +
"        Execute\r\n        </button>\r\n        <button type=\"button\" class=\"btn btn" +
"-primary btn-sm\" v-on:click=\"createNewAction(actionTypes.assert)\">\r\n            " +
"Assert\r\n        </button>\r\n    </div>\r\n    <div class=\"edit-area\">\r\n        <tem" +
"plate v-for=\"(action, index) in orderedActions\">\r\n            <div class=\"edit-a" +
"ction\"\r\n                 v-on:click=\"focusAction(index)\"\r\n                 v-bin" +
"d:class=\"{ focused: focusedActionIndex === index }\">\r\n                <span clas" +
"s=\"edit-operations text-primary\">\r\n                    <span v-on:click=\"upActio" +
"n(index)\"><i class=\"far fa-arrow-alt-circle-up\"></i></span>\r\n                   " +
" <span v-on:click=\"downAction(index)\"><i class=\"far fa-arrow-alt-circle-down\"></" +
"i></span>\r\n                    <span v-on:click=\"removeAction(index)\"><i class=\"" +
"far fa-times-circle\"></i></span>\r\n                </span>\r\n                <span" +
">{{action.Order + 1}}.</span>\r\n                <assign v-if=\"action.Type == acti" +
"onTypes.assign\"\r\n                        v-bind:action=\"action\">\r\n              " +
"  </assign>\r\n                <execute v-else-if=\"action.Type == actionTypes.exec" +
"ute\"\r\n                         v-bind:action=\"action\">\r\n                </execut" +
"e>\r\n                <assert v-else-if=\"action.Type == actionTypes.assert\"\r\n     " +
"                   v-bind:action=\"action\">\r\n                </assert>\r\n         " +
"   </div>\r\n        </template>\r\n    </div>\r\n    <div class=\"save\">\r\n        <but" +
"ton type=\"button\" class=\"btn btn-primary btn-sm\" v-on:click=\"saveScenario\">\r\n   " +
"         Save\r\n        </button>\r\n    </div>\r\n\r\n    <datalist id=\"existingVariab" +
"les\">\r\n        <option v-for=\"variable in existingVariables\">{{variable.Name}}</" +
"option>\r\n    </datalist>\r\n\r\n    <datalist id=\"existingMethods\">\r\n        <option" +
" v-for=\"methodName in Object.keys(existingMethods)\">{{methodName}}</option>\r\n   " +
" </datalist>\r\n</div>\r\n\r\n<script>\r\n    var scenarioPage = initScenarioPage(");


            
            #line 172 "..\..\Views\ScenarioPage\ScenarioPage.cshtml"
                                   Write(ReflectedCollectionJson);

            
            #line default
            #line hidden
WriteLiteral(", ");


            
            #line 172 "..\..\Views\ScenarioPage\ScenarioPage.cshtml"
                                                             Write(ScenarioPageJson);

            
            #line default
            #line hidden
WriteLiteral(");\r\n</script>\r\n");


        }
    }
}
#pragma warning restore 1591