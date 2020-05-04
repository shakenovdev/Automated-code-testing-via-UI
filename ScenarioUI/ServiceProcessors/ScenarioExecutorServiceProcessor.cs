using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using ScenarioUI.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioUI.ServiceProcessors
{
    internal class ScenarioExecutorServiceProcessor : ServiceProcessor
    {
        internal const string ProcessorName = "executor";
        private readonly IScenarioExecutorService _service;

        public ScenarioExecutorServiceProcessor(IServiceProvider serviceProvider)
        {
            _service = (IScenarioExecutorService)serviceProvider.GetService(typeof(IScenarioExecutorService));
        }

        protected override async Task ProcessGetMethod(HttpContext httpContext, string actionName)
        {
            switch (actionName)
            {
                case "getExecutionResult":
                    await GetExecutionResultAction(httpContext);
                    break;
                case "getAllExecutionResults":
                    await GetAllExecutionResultsAction(httpContext);
                    break;
                default:
                    throw RouteException(httpContext);
            }
        }

        protected override async Task ProcessPostMethod(HttpContext httpContext, string actionName)
        {
            switch (actionName)
            {
                case "runSingle":
                    RunSingleAction(httpContext);
                    break;
                case "runAll":
                    await RunAllAction(httpContext);
                    break;
                default:
                    throw RouteException(httpContext);
            }
        }

        private async Task GetExecutionResultAction(HttpContext httpContext)
        {
            var scenarioId = Convert.ToInt32(httpContext.Request.Query["scenarioId"]);
            var executionResult = _service.GetExecutionResult(scenarioId);
            await httpContext.WriteJsonResponseAsync(executionResult);
        }

        private async Task GetAllExecutionResultsAction(HttpContext httpContext)
        {
            var scenarioIds = httpContext.GetRequestBody<List<int>>();
            var allExecutionResults = _service.GetAllExecutionResults(scenarioIds);
            await httpContext.WriteJsonResponseAsync(allExecutionResults);
        }

        private void RunSingleAction(HttpContext httpContext)
        {
            var scenarioId = Convert.ToInt32(httpContext.Request.Query["scenarioId"]);
            _service.RunSingle(scenarioId);
            httpContext.Response.StatusCode = 200;
        }

        private async Task RunAllAction(HttpContext httpContext)
        {
            var scenarioIds = _service.RunAll();
            await httpContext.WriteJsonResponseAsync(scenarioIds);
        }
    }
}