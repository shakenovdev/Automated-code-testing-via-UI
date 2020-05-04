using BL.Services.Interfaces;
using BL.ViewModels;
using Microsoft.AspNetCore.Http;
using ScenarioUI.Extensions;
using ScenarioUI.Views.ScenarioPage;
using System;
using System.Threading.Tasks;
using BL.ExecutorActions.Interfaces;
using Newtonsoft.Json;

namespace ScenarioUI.ServiceProcessors
{
    internal class ScenarioCreatorServiceProcessor : ServiceProcessor
    {
        internal const string ProcessorName = "creator"; 
        private readonly IScenarioCreatorService _service;
        private readonly IReflectedCollection _reflectedCollection;

        public ScenarioCreatorServiceProcessor(IServiceProvider serviceProvider)
        {
            _service = (IScenarioCreatorService)serviceProvider.GetService(typeof(IScenarioCreatorService));
            _reflectedCollection = (IReflectedCollection)serviceProvider.GetService(typeof(IReflectedCollection));
        }

        protected override async Task ProcessGetMethod(HttpContext httpContext, string actionName)
        {
            await ScenarioView(httpContext);
        }

        protected override async Task ProcessPostMethod(HttpContext httpContext, string actionName)
        {
            CreateOrEditAction(httpContext);
        }

        private async Task ScenarioView(HttpContext httpContext)
        {
            var scenarioId = Convert.ToInt32(httpContext.Request.Query["scenarioId"]);
            var scenarioCreation = scenarioId == default(int) 
                ? new ScenarioCreationViewModel() 
                : _service.Get(scenarioId);

            var reflectedCollectionJson = _reflectedCollection.GetJSON();
            var scenarioPageJson = JsonConvert.SerializeObject(scenarioCreation);
            var view = new ScenarioPage(httpContext, reflectedCollectionJson, scenarioPageJson);
            await GenerateView(httpContext.Response, view);
        }

        public void CreateOrEditAction(HttpContext httpContext)
        {
            var scenarioCreation = httpContext.GetRequestBody<ScenarioCreationViewModel>();

            if (scenarioCreation.Id == default(int))
                _service.Create(scenarioCreation);
            else
                _service.Edit(scenarioCreation);

            httpContext.Response.StatusCode = 200;
        }
    }
}