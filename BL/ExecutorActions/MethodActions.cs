using System;
using System.Collections.Generic;
using System.Linq;
using BL.ExecutorActions.Interfaces;
using DAL.Models;

namespace BL.ExecutorActions
{
    internal class MethodAction : IMethodAction
    {
        private readonly IVariableAction _variableAction;
        private readonly IReflectedCollection _reflectedCollection;

        public MethodAction(IVariableAction variableAction,
            IReflectedCollection reflectedCollection)
        {
            _variableAction = variableAction;
            _reflectedCollection = reflectedCollection;
        }

        public object Run(Method method)
        {
            object methodResult;

            if (method.IsStatic)
            {
                methodResult = RunStaticMethod(method);
            }
            else if (method.IsConstructor)
            {
                methodResult = RunConstructor(method);
            }
            else
            {
                methodResult = RunMethod(method);
            }

            return methodResult;
        }

        private object RunMethod(Method method)
        {
            if (!method.VariableId.HasValue)
                throw new InvalidOperationException();

            var typeInstance = _variableAction.GetTypeInstance(method.Variable);
            var arguments = GetArguments(method.Arguments).ToArray();

            return typeInstance.InvokeMethod(method.Name, arguments);
        }

        private object RunStaticMethod(Method method)
        {
            var scenarioType = _reflectedCollection.GetByNamespace(method.TypeName);
            var staticMethod = scenarioType.Methods.First(x => x.FullName == method.Name);
            var arguments = GetArguments(method.Arguments).ToArray();
            return staticMethod.Invoke(null, arguments);
        }

        private object RunConstructor(Method method)
        {
            var scenarioType = _reflectedCollection.GetByNamespace(method.TypeName);
            var arguments = GetArguments(method.Arguments).ToArray();
            return scenarioType.ConstructObject(arguments);
        }

        private IEnumerable<object> GetArguments(List<Argument> arguments)
        {
            return arguments.Select(x => _variableAction.Get(x.Variable));
        }
    }
}