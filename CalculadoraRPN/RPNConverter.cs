using System;

namespace StackDev
{
    internal static class RPNConverter
    {
        
        private const int _priorityAddSub = 1;
        private const int _priorityMulDiv = 2;
        private const int _priorityPow = 3;
        private const int _priorityNone = 0;

    
        public static string ConvertToRPN(string expression)
        {
            string output = "";
            IStack<char> stack = new ArrayStack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

            
                if (c == ' ')
                    continue;

               
                if (char.IsDigit(c))
                {
                    while (i < expression.Length && char.IsDigit(expression[i]))
                    {
                        output += expression[i];
                        i++;
                    }
                    output += " ";
                    i--;
                    continue;
                }

              
                if (c == '(')
                {
                    stack.Push(c);
                    continue;
                }

               
                if (c == ')')
                {
                    while (!stack.Empty && stack.Peek() != '(')
                    {
                        output += stack.Pop();
                        output += " ";
                    }
                    stack.Pop();
                    continue;
                }

               
                if (IsOperator(c))
                {
                    while (!stack.Empty
                           && stack.Peek() != '('
                           && Priority(stack.Peek()) >= Priority(c))
                    {
                        output += stack.Pop();
                        output += " ";
                    }
                    stack.Push(c);
                }
            }

           
            while (!stack.Empty)
            {
                output += stack.Pop();
                output += " ";
            }

            return output.Trim();
        }

    
        private static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
        }

        private static int Priority(char op)
        {
            if (op == '+' || op == '-') return _priorityAddSub;
            if (op == '*' || op == '/') return _priorityMulDiv;
            if (op == '^') return _priorityPow;
            return _priorityNone;
        }
    }
}