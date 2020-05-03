using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BL.ExportedMembers;
using Newtonsoft.Json;
using Scenario.Annotations;

[assembly: InternalsVisibleTo("Tests")]
namespace BL
{
    public static class ReflectedModel
    {
        internal static ScenarioType[] ScenarioTypes;

        public static string GetJSON()
        {
            return JsonConvert.SerializeObject(ScenarioTypes);
        }

        public static void Build(Assembly assembly)
        {
            var exportedTypes = GetAllAssemblyExportedTypes(assembly);

            ScenarioTypes = exportedTypes
                .Select(ConstructScenarioType)
                .ToArray();
            
            //var json = em.ReflectedModel.GetJSON();
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
    }
}
