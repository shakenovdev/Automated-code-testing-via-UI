using BL.ExecutorActions.Interfaces;
using BL.Services.Interfaces;
using BL.ViewModels;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BL.ViewModels.Internals;

namespace BL.Services
{
    public class ScenarioExecutorService : IScenarioExecutorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IScenarioRepository _scenarioRepository;
        private static readonly IDictionary<int, ExecutionResultViewModel> _executionResults = new Dictionary<int, ExecutionResultViewModel>();

        public ScenarioExecutorService(IServiceProvider serviceProvider,
            IScenarioRepository scenarioRepository)
        {
            _serviceProvider = serviceProvider;
            _scenarioRepository = scenarioRepository;
        }

        public void RunSingle(int scenarioId)
        {
            Task.Run(() => Execute(scenarioId));
        }

        public List<int> RunAll()
        {
            var scenarioIds = _scenarioRepository.Query()
                .Where(x => !x.IsDeleted)
                .Select(x => x.Id)
                .ToList();

            Task.Run(() => scenarioIds.ForEach(Execute));

            return scenarioIds;
        }

        public ExecutionResultViewModel GetExecutionResult(int scenarioId)
        {
            _executionResults.TryGetValue(scenarioId, out var executionResult);
            return executionResult;
        }

        public IEnumerable<ExecutionResultViewModel> GetAllExecutionResults(ICollection<int> scenarioIds)
        {
            return scenarioIds == null 
                ? _executionResults.Select(x => x.Value) 
                : _executionResults.Where(x => scenarioIds.Contains(x.Key)).Select(x => x.Value);
        }

        public void FillExecutionResults(ScenarioListViewModel scenarioList)
        {
            void FillFolderScenarios(FolderViewModel folder)
            {
                folder.Scenarios.ForEach(FillExecutionResult);
                folder.Folders.ForEach(FillFolderScenarios);
            }

            scenarioList.Folders.ForEach(FillFolderScenarios);
            scenarioList.RootScenarios.ForEach(FillExecutionResult);
        }

        private static void FillExecutionResult(ScenarioViewModel scenario)
        {
            if (_executionResults.TryGetValue(scenario.Id, out var executionResult))
            {
                scenario.FillExecutionResults(executionResult);
            }
        }

        private IExecutor GetExecutorInstance()
        {
            return (IExecutor) _serviceProvider.GetService(typeof(IExecutor));
        }

        private void Execute(int scenarioId)
        {
            var scenario = _scenarioRepository.GetEntirely(scenarioId);
            var executor = GetExecutorInstance();
            ExecutionResultViewModel executionResult;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                executor.Execute(scenario);

                executionResult = new ExecutionResultViewModel
                {
                    ScenarioId = scenarioId,
                    IsSuccess = true,
                    ExecutionTime = GetEllapsedTime(stopwatch)
                };
            }
            catch (Exception exception)
            {
                executionResult = new ExecutionResultViewModel
                {
                    ScenarioId = scenarioId,
                    IsSuccess = false,
                    ExecutionTime = GetEllapsedTime(stopwatch),
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                };
            }

            if (_executionResults.ContainsKey(scenarioId))
                _executionResults[scenarioId] = executionResult;
            else
                _executionResults.Add(scenarioId, executionResult);
        }

        private static string GetEllapsedTime(Stopwatch stopwatch)
        {
            var elapsedTime = stopwatch.Elapsed;

            if (elapsedTime.TotalSeconds < 1)
                return "00:01";

            return elapsedTime.ToString(@"mm\:ss");
        }
    }
}