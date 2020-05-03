namespace BL.ExecutorActions.Interfaces
{
    internal interface IVariableAction
    {
        object Get(int variableId);
        void Set(int variableId);
    }
}