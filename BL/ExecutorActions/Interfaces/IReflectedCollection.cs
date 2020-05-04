using System.Collections.Generic;
using BL.ExportedMembers;

namespace BL.ExecutorActions.Interfaces
{
    public interface IReflectedCollection : IReadOnlyCollection<ScenarioType>
    {
        ScenarioType GetByNamespace(string nameSpace);
        string GetJSON();
    }
}