using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BL.ExecutorActions.Interfaces;
using BL.ExportedMembers;
using Scenario.Annotations;

namespace BL.ExecutorActions
{
    internal class ReflectedCollection : IReflectedCollection
    {
        private ScenarioType[] _scenarioTypes;

        public ReflectedCollection()
        {
            Build(Assembly.GetEntryAssembly());
        }

        public IEnumerator<ScenarioType> GetEnumerator()
        {
            foreach (var scenarioType in _scenarioTypes)
            {
                yield return scenarioType;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _scenarioTypes.GetEnumerator();
        }

        public int Count => _scenarioTypes.Length;

        public ScenarioType GetByNamespace(string nameSpace)
        {
            return _scenarioTypes.FirstOrDefault(x => x.FullName == nameSpace);
        }

        #region privates

        private void Build(Assembly assembly)
        {
            var exportedTypes = GetAllAssemblyExportedTypes(assembly);

            _scenarioTypes = exportedTypes
                .Select(ConstructScenarioType)
                .ToArray();

            //var json = em.GetJSON();
            //Console.WriteLine(json);
        }

        private static ScenarioType ConstructScenarioType(ActivatedType activatedType)
        {
            var activatedMode = activatedType.ActivationMode;

            bool IsActivated(MemberInfo memberInfo)
            {
                switch (activatedMode)
                {
                    case ActivationMode.None:
                        return false;
                    case ActivationMode.Single:
                        return memberInfo.GetCustomAttribute<ScenarioDescriptionAttribute>() != null ||
                            memberInfo.GetCustomAttribute<ScenarioActivationAttribute>()?.ActivationMode == ActivationMode.Single;
                    case ActivationMode.All:
                        return memberInfo.GetCustomAttribute<ScenarioActivationAttribute>()?.ActivationMode != ActivationMode.Single;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(activatedMode));
                }
            }

            var typeInfo = activatedType.Type.GetTypeInfo();

            return new ScenarioType(typeInfo)
            {
                Properties = typeInfo.GetProperties()
                    .Where(IsActivated)
                    .Select(x => new Property(x))
                    .ToArray(),
                Constructors = typeInfo.GetConstructors()
                    .Where(IsActivated)
                    .Select(x => new Constructor(x))
                    .ToArray(),
                Fields = typeInfo.GetFields()
                    .Where(IsActivated)
                    .Select(x => new Field(x))
                    .ToArray(),
                Methods = typeInfo.GetMethods() // BindingFlags.DeclaredOnly?
                    .Where(x => IsActivated(x) && !x.IsSpecialName)
                    .Select(x => new Method(x))
                    .ToArray()
            };
        }

        private static IEnumerable<ActivatedType> GetAllAssemblyExportedTypes(Assembly assembly)
        {
            var allAssemblies = assembly.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Append(assembly)
                .ToList();

            foreach (var type in allAssemblies.SelectMany(x => x.ExportedTypes))
            {
                var activationAttribute = type.GetCustomAttribute<ScenarioActivationAttribute>();

                if (activationAttribute == null || activationAttribute.ActivationMode == ActivationMode.None)
                    continue;

                yield return new ActivatedType
                {
                    Type = type,
                    ActivationMode = activationAttribute.ActivationMode
                };
            }
        }

        private class ActivatedType
        {
            public Type Type;
            public ActivationMode ActivationMode;
        }

        #endregion
    }
}