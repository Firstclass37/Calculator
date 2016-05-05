using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public double AutoCalc(double arg1, double arg2,Operations operation)
        {
            switch (operation)
            {
                case Operations.Sum: return Sum(arg1, arg2);
                case Operations.Sub: return Sub(arg1, arg2);
                case Operations.Dev: return Dev(arg1, arg2);
                case Operations.Mul: return Mul(arg1, arg2);
            }
            return 0;

        }
        public double Sum(double inpu1, double input2)
        {
            return inpu1 + input2;
        }
        public double Sub(double inpu1, double input2)
        {
            return inpu1 - input2;
        }
        public double Dev(double inpu1, double input2)
        {
            return inpu1 / input2;
        }
        public double Mul(double inpu1, double input2)
        {
            return inpu1 * input2;
        }
    }
}
