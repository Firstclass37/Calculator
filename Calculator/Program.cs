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
            ShowHelp();

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
                    Console.WriteLine(calculator.ErrorMessage);
                }

            }
        }

        private static  bool CheckCommand(string inputString)
        {
            if (inputString == "-c")
            {
                Console.Clear();
                ShowHelp();
                return true;
            }
            return false;
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"
Operations: E PI - + / * ^ !  lg(<value>)  ln(<value>)  sin(<Value>) cos(<Value>) tg(<value>)  exp(<value>)          
                               ");

        }

     
    }
}
