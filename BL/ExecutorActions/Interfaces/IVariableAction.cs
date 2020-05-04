using DAL.Models;

namespace BL.ExecutorActions.Interfaces
{
    internal interface IVariableAction
    {
        object Get(Variable variable);
        TypeInstance GetTypeInstance(Variable variable);
        void SetConstant(Variable variable);
        void SetObject(Variable variable, object value);
    }
}