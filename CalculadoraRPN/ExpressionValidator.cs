using System;
using System.Collections.Generic;

public class ExpressionValidator
{
    public static bool CheckParentheses(string expression)
    {
        // Pila para almacenar los paréntesis de apertura
        ArrayStack<T> stack = new ArrayStack<T>();

        foreach (char ch in expression)
        {
            if (ch == '(')
            {
                // Guarda el paréntesis de apertura en la pila
                stack.Push(ch);
            }
            else if (ch == ')')
            {
                // Si encontramos un cierre pero la pila está vacía,
                // significa que hay un ')' sin su '(' correspondiente.
                if (stack.Count == 0)
                {
                    return false;
                }

                // Saca el '(' equivalente de la pila
                stack.Pop();
            }
        }

        // Si la pila quedó vacía, todos los paréntesis se cerraron correctamente.
        return stack.Count == 0;
    }
}