using NUnit.Framework;
using BL;
using BL.ExportedMembers;
using System;
using System.Linq;
using AssemblyForTesting;

namespace Tests
{
    public class TypeInstanceTests
    {
        private const string CalculatorFullName = "AssemblyForTesting.Calculator";
        
        [SetUp]
        public void Setup()
        {
            var calc = new Calculator(5); // just to init assembly
            var assembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.GetName().Name == "AssemblyForTesting");
            ReflectedModel.Build(assembly);
        }

        [Test]
        public void TypeIsLoadedTest()
        {
            var allTypes = ReflectedModel.ScenarioTypes;

            var calculatorType = allTypes.FirstOrDefault(x => x.FullName == CalculatorFullName);

            Assert.IsNotNull(calculatorType);
        }

        [Test]
        public void ConstructInstanceTest()
        {
            var instance = GetCalculatorInstance(5);

            Assert.IsNotNull(instance);
        }

        [Test]
        public void GetSetFieldTest()
        {
            var calc = GetCalculatorInstance(2);
            const string fieldName = "InitialSum";

            var fieldValue = calc.GetField(fieldName);
            Assert.AreEqual(0, fieldValue);

            calc.SetField(fieldName, 5);
            fieldValue = calc.GetField(fieldName);
            Assert.AreEqual(5, fieldValue);
        }

        [Test]
        public void GetSetPropertyTest()
        {
            var calc = GetCalculatorInstance(2);
            const string propertyName = "IsIncludeCoefficient";

            var propertyValue = calc.GetProperty(propertyName);
            Assert.AreEqual(false, propertyValue);

            calc.SetProperty(propertyName, true);
            propertyValue = calc.GetProperty(propertyName);
            Assert.AreEqual(true, propertyValue);
        }

        [Test]
        public void InvokeMethodsTest()
        {
            var calc = GetCalculatorInstance(5);
            const int a = 2;
            const int b = 3;
            const int expectedSum = a + b;
            const int expectedMultiple = a * b;
            var parameters = new object[] {a, b};

            var sum = calc.InvokeMethod("Sum", parameters);
            Assert.AreEqual(expectedSum, sum);

            var multiple = calc.InvokeMethod("Multiply", parameters);
            Assert.AreEqual(expectedMultiple, multiple);
        }

        private static TypeInstance GetCalculatorInstance(int multiplier)
        {
            var calculatorType = ReflectedModel.ScenarioTypes.First(x => x.FullName == CalculatorFullName);

            return calculatorType.ConstructObject(new object[] {multiplier});
        }
    }
}