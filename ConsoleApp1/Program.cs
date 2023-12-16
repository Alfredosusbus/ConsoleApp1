using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            // Create a context
            Context context = ContextSingleton.Instance;

            // Prompt user to input values for variables
            Console.Write("Enter the value for variable A (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool aValue))
                context.Assign("A", aValue);

            Console.Write("Enter the value for variable B (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool bValue))
                context.Assign("B", bValue);

            Console.Write("Enter the value for variable C (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool cValue))
                context.Assign("C", cValue);

            Console.Write("Enter the value for variable D (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool dValue))
                context.Assign("D", dValue);

            // Prompt user to choose a logical expression
            Console.WriteLine("Choose a logical expression:");
            Console.WriteLine("1. (A AND B) OR (C AND NOT D)");
            Console.WriteLine("2. (A OR B) AND (C OR D)");
            Console.WriteLine("3. (A AND B AND C) OR (NOT D)");
            Console.Write("Enter the number of the chosen expression (1, 2, or 3): ");

            // Read user's choice
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                IExpression expression;

                // Create the chosen logical expression
                switch (choice)
                {
                    case 1:
                        expression = new OrExpression(
                            new AndExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                            new AndExpression(new TerminalExpression("C"), new NotExpression(new TerminalExpression("D")))
                        );
                        break;

                    case 2:
                        expression = new AndExpression(
                            new OrExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                            new OrExpression(new TerminalExpression("C"), new TerminalExpression("D"))
                        );
                        break;

                    case 3:
                        expression = new OrExpression(
                            new AndExpression(new TerminalExpression("A"), new AndExpression(new TerminalExpression("B"), new TerminalExpression("C"))),
                            new NotExpression(new TerminalExpression("D"))
                        );
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Using the default expression: (A AND B) OR (C AND NOT D)");
                        expression = new OrExpression(
                            new AndExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                            new AndExpression(new TerminalExpression("C"), new NotExpression(new TerminalExpression("D")))
                        );
                        break;
                }

                // Interpret the expression and print the result
                bool result = expression.Interpret(context);
                Console.WriteLine("Interpreted Result: " + result);
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting...");
            }

            // Wait for user input before closing the console window
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}