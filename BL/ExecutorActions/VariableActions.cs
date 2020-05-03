using BL.ExecutorActions.Interfaces;
using System.Collections.Generic;

namespace BL.ExecutorActions
{
    internal class VariableAction : IVariableAction
    {
        private readonly Dictionary<int, object> _variables = new Dictionary<int, object>();

        public VariableAction()
        {
        }
        
        public object Get(int variableId)
        {
            throw new System.NotImplementedException();
        }

        public void Set(int variableId)
        {
            throw new System.NotImplementedException();
        }

        private void SetConstant()
        {

        }

        private void SetConstructor()
        {

        }
    }
}
