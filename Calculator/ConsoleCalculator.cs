using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class ConsoleCalculator
    {
        double? previousResult = null;
        int MaxOperationCount = 1;
        int MaxArgumentsCount = 2;
        string[] operations = new string[] { "-", "+", "/", "*" };


        public bool TryCalculate(string inputString,out double result)
        {
            bool isCalculated = false;
            result = 0;

            if (CheckOperations(inputString) != String.Empty && CheckArgumentsCount(inputString) == 1 && previousResult != null)
            {
                double arg = 0;
                string[] argumets = inputString.Split(operations, StringSplitOptions.RemoveEmptyEntries);
                if (double.TryParse(argumets[0], out arg))
                {
                    result = Calculate(previousResult.Value, arg, CheckOperations(inputString));
                    previousResult = result;
                    isCalculated = true;
                }    
            }

            if (CheckOperations(inputString) != String.Empty && CheckArgumentsCount(inputString) == 2)
            {
                double arg1 = 0;
                double arg2 = 0;
                string[] argumets = inputString.Split(operations, StringSplitOptions.RemoveEmptyEntries);
                if (double.TryParse(argumets[0], out arg1) && double.TryParse(argumets[1], out arg2))
                {
                    result = Calculate(arg1, arg2, CheckOperations(inputString));
                    previousResult = result;
                    isCalculated = true;
                }

            }

            return isCalculated;

        }

        private double Calculate(double arg1, double arg2, string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "-": result = arg1 - arg2;break;
                case "+": result = arg1 + arg2; break;
                case "*": result = arg1 * arg2; break;
                case "/": result = arg1 / arg2; break;
            }

            return result;
        }

        private int CheckArgumentsCount(string inputString)
        {
            string[] argumentsString = inputString.Split(operations,StringSplitOptions.RemoveEmptyEntries);
            return argumentsString.Count();
        }

        private string CheckOperations(string inputString)
        {
            int currentOperationsCount = 0;
            string operation = String.Empty;

            for (int i = 0; i < inputString.Length; i++)
            {
                if (operations.Contains(inputString[i].ToString()))
                {
                    currentOperationsCount++;
                    operation = inputString[i].ToString();
                }       
            }

            if (currentOperationsCount == 1) return operation;
            else return string.Empty;           
        }
    }
}
