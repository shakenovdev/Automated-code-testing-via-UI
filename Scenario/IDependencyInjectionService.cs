using System;

namespace Scenario.Core
{
    public interface IDependencyInjectionService
    {
        object CreateInstance(Type type);
    }
}