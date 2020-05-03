using BL.ExecutorActions.Interfaces;

namespace BL.ExecutorActions
{
    internal class MethodAction : IMethodAction
    {
        private readonly IVariableAction _variableAction;

        public MethodAction(IVariableAction variableAction)
        {
            _variableAction = variableAction;
        }

        public void Run(int methodId)
        {

        }

        private void RunStatic()
        {

        }

        private void RunConstructor()
        {

        }
    }
}