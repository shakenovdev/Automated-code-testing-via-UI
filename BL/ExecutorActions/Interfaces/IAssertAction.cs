using DAL.Models;

namespace BL.ExecutorActions.Interfaces
{
    internal interface IAssertAction
    {
        void Check(Assert assert);
    }
}