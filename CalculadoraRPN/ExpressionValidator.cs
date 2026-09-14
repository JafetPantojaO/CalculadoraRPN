using StackDev;
using System;
using System.Collections.Generic;

public class ExpressionValidator
{
    // (-A-) Verificador de balanceo de paréntesis
    public static bool CheckParentheses(string expression)
    {
        // Pila de caracteres para almacenar los paréntesis de apertura
        ArrayStack<char> stack = new ArrayStack<char>();

        foreach (char ch in expression)
        {
            if (ch == '(')
            {
                // Guarda el paréntesis de apertura en la pila
                stack.Push(ch);
            }
            else if (ch == ')')
            {
                // Si encontramos un cierre pero la pila está vacía, no está balanceado
                if (stack.Empty)
                {
                    return false;
                }

                // Saca el '(' equivalente de la pila
                stack.Pop();
            }
        }

        // Si la pila quedó vacía, todos los paréntesis se cerraron correctamente
        return stack.Empty;
    }

    // Prioridad de los operadores (+, -, *, /, ^)
    private static int Priority(char op)
    {
        if (op == '^') return 3;
        if (op == '*' || op == '/') return 2;
        if (op == '+' || op == '-') return 1;
        return 0;
    }

    // (-B-) Convertidor a notación RPN (devuelve la lista de tokens)
    public static List<string> ConvertToRPN(string expression)
    {
        List<string> output = new List<string>();
        ArrayStack<char> stack = new ArrayStack<char>();

        int i = 0;
        while (i < expression.Length)
        {
            char ch = expression[i];

            // Ignora los espacios en blanco
            if (ch == ' ')
            {
                i++;
                continue;
            }

            // Si es un número (puede tener varios dígitos)
            if (char.IsDigit(ch))
            {
                string number = "";
                while (i < expression.Length && char.IsDigit(expression[i]))
                {
                    number += expression[i];
                    i++;
                }
                output.Add(number);
                continue;
            }

            // Si es una variable (letras)
            if (char.IsLetter(ch))
            {
                string variable = "";
                while (i < expression.Length && (char.IsLetterOrDigit(expression[i]) || expression[i] == '_'))
                {
                    variable += expression[i];
                    i++;
                }
                output.Add(variable);
                continue;
            }

            // Si es paréntesis de apertura '('
            if (ch == '(')
            {
                stack.Push(ch);
            }
            // Si es paréntesis de cierre ')'
            else if (ch == ')')
            {
                // Saca de la pila a la salida hasta topar con '('
                while (!stack.Empty && stack.Peek() != '(')
                {
                    output.Add(stack.Pop().ToString());
                }

                // Descarta el '(' de la pila
                if (!stack.Empty && stack.Peek() == '(')
                {
                    stack.Pop();
                }
            }
            // Si es un operador (+, -, *, /, ^)
            else if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^')
            {
                // Desapila los que tengan mayor o igual prioridad
                while (!stack.Empty && stack.Peek() != '(' && Priority(stack.Peek()) >= Priority(ch))
                {
                    // La potencia '^' se asocia por la derecha
                    if (ch == '^' && stack.Peek() == '^')
                    {
                        break;
                    }

                    output.Add(stack.Pop().ToString());
                }

                stack.Push(ch);
            }

            i++;
        }

        // Vacía los operadores que hayan quedado en la pila
        while (!stack.Empty)
        {
            output.Add(stack.Pop().ToString());
        }

        return output;
    }

    // Sobrecarga que regresa la expresión RPN en un solo string
    public static string ConvertToRPNString(string expression)
    {
        List<string> rpn = ConvertToRPN(expression);
        return string.Join(" ", rpn.ToArray());
    }
}