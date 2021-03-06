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

namespace ScenarioUI.Views.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 3 "..\..\Views\Home\Home.cshtml"
    using ScenarioUI.ViewGenerators;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Home\Home.cshtml"
    using ScenarioUI.Views.Shared;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class Home : WebViewGenerator
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");




WriteLiteral("\r\n");


            
            #line 6 "..\..\Views\Home\Home.cshtml"
  
    Layout = new Layout
    {
        HttpContext = HttpContext,
        Title = "💉 Prepare for testing",
        SpecificScript = "scenariolist"
    };


            
            #line default
            #line hidden
WriteLiteral("\r\n<script type=\"text/x-template\" id=\"scenario-template\">\r\n    <div :class=\"{\'text" +
"-success\': (scenario.LastExecutedStatus == 1), \r\n                  \'text-danger\'" +
": (scenario.LastExecutedStatus == 2)}\">\r\n        <span v-on:click=\"execute\" v-sh" +
"ow=\"!isExecuting\"><i class=\"far fa-play-circle\"></i></span>\r\n        <span v-sho" +
"w=\"isExecuting\"><i class=\"fas fa-spinner fa-spin\"></i></span>\r\n        <span v-o" +
"n:dblclick=\"$root.openScenarioPage(scenario.Id)\">{{scenario.Name}}</span>\r\n     " +
"   <span class=\"text-primary\" v-on:click=\"$root.openScenarioPage(scenario.Id)\">\r" +
"\n            <i class=\"fas fa-edit\"></i>\r\n        </span>\r\n        <span class=\"" +
"badge\" \r\n              v-show=\"scenario.LastExecutedStatus\" \r\n              :cla" +
"ss=\"{\'badge-success\': (scenario.LastExecutedStatus == 1),\r\n                     " +
"  \'badge-danger\': (scenario.LastExecutedStatus == 2)}\">\r\n            {{scenario." +
"LastExecutionTime}}\r\n        </span>\r\n        <span class=\"badge badge-danger\" v" +
"-show=\"scenario.LastExecutedStatus == 2\">\r\n            stack trace\r\n        </sp" +
"an>\r\n        <span class=\"text-danger\" v-on:click=\"$emit(\'remove\')\">\r\n          " +
"  <i class=\"fas fa-trash-alt\"></i>\r\n        </span>\r\n    </div>\r\n</script>\r\n\r\n<s" +
"cript type=\"text/x-template\" id=\"folder-template\">\r\n    <div>\r\n        <span>\r\n " +
"           <span v-show=\"isOpen\" v-on:click=\"isOpen = false\"><i class=\"fas fa-fo" +
"lder-open\"></i></span>\r\n            <span v-show=\"!isOpen\" v-on:click=\"isOpen = " +
"true\"><i class=\"fas fa-folder\"></i></span>\r\n            <span v-on:dblclick=\"sta" +
"rtRenaming\" v-show=\"!isRenaming\">\r\n                {{folder.Name}}\r\n            " +
"</span>\r\n            <input v-autowidth=\"{maxWidth: \'15em\', minWidth: \'1em\', com" +
"fortZone: 0}\"\r\n                   v-model=\"folder.Name\"\r\n                   v-sh" +
"ow=\"isRenaming\"\r\n                   v-on:keyup.enter=\"rename\"\r\n                 " +
"  v-on:blur=\"rename\"\r\n                   ref=\"renameInput\" />\r\n            <span" +
" class=\"text-primary\" v-on:click=\"createNewFolder\">\r\n                <i class=\"f" +
"as fa-folder-plus\"></i>\r\n            </span>\r\n            <span class=\"text-dang" +
"er\" v-on:click=\"$emit(\'remove\')\">\r\n                <i class=\"fas fa-trash-alt\"><" +
"/i>\r\n            </span>\r\n        </span>\r\n        <div class=\"folder-content\" v" +
"-show=\"isOpen\">\r\n            <folder \r\n                v-for=\"(nestedFolder, ind" +
"ex) in folder.Folders\" \r\n                v-bind:key=\"nestedFolder.Id + \'-folder\'" +
"\" \r\n                v-bind:folder=\"nestedFolder\"\r\n                v-on:remove=\"$" +
"root.removeFolder(index, folder.Folders)\">\r\n            </folder>\r\n            <" +
"scenario \r\n                v-for=\"(scenario, index) in folder.Scenarios\" \r\n     " +
"           v-bind:key=\"scenario.Id + \'-scenario\'\" \r\n                v-bind:scena" +
"rio=\"scenario\"\r\n                v-on:remove=\"$root.removeScenario(index, folder." +
"Scenarios, true)\">\r\n            </scenario>\r\n        </div>\r\n    </div>\r\n</scrip" +
"t>\r\n\r\n<script type=\"text/x-template\" id=\"trash-template\">\r\n    <div>\r\n        <s" +
"pan v-on:click=\"isOpen = !isOpen\">\r\n            <span v-show=\"isOpen\"><i class=\"" +
"far fa-trash-alt\"></i></span>\r\n            <span v-show=\"!isOpen\"><i class=\"fas " +
"fa-trash-alt\"></i></span>\r\n            Trash\r\n        </span>\r\n        <div clas" +
"s=\"folder-content\" v-show=\"isOpen\">\r\n            <div v-for=\"scenario in scenari" +
"os\">\r\n                <span><i class=\"fas fa-minus-circle\"></i></span>\r\n        " +
"        <span>{{scenario.Name}}</span>\r\n                <span class=\"text-primar" +
"y\" v-on:click=\"restoreScenario(scenario)\">\r\n                    <i class=\"fas fa" +
"-trash-restore-alt\"></i>\r\n                </span>\r\n                <span class=\"" +
"text-danger\" v-on:click=\"removeScenario(scenario)\">\r\n                    <i clas" +
"s=\"fas fa-trash-alt\"></i>\r\n                </span>\r\n            </div>\r\n        " +
"</div>\r\n    </div>\r\n</script>\r\n\r\n<div id=\"scenario-list\" class=\"scenario-list\">\r" +
"\n    <div>\r\n        <button type=\"button\" class=\"btn btn-primary btn-sm\" v-on:cl" +
"ick=\"createNewFolder\">\r\n            <span><i class=\"fas fa-folder-plus\"></i></sp" +
"an>\r\n            New folder\r\n        </button>\r\n        <button type=\"button\" cl" +
"ass=\"btn btn-primary btn-sm\" v-on:click=\"openScenarioPage()\">\r\n            <span" +
"><i class=\"fas fa-plus-circle\"></i></span>\r\n            New scenario\r\n        </" +
"button>\r\n        <button type=\"button\" class=\"btn btn-primary btn-sm\" v-on:click" +
"=\"runAll\">\r\n            <span v-show=\"!isExecuting\"><i class=\"far fa-play-circle" +
"\"></i></span>\r\n            <span v-show=\"isExecuting\"><i class=\"fas fa-spinner f" +
"a-spin\"></i></span>\r\n            Run all scenarios\r\n        </button>\r\n    </div" +
">\r\n    <folder v-for=\"(folder, index) in scenarioList.Folders\"\r\n            v-bi" +
"nd:key=\"folder.Id + \'-folder\'\"\r\n            v-bind:folder=\"folder\"\r\n            " +
"v-on:remove=\"removeFolder(index, scenarioList.Folders)\">\r\n    </folder>\r\n    <sc" +
"enario v-for=\"(scenario, index) in scenarioList.RootScenarios\"\r\n              v-" +
"bind:key=\"scenario.Id\"\r\n              v-bind:scenario=\"scenario\"\r\n              " +
"v-on:remove=\"removeScenario(index, scenarioList.RootScenarios, true)\">\r\n    </sc" +
"enario>\r\n    <trash v-bind:scenarios=\"scenarioList.TrashScenarios\">\r\n    </trash" +
">\r\n</div>\r\n\r\n<script>\r\n    var scenarioList = initScenarioList(");


            
            #line 130 "..\..\Views\Home\Home.cshtml"
                                   Write(ScenarioListJson);

            
            #line default
            #line hidden
WriteLiteral(");\r\n</script>");


        }
    }
}
#pragma warning restore 1591
