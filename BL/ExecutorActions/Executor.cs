using BL.ExecutorActions.Interfaces;

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

        public ExecutionResult Execute(int scenarioId)
        {
            throw new System.NotImplementedException();
        }
    }
}