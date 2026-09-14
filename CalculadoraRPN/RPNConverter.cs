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

                //Ignorar espacios
                if (c == ' ')
                    continue;

                //Número (uno o varios dígitos)
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

                //Paréntesis de apertura
                if (c == '(')
                {
                    stack.Push(c);
                    continue;
                }

                //Paréntesis de cierre
                if (c == ')')
                {
                    while (!stack.Empty && stack.Peek() != '(')
                    {
                        output += stack.Pop();
                        output += " ";
                    }
                    stack.Pop(); //descarta el '('
                    continue;
                }

                //Operador
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

            //Vaciar lo que quede en la pila
            while (!stack.Empty)
            {
                output += stack.Pop();
                output += " ";
            }

            return output.Trim()    ;


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