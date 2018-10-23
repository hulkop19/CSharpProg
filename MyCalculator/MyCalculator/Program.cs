using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            string formatExpr = Format(expression);
            double ansver = Calculator.Solve(formatExpr);
            Console.WriteLine(ansver);
        }

        static string Format(string expression)
        {
            string expr = expression.Replace(" ", "");
            StringBuilder newSb = new StringBuilder();

            foreach (char elem in expr) {
                if (elem == '.'
                    || char.IsDigit(elem) && (newSb.Length == 0 
                    || char.IsDigit(newSb[newSb.Length - 1]) || newSb[newSb.Length - 1] == '.')
                    || char.IsLetter(elem) && (newSb.Length == 0
                    || char.IsLetter(newSb[newSb.Length - 1])))
                {
                    newSb.Append(elem);
                }
                else
                {
                    newSb.Append(" " + elem);
                }
            }

            return newSb.ToString().Trim(' ');
        }
    }
}
