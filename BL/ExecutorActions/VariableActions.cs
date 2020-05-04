using System;
using BL.ExecutorActions.Interfaces;
using System.Collections.Generic;
using System.Data;
using DAL.Models;

namespace BL.ExecutorActions
{
    internal class VariableAction : IVariableAction
    {
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
        
        public object Get(Variable variable)
        {
            if (!string.IsNullOrEmpty(variable.PropertyName))
                return GetTypeInstance(variable).GetProperty(variable.PropertyName);

            if (variable.Name != null && _variables.ContainsKey(variable.Name))
                return _variables[variable.Name];

            if (!string.IsNullOrEmpty(variable.ConstantValue))
                return GetConstantValue(variable);

            throw new InvalidOperationException();
        }

        public TypeInstance GetTypeInstance(Variable variable)
        {
            var typeInstance = _variables[variable.Name] as TypeInstance;
            
            if (typeInstance == null)
                throw new NoNullAllowedException();

            return typeInstance;
        }

        public void SetConstant(Variable variable)
        {
            var constantValue = GetConstantValue(variable);
            SetVariable(variable, constantValue);
        }

        public void SetObject(Variable variable, object value)
        {
            SetVariable(variable, value);
        }

        private void SetVariable(Variable variable, object value)
        {
            if (string.IsNullOrEmpty(variable.PropertyName))
            {
                AddOrUpdateObject(variable.Name, value);
            }
            else
            {
                var parentVariable = GetTypeInstance(variable);
                parentVariable.SetProperty(variable.PropertyName, value);
            }
        }

        private void AddOrUpdateObject(string variableName, object variable)
        {
            if (_variables.ContainsKey(variableName))
                _variables[variableName] = variable;

            _variables.Add(variableName, variable);
        }

        private static object GetConstantValue(Variable variable)
        {
            switch (variable.Type)
            {
                case "Int32":
                    return Convert.ToInt32(variable.ConstantValue);
                case "Boolean":
                    return Convert.ToBoolean(variable.ConstantValue);
                default:
                    return variable.ConstantValue;
            }
        }
    }
}
