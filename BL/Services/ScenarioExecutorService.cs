using System;
using System.Collections.Generic;
using System.Linq;
using BL.ExecutorActions.Interfaces;
using BL.Mappers;
using BL.Services.Interfaces;
using BL.ViewModels;
using DAL.Repositories.Interfaces;

namespace BL.Services
{
    public class ScenarioExecutorService : IScenarioExecutorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IScenarioRepository _scenarioRepository;

        public ScenarioExecutorService(IServiceProvider serviceProvider,
            IScenarioRepository scenarioRepository)
        {
            _serviceProvider = serviceProvider;
            _scenarioRepository = scenarioRepository;
        }

        public ExecutionResultViewModel RunSingle(int scenarioId)
        {
            return Execute(scenarioId);
        }

        public IEnumerable<ExecutionResultViewModel> RunAll()
        {
            var scenarioIds = _scenarioRepository.Query()
                .Where(x => !x.IsDeleted)
                .Select(x => x.Id)
                .ToList();

            return scenarioIds.Select(Execute);
        }

        private IExecutor GetExecutorInstance()
        {
            return (IExecutor) _serviceProvider.GetService(typeof(IExecutor));
        }

        private ExecutionResultViewModel Execute(int scenarioId)
        {
            var executor = GetExecutorInstance();

            try
            {
                var executionResult = executor.Execute(scenarioId).ToExecutionResultViewModel();
                return executionResult;
            }
            catch (Exception exception)
            {
                return new ExecutionResultViewModel
                {
                    IsSuccess = false,
                    StackTrace = exception.StackTrace
                };
            }
        }
    }
}