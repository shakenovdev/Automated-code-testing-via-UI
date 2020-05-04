using System;
using System.Linq;
using BL.ExecutorActions.Interfaces;
using DAL.Enums;
using Action = DAL.Models.Action;
using ScenarioEntity = DAL.Models.Scenario;

namespace BL.ExecutorActions
{
    internal class Executor : IExecutor
    {
        private readonly IVariableAction _variableAction;
        private readonly IMethodAction _methodAction;
        private readonly IAssertAction _assertAction;

        public Executor(IVariableAction variableAction,
            IMethodAction methodAction,
            IAssertAction assertAction)
        {
            _variableAction = variableAction;
            _methodAction = methodAction;
            _assertAction = assertAction;
        }

        public void Execute(ScenarioEntity scenario)
        {
            foreach (var scenarioAction in scenario.Actions.OrderBy(x => x.Order))
            {
                ExecuteAction(scenarioAction);
            }
        }

        private void ExecuteAction(Action action)
        {
            switch (action.Type)
            {
                case ActionType.SetVariable:
                    _variableAction.SetConstant(action.Variable);
                    break;
                case ActionType.RunMethod:
                    var result = _methodAction.Run(action.Method);

                    if (action.VariableId.HasValue && !string.IsNullOrEmpty(action.Variable.Name))
                        _variableAction.SetObject(action.Variable, result);

                    break;
                case ActionType.Assert:
                    _assertAction.Check(action.Assert);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}