using System;
using BL.ExecutorActions.Interfaces;
using DAL.Enums;
using DAL.Models;

namespace BL.ExecutorActions
{
    internal class AssertAction : IAssertAction
    {
        private readonly IVariableAction _variableAction;

        public AssertAction(IVariableAction variableAction)
        {
            _variableAction = variableAction;
        }

        public void Check(Assert assert)
        {
            bool checkResult;

            switch (assert.Type)
            {
                case AssertType.AreEqual:
                    checkResult = CheckEquality(assert);
                    break;
                case AssertType.AreNotEqual:
                    checkResult = CheckNonEquality(assert);
                    break;
                case AssertType.IsTrue:
                    checkResult = CheckIsTrue(assert);
                    break;
                case AssertType.IsFalse:
                    checkResult = CheckIsFalse(assert);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!checkResult)
                throw new AssertException(assert.ExceptionMessage);
        }

        private bool CheckEquality(Assert assert)
        {
            var value = _variableAction.Get(assert.ValueVariable);
            var expectedValue = _variableAction.Get(assert.ExpectedVariable);

            return value.Equals(expectedValue);
        }

        private bool CheckNonEquality(Assert assert)
        {
            return !CheckEquality(assert);
        }

        private bool CheckIsTrue(Assert assert)
        {
            var value = (bool)_variableAction.Get(assert.ValueVariable);
            return value;
        }

        private bool CheckIsFalse(Assert assert)
        {
            return !CheckIsTrue(assert);
        }
    }
}