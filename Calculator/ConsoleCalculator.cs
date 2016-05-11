﻿using System;
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
            string expressionString = inputString.Replace(".", ",").ToLower().Replace("pi",Math.PI.ToString()).Replace(" ",String.Empty);
            result = 0;
            if ( !CheckBrackets(expressionString) || CheckUnacceptableSymbols(expressionString) || !CheckEexpressionСorrectness(expressionString)) return false;
            result = Calculate(expressionString);
            previousResult = result;
            return true;

        }

        private double Calculate(string expressionString)
        {
            string modifiedExpressionString = string.Copy(expressionString);
            if (operations.Contains(expressionString[0].ToString())) modifiedExpressionString = previousResult.ToString() + expressionString;
          
            modifiedExpressionString = CulculateBrackets(modifiedExpressionString);
            if (expressionString.Contains("sin")) modifiedExpressionString = CalculateSin(modifiedExpressionString);
            if (expressionString.Contains("cos")) modifiedExpressionString = CalculateCos(modifiedExpressionString);
            if (expressionString.Contains("tg")) modifiedExpressionString = CalculateTg(modifiedExpressionString);
            if (expressionString.Contains("lg")) modifiedExpressionString = CalculateLg(modifiedExpressionString);
            if (expressionString.Contains("ln")) modifiedExpressionString = CalculateLn(modifiedExpressionString);
            modifiedExpressionString = CalculeteSmallExpression(modifiedExpressionString);
           
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
                string smallResult = CalculeteSmallExpression(smallExpression.Remove(0,1).Remove(smallExpression.Length-2, 1));
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
            string unacceptableSymbols = "qwertyuiop[]';lkjhgfdsazxcvbm?=_%$#'&йцукенгшщзхждлорпавыфячсмитьбю№";

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
