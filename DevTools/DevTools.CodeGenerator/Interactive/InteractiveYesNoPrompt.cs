using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.CodeGenerator.Interactive;
public static class InteractiveYesNoPrompt
{
    public static bool Ask(string question, string yesOptionDescription = "Yes", string noOptionDescription = "No", bool sameLine = true)
    {
        string[] opcoes = { (yesOptionDescription ?? "Yes"), (noOptionDescription ?? "No") };
        int selected = 0; // 0 = Yes, 1 = No
        ConsoleKey key;

        if (sameLine)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{question} ");
            Console.ResetColor();
            
            RenderOptionsInline(selected, question.Length + 1);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(question);
            Console.ResetColor();

            RenderOptionsMultiline(selected);
        }

        do
        {
            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if ( key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow )
            {
                selected = selected == 0 ? 1 : 0;

                if ( sameLine )
                {
                    Console.SetCursorPosition(question.Length + 1, Console.CursorTop);
                    RenderOptionsInline(selected, question.Length + 1);
                }
                else
                {
                    // Move cursor para linha da resposta
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    RenderOptionsMultiline(selected);
                }
            }

        } while ( key != ConsoleKey.Enter );


        Console.WriteLine(); // nova linha apenas se for multiline

        return selected == 0;
    }

    private static void RenderOptionsInline(int selected, int cursorStart)
    {
        RenderOption("Yes", selected == 0);
        Console.Write("/");
        RenderOption("No", selected == 1);
    }

    private static void RenderOptionsMultiline(int selected)
    {
        RenderOption("Yes", selected == 0);
        Console.Write("/");
        RenderOption("No", selected == 1);
        Console.WriteLine();
    }

    private static void RenderOption(string label, bool selected)
    {
        if ( selected )
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{label}]");
            Console.ResetColor();
        }
        else
        {
            Console.Write($" {label} ");
        }
    }
}
