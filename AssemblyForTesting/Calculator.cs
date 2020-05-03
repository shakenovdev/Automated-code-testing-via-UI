using Scenario.Annotations;

namespace AssemblyForTesting
{
    [ScenarioActivation(ActivationMode.All)]
    [ScenarioDescription("Калькулятор", "Позволяет посчитать различные значения")]
    public class Calculator
    {
        private readonly int _multiplier;
        public int InitialSum;

        public Calculator(int multiplier)
        {
            _multiplier = multiplier;
        }
        
        public bool IsIncludeCoefficient { get; set; }

        [ScenarioDescription("Сумма")]
        public int Sum(int a, int b)
        {
            if (IsIncludeCoefficient)
                return InitialSum + a + b;

            return a + b;
        }
        
        public int Multiply(int a, int b)
        {
            if (IsIncludeCoefficient)
                return a * b * _multiplier;

            return a * b;
        }
    }
}
