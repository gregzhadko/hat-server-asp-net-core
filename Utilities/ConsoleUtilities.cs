using System;
using System.Linq;
using Alba.CsConsoleFormat;

namespace Utilities
{
    public static class ConsoleUtilities
    {
        public static void WriteInfo(string firstColumn, params string[] phrases)
        {
            var document = new Document
            {
                Background = ConsoleColor.Black,
                Color = ConsoleColor.Gray,
                Children =
                {
                    new Grid
                    {
                        Stroke = new LineThickness(0, 0),
                        StrokeColor = ConsoleColor.DarkGray,
                        Columns =
                        {
                            new Column {Width = GridLength.Auto, MinWidth = 10},
                            new Column {Width = GridLength.Auto, MinWidth = 12},
                            phrases.Select(p => new Column {Width = GridLength.Star(1)})
                        },
                        Children =
                        {
                            new Cell
                            {
                                Stroke = new LineThickness(0, 0),
                                Children = {DateTime.Now.ToString("hh:mm:ss")}
                            },
                            new Cell
                            {
                                Stroke = new LineThickness(0, 0),
                                Color = ConsoleColor.Green,
                                Children = {firstColumn}
                            },
                            phrases.Select(phrase => new[]
                            {
                                new Cell {Children = {$" {phrase}"}, Stroke = new LineThickness(0, 0)}
                            })
                        }
                    }
                }
            };

            ConsoleRenderer.RenderDocument(document);
        }

        public static void WriteException(Exception exception, string message = "")
        {
            WriteRedLine($"{message}:\n{exception}");
        }

        public static void WriteValidLine(string message)
        {
            WriteLine(message, ConsoleColor.Green);
        }

        public static void WriteRedLine(string message)
        {
            WriteLine(message, ConsoleColor.Red);
        }

        public static void WriteRed(string message) => Write(message, ConsoleColor.Red);

        public static void WriteGreen(string message) => Write(message, ConsoleColor.Green);

        private static void WriteLine(string message, ConsoleColor color)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{message}");
            Console.ForegroundColor = temp;
        }

        private static void Write(string message, ConsoleColor color)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write($"{message}");
            Console.ForegroundColor = temp;
        }
    }
}