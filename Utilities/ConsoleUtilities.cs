﻿using System;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.Fluent;

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
                            phrases.Select(p => new Column {Width = GridLength.Auto})
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
            Console.WriteLine($"{message}\n{exception}".Red());
        }
    }
}