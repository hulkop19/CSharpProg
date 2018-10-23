using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MyCalculator
{
    static class Calculator
    {
        static public double Solve(string expr)
        {
            string rpnExpr = ConvertToReversePolish(expr);

            return SolveRPN(rpnExpr);
        }

        static string ConvertToReversePolish(string expr)
        {
            StringBuilder rpnSb = new StringBuilder();
            Stack<string> opStack = new Stack<string>();
            List<string> parseExpr = new List<string>(expr.Split(' '));
            double tmp = 0;

            foreach (string elem in parseExpr)
            {
                if (double.TryParse(elem, out tmp))
                {
                    rpnSb.Append(elem + " ");
                }
                else if (elem == "(")
                {
                    opStack.Push(elem);
                }
                else if (elem == ")")
                {
                    while (opStack.Peek() != "(")
                    {
                        rpnSb.Append(opStack.Pop() + " ");
                    }

                    opStack.Pop();
                }
                else if (!Operations.IsOperation(elem))
                {
                    Console.WriteLine("Error: invalid expression");
                }
                else if (Operations.GetOperationType(elem) == OperationType.Postfix)
                {
                    opStack.Push(elem);
                }
                else if (Operations.GetOperationType(elem) == OperationType.Prefix)
                {
                    opStack.Push(elem);
                }
                else if (Operations.GetOperationType(elem) == OperationType.Binary)
                {
                    while (opStack.Count != 0 && !Operations.PriorityCompare(elem, opStack.Peek()))
                    {
                        rpnSb.Append(opStack.Pop() + " ");
                    }

                    opStack.Push(elem);
                }
            }

            while (opStack.Count != 0)
                rpnSb.Append(opStack.Pop() + " ");

            return rpnSb.ToString().Trim(' ');
        }

        static double SolveRPN(string rpnExpr)
        {
            Stack<string> stk = new Stack<string>();

            foreach (string elem in rpnExpr.Split(' '))
            {
                if (!Operations.IsOperation(elem))
                {
                    stk.Push(elem);
                }
                else if (Operations.GetOperationType(elem) != OperationType.Binary)
                {
                    stk.Push(Operations.Perform(elem, stk.Pop()).ToString());
                } else
                {
                    stk.Push(Operations.Perform(elem, stk.Pop(), stk.Pop()).ToString());
                }
            }

            return double.Parse(stk.Pop(), CultureInfo.InvariantCulture);
        }
    }
}
