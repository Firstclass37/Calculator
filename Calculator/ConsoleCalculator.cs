using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class ConsoleCalculator
    {
        public string ErrorMessage { get; set; }
        double? previousResult = null;
        string[] operations = new string[] { "-", "+", "/", "*" ,"!","^"};


        public bool TryCalculate(string inputString, out double result)
        {
            string expressionString = inputString.Replace(".", ",").Replace("pi",Math.PI.ToString()).Replace("E",Math.E.ToString()).Replace(" ",String.Empty).ToLower();
            result = 0;
            if ( !CheckBrackets(expressionString) || CheckUnacceptableSymbols(expressionString) || !CheckEexpressionСorrectness(expressionString)) return false;
            result = Calculate(expressionString);
            previousResult = result;
            History.Add(expressionString + " = " + result.ToString());
            return true;

        }

        private double Calculate(string expressionString)
        {
            string modifiedExpressionString = string.Copy(expressionString);
            if (operations.Contains(expressionString[0].ToString())) modifiedExpressionString = previousResult.ToString() + expressionString;
            try
            {
                modifiedExpressionString = CulculateBrackets(modifiedExpressionString);
                modifiedExpressionString = CalculateTrigonometricFunctions(modifiedExpressionString);
                modifiedExpressionString = CalculeteSmallExpression(modifiedExpressionString);
            }
            catch (DivideByZeroException)
            {
                ErrorMessage = "Devide by zero!!!";
                return 0;
            }

            return  double.Parse(modifiedExpressionString);

        }

        private string CulculateBrackets(string expressionString)
        {
            string result = string.Copy(expressionString);            
            while (result.Contains("("))
            {
                int leftBrecketPos = result.LastIndexOf("(");
                int rightBracketPos = GetRightBraketIndex(result,leftBrecketPos);
                string smallExpression = result.Substring(leftBrecketPos,rightBracketPos - leftBrecketPos+1);
                string smallResult = CalculateTrigonometricFunctions(smallExpression.Remove(0, 1).Remove(smallExpression.Length - 2, 1));
                smallResult = CalculeteSmallExpression(smallResult);
                result = result.Replace(smallExpression,smallResult);
            }
            return result;

        }

        private string CalculeteSmallExpression(string expressionString)
        {
            int index = 0;
            double result = MulDiv(expressionString,ref index);

            while (index < expressionString.Length-1)
            {
                if (expressionString[index] == '+')
                {
                    index++;
                    result += MulDiv(expressionString, ref index);
                    break;
                }
                if (expressionString[index] == '-')
                {
                    index++;
                    result -= MulDiv(expressionString, ref index);
                    break;
                }

            }

            return result.ToString();
        }
        private double MulDiv(string expressionString, ref int index)
        {
            double result = Sqr(expressionString, ref index);

            while (index < expressionString.Length - 1)
            {
                if (expressionString[index] == '*')
                {
                    index++;
                    result *= Sqr(expressionString, ref index);
                    break;
                }
                else if (expressionString[index] == '/')
                {
                    index++;
                    result /= Sqr(expressionString, ref index);
                    break;
                }
                else break;
                

            }
            return result;

        }
        private double Fact(string expressionString, ref int index)
        {
            double number = GetNumber(expressionString, ref index); //todo if number is double - ?????
            double result = 1;
            if (index < expressionString.Length && expressionString[index] == '!')
            {
                int i = 1;
                while (i <= number)
                {
                    result *= i;
                    i++;
                }
                return result;
            }
            return number;
        }       
        private double Sqr(string expressionString, ref int index)
        {
            double number = Fact(expressionString,ref index);

            if (index < expressionString.Length && expressionString[index] == '^')
            {
                index++;
                number = Math.Pow(number, Fact(expressionString, ref index));
            }
            return number;
        }
        private double GetNumber(string expressionString, ref int index)
        {
            string result = string.Empty;
            if (expressionString.Substring(index)[0] == '-')
            {
                index++;
                result += "-";
            }
            foreach (char c in expressionString.Substring(index))
            {               
                if (char.IsDigit(c) || c==',')
                {
                    result += c.ToString();
                    index++;
                }
                else break;
            }
            return double.Parse(result);

        }

        private string CalculateTrigonometricFunctions(string expressionString)
        {
            string resultString = string.Copy(expressionString);
            if (expressionString.Contains("sin")) resultString = CalculateSin(resultString);
            if (expressionString.Contains("cos")) resultString = CalculateCos(resultString);
            if (expressionString.Contains("tg")) resultString = CalculateTg(resultString);
            if (expressionString.Contains("lg")) resultString = CalculateLg(resultString);
            if (expressionString.Contains("ln")) resultString = CalculateLn(resultString);
            if (expressionString.Contains("exp")) resultString = CalculateExp(resultString);
            return resultString;

        }
        private string CalculateSin(string expressionString)
        {
            string resultStringWoSin = String.Copy(expressionString);

            while (resultStringWoSin.Contains("sin"))
            {
                int index = expressionString.IndexOf("s") + 3;
                double value = GetNumber(expressionString, ref index);
                double result = Math.Sin(value*Math.PI/180);
                resultStringWoSin = resultStringWoSin.Replace("sin"+value.ToString(),result.ToString());
            }
            return resultStringWoSin;

        }
        private string CalculateCos(string expressionString)
        {
            string resultStringWoSin = String.Copy(expressionString);

            while (resultStringWoSin.Contains("cos"))
            {
                int index = expressionString.IndexOf("c") + 3;
                double value = GetNumber(expressionString, ref index);
                double result = Math.Cos(value * Math.PI / 180);
                resultStringWoSin = resultStringWoSin.Replace("cos" + value.ToString(), result.ToString());
            }
            return resultStringWoSin;

        }
        private string CalculateTg(string expressionString)
        {
            string resultStringWoSin = String.Copy(expressionString);

            while (resultStringWoSin.Contains("tg"))
            {
                int index = expressionString.IndexOf("t") + 2;
                double value = GetNumber(expressionString, ref index);
                double result = Math.Tan(value * Math.PI / 180);
                resultStringWoSin = resultStringWoSin.Replace("tg" + value.ToString(), result.ToString());
            }
            return resultStringWoSin;
        }
        private string CalculateLg(string expressionString)
        {
            string resultStringWoSin = String.Copy(expressionString);

            while (resultStringWoSin.Contains("lg"))
            {
                int index = expressionString.IndexOf("lg") + 2;
                double value = GetNumber(expressionString, ref index);
                double result = Math.Log10(value);
                resultStringWoSin = resultStringWoSin.Replace("lg" + value.ToString(), result.ToString());
            }
            return resultStringWoSin;
        }
        private string CalculateLn(string expressionString)
        {
            string resultStringWoSin = String.Copy(expressionString);

            while (resultStringWoSin.Contains("ln"))
            {
                int index = expressionString.IndexOf("ln") + 2;
                double value = GetNumber(expressionString, ref index);
                double result = Math.Log(value);
                resultStringWoSin = resultStringWoSin.Replace("ln" + value.ToString(), result.ToString());
            }
            return resultStringWoSin;
        }
        private string CalculateExp(string expressionString)
        {
            string resultStringWoSin = String.Copy(expressionString);

            while (resultStringWoSin.Contains("exp"))
            {
                int index = expressionString.IndexOf("exp") + 3;
                double value = GetNumber(expressionString, ref index);
                double result = Math.Exp(value);
                resultStringWoSin = resultStringWoSin.Replace("exp" + value.ToString(), result.ToString());
            }
            return resultStringWoSin;
        }

        private int GetRightBraketIndex(string expressionString, int leftBraketIndex)
        {
            for (int i = leftBraketIndex; i < expressionString.Length; i++)
            {
                if (expressionString[i] == ')') return i;
            }
            return 0;
                
        }


        private bool CheckBrackets(string inputString)
        {
            int leftBracketsCount = 0;
            int rightBracketsCount = 0;

            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] == '(') leftBracketsCount++;
                if (inputString[i] == ')') rightBracketsCount++;
            }

            if (leftBracketsCount == rightBracketsCount) return true;
            else
            {
                ErrorMessage = "BracketsError";
                return false;
            }
        }

        private bool CheckUnacceptableSymbols(string inputString)
        {
            string unacceptableSymbols = "qwrtyeuiop[]';lkjhgfdsazxcvbm?=_%$#'&йцукенгшщзхждлорпавыфячсмитьбю№";

            for (int i = 0; i < inputString.Length; i++)
            {
                if (unacceptableSymbols.Contains(inputString[i].ToString().ToLower()))
                {
                    if (inputString[i] == 'c' && inputString[i + 1] == 'o' && inputString[i + 2] == 's')
                    {
                        i += 3;
                        continue;
                    }
                    if (inputString[i] == 's' && inputString[i + 1] == 'i' && inputString[i + 2] == 'n')
                    {
                        i += 3;
                        continue;
                    }
                    if (inputString[i] == 't' && inputString[i + 1] == 'g')
                    {
                        i += 2;
                        continue;
                    }
                    if (inputString[i] == 'p' && inputString[i + 1] == 'i')
                    {
                        i += 2;
                        continue;
                    }
                    if (inputString[i] == 'l' && inputString[i + 1] == 'g')
                    {
                        i += 2;
                        continue;
                    }
                    if (inputString[i] == 'l' && inputString[i + 1] == 'n')
                    {
                        i += 2;
                        continue;
                    }
                    if (inputString[i] == 'e' && inputString[i + 1] == 'x' && inputString[i + 2] == 'p')
                    {
                        i += 3;
                        continue;
                    }
                    ErrorMessage = "UnacceptableSymbols";
                    return true;
                }                
            }
            return false;
        }

        private bool CheckEexpressionСorrectness(string inputString)
        {
            
            if (previousResult == null && operations.Contains(inputString[0].ToString())) { ErrorMessage = "IncorrectInput"; return false;  }

            for (int i = 0;i< inputString.Length-1; i++)
            {
                if (operations.Contains(inputString[i].ToString()) && operations.Contains(inputString[i + 1].ToString())) { ErrorMessage = "IncorrectInput"; return false; }       
            }

            return true;
        }

      
    }
}
