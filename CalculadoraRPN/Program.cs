using StackDev;
using System;
using System.Collections.Generic;

namespace CalculadoraRPN
{
    internal class Program
    {
        // (-D-) Entorno REPL (Read-Eval-Print-Loop)
        static void Main(string[] args)
        {
            // Workspace para almacenar las variables en memoria
            Dictionary<string, double> workspace = new Dictionary<string, double>();

            Console.WriteLine("=================================================");
            Console.WriteLine("              CALCULADORA RPN (REPL)             ");
            Console.WriteLine("=================================================");
            Console.WriteLine("Escribe 'exit' o 'salir' para salir\n");

            while (true)
            {
                Console.Write(">> ");
                string? line = Console.ReadLine();

                // Ignora si no se escribió nada
                if (string.IsNullOrWhiteSpace(line)) continue;
                line = line.Trim();

                // Terminar el programa
                if (line.ToLower() == "exit" || line.ToLower() == "salir")
                {
                    Console.WriteLine("Saliendo...");
                    break;
                }

                try
                {
                    // Asignación de variables: variable = expresión
                    if (line.Contains("=") && !line.Contains("=="))
                    {
                        string[] parts = line.Split(new char[] { '=' }, 2);
                        string varName = parts[0].Trim();
                        string expr = parts[1].Trim();

                        // Verifica que los paréntesis estén bien balanceados
                        if (!ExpressionValidator.CheckParentheses(expr))
                        {
                            Console.WriteLine("Error: Paréntesis no balanceados.\n");
                            continue;
                        }

                        // Convierte a RPN y luego evalúa
                        List<string> rpn = ExpressionValidator.ConvertToRPN(expr);
                        double result = ExpressionValidator.EvaluateRPN(rpn, workspace);

                        // Guarda o actualiza la variable en el workspace
                        workspace[varName] = result;

                        Console.WriteLine("[RPN: " + string.Join(" ", rpn.ToArray()) + "]");
                        Console.WriteLine(varName + " = " + result + "\n");
                    }
                    else
                    {
                        // Evaluación directa de una expresión infija
                        if (!ExpressionValidator.CheckParentheses(line))
                        {
                            Console.WriteLine   ("Error: Paréntesis no balanceados.\n");
                            continue;
                        }

                        // Convierte a RPN y calcula el resultado
                        List<string> rpn = ExpressionValidator.ConvertToRPN(line);
                        double result = ExpressionValidator.EvaluateRPN(rpn, workspace);

                        Console.WriteLine("[RPN: " + string.Join(" ", rpn.ToArray()) + "]");
                        Console.WriteLine("= " + result + "\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n");
                }
            }
        }
    }
}
