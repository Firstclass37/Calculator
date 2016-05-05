using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace Calculator
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleCalculator calculator = new ConsoleCalculator();

            string expressionString = string.Empty;

            while ((expressionString = Console.ReadLine()) != null)
            {
                if (CheckCommand(expressionString)) continue;
                double result = 0;
                if (calculator.TryCalculate(expressionString, out result))
                {
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine("Error!!! try again");
                }

            }
        }

        private static  bool CheckCommand(string inputString)
        {
            if (inputString == "-c") { Console.Clear(); return true; }
            return false;
        }

     
    }
}
