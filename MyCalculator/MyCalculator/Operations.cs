using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{


    static class Operations
    {
        static List<string> postfixOperations = new List<string> { "!" };
        static List<string> prefixOperations = new List<string> { "sin", "cos", "tan" };
        static List<string> binaryOperations = new List<string> { "^", "*", "/", "+", "-" };

        public static OperationType GetOperationType(string operation)
        {   
            if (postfixOperations.Contains(operation))
                return OperationType.Postfix;

            if (binaryOperations.Contains(operation))
                return OperationType.Binary;

            return OperationType.Prefix;
        }

       // return true if priority of first operation highter or equal
        public static bool PriorityCompare(string first, string second)
        {
            if (second == "(")
                return true;

            if (GetOperationType(second) == OperationType.Prefix)
                return false;

            int tmp = binaryOperations.IndexOf(first) - binaryOperations.IndexOf(second);

            if (tmp > 1) return false;
            if ((tmp == 1) && (first == "*" || first == "+")) return false;

            return true;
        }

        public static bool IsOperation(string c)
        {
            if (postfixOperations.Contains(c)
                || prefixOperations.Contains(c)
                || binaryOperations.Contains(c))
            {
                return true;
            }

            return false;
        }

        public static double Perform(string op, params string[] args)
        {
            switch (op)
            {
                case "!":
                    return MathNet.Numerics.SpecialFunctions.Factorial(int.Parse(args[0]));
                case "sin":
                    return Math.Sin(double.Parse(args[0]));
                case "cos":
                    return Math.Cos(double.Parse(args[0]));
                case "tan":
                    return Math.Tan(double.Parse(args[0]));
                case "^":
                    return Math.Pow(double.Parse(args[1]), double.Parse(args[0]));
                case "*":
                    return double.Parse(args[1]) * double.Parse(args[0]);
                case "/":
                    return double.Parse(args[1]) / double.Parse(args[0]);
                case "+":
                    return double.Parse(args[1]) + double.Parse(args[0]);
                case "-":
                    return double.Parse(args[1]) - double.Parse(args[0]);
            }

            return 0;
        }
    }
}
