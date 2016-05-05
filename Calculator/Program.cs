using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            string inputString = String.Empty;
            while ((inputString = Console.ReadLine()) != null)
            {
                int result = 0;
                string[] stringArg = inputString.Split(new Char[] { '-', '+', '/', '*' });
                int arg1 = 0;
                int arg2 = 0;
                if (int.TryParse(stringArg[0], out arg1) && int.TryParse(stringArg[1], out arg2))
                {
                    if (inputString.Contains("-")) { Console.WriteLine("Result: " + calculator.Sub(arg1, arg2).ToString()); continue; }
                    if (inputString.Contains("+")) { Console.WriteLine("Result: " + calculator.Sum(arg1, arg2).ToString()); continue; }
                    if (inputString.Contains("/")) { Console.WriteLine("Result: " + calculator.Dev(arg1, arg2).ToString()); continue; }
                    if (inputString.Contains("*")) { Console.WriteLine("Result: " + calculator.Mul(arg1, arg2).ToString()); continue; }                   
                }
                else Console.WriteLine("Неккоректный ввод! Попробуйте снова!)");

            }
        }

    }
}
