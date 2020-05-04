using BL.Services.Interfaces;
using BL.ViewModels.Internals;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ScenarioUI.Extensions;
using ScenarioUI.Views.Home;
using System;
using System.Threading.Tasks;

namespace ScenarioUI.ServiceProcessors
{
    internal class ScenarioListServiceProcessor : ServiceProcessor
    {
        internal const string ProcessorName = "list";
        private readonly IScenarioListService _service;

        public ScenarioListServiceProcessor(IServiceProvider serviceProvider)
        {
            _service = (IScenarioListService) serviceProvider.GetService(typeof(IScenarioListService));
        }

        protected override async Task ProcessGetMethod(HttpContext httpContext, string actionName)
        {
            switch (actionName)
            {
                default:
                    await HomeView(httpContext);
                    break;
            }
        }

        protected override async Task ProcessPostMethod(HttpContext httpContext, string actionName)
        {
            switch (actionName)
            {
                case "deleteScenario":
                    DeleteScenarioAction(httpContext);
                    break;
                case "restoreScenario":
                    RestoreScenarioAction(httpContext);
                    break;
                case "moveScenarioToFolder":
                    MoveScenarioToFolderAction(httpContext);
                    break;
                case "createFolder":
                    await CreateFolderAction(httpContext);
                    break;
                case "renameFolder":
                    RenameFolderAction(httpContext);
                    break;
                case "deleteFolder":
                    await DeleteFolderAction(httpContext);
                    break;
                default:
                    throw RouteException(httpContext);
            }
        }

        private async Task HomeView(HttpContext httpContext)
        {
            var scenarioList = _service.GetAll();
            var scenarioListJson = JsonConvert.SerializeObject(scenarioList);
            var view = new Home(httpContext, scenarioListJson);
            await GenerateView(httpContext.Response, view);
        }

        private void DeleteScenarioAction(HttpContext httpContext)
        {
            var scenarioId = Convert.ToInt32(httpContext.Request.Query["scenarioId"]);
            _service.DeleteScenario(scenarioId);
            httpContext.Response.StatusCode = 200;
        }

        private void RestoreScenarioAction(HttpContext httpContext)
        {
            var scenarioId = Convert.ToInt32(httpContext.Request.Query["scenarioId"]);
            _service.RestoreScenario(scenarioId);
            httpContext.Response.StatusCode = 200;
        }

        private void MoveScenarioToFolderAction(HttpContext httpContext)
        {
            var scenarioId = Convert.ToInt32(httpContext.Request.Query["scenarioId"]);
            var folderId = Convert.ToInt32(httpContext.Request.Query["folderId"]);
            _service.MoveScenarioToFolder(scenarioId, folderId);
            httpContext.Response.StatusCode = 200;
        }

        private async Task CreateFolderAction(HttpContext httpContext)
        {
            var folder = httpContext.GetRequestBody<FolderViewModel>();
            var createdFolder = _service.CreateFolder(folder);
            await httpContext.WriteJsonResponseAsync(createdFolder);
        }

        private void RenameFolderAction(HttpContext httpContext)
        {
            var folder = httpContext.GetRequestBody<FolderViewModel>();
            _service.RenameFolder(folder);
            httpContext.Response.StatusCode = 200;
        }

        private async Task DeleteFolderAction(HttpContext httpContext)
        {
            var folderId = Convert.ToInt32(httpContext.Request.Query["folderId"]);
            var deletedScenarios = _service.DeleteFolder(folderId);
            await httpContext.WriteJsonResponseAsync(deletedScenarios);
        }
    }
}