using BL.ExecutorActions.Interfaces;

namespace BL.ExecutorActions
{
    internal class AssertAction : IAssertAction
    {
        private readonly IVariableAction _variableAction;

        public AssertAction(IVariableAction variableAction)
        {
            _variableAction = variableAction;
        }

        public bool Check(int assertId)
        {
            throw new System.NotImplementedException();
        }

        private bool CheckEquality()
        {
            throw new System.NotImplementedException();
        }

        private bool CheckNonEquality()
        {
            throw new System.NotImplementedException();
        }

        private bool CheckIsTrue()
        {
            throw new System.NotImplementedException();
        }

        private bool CheckIsFalse()
        {
            throw new System.NotImplementedException();
        }
    }
}