using DAL.Models;

namespace BL.ExecutorActions.Interfaces
{
    internal interface IMethodAction
    {
        object Run(Method method);
    }
}