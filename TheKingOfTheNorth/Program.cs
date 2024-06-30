using System;
using System.Collections.Generic;

namespace TheKingOfTheNorth
{
    class Program
    {
        static int row, column, castleRow, castleColumn;
        static int[] dx = { 0, 1, 0, -1 };
        static int[] dy = { 1, 0, -1, 0 };

        static void Main(string[] args)
        {
            (row, column) = GetValidInput(3, 300, 3, 300);

            Dinic dinic = new Dinic(2 * row * column + 1);
            int source = 2 * row * column;

            for (int x = 0; x < row; x++)
            {
                string[] values = GetValidLineInput();
                for (int y = 0; y < column; y++)
                {
                    if (x == 0 || y == 0 || x == row - 1 || y == column - 1)
                    {
                        dinic.AddEdge(source, GetNodeIndex(x, y), long.MaxValue);
                    }
                    int value = int.Parse(values[y]);
                    dinic.AddEdge(GetNodeIndex(x, y), row * column + GetNodeIndex(x, y), value);

                    for (int dir = 0; dir < 4; dir++)
                    {
                        int nextX = x + dx[dir], nextY = y + dy[dir];
                        if (nextX < 0 || nextX == row || nextY < 0 || nextY == column)
                            continue;
                        dinic.AddEdge(row * column + GetNodeIndex(x, y), GetNodeIndex(nextX, nextY), long.MaxValue);
                    }
                }
            }

            (castleRow, castleColumn) = GetValidInput(1, row - 2, 1, column - 2);

            Console.WriteLine(dinic.MaxFlow(source, row * column + GetNodeIndex(castleRow, castleColumn)));
        }

        static (int, int) GetValidInput(int minRow, int maxRow, int minColumn, int maxColumn)
        {
            int inputRow;
            int inputColumn;
            while (true)
            {
                string[] input = Console.ReadLine().Split();
                if (input.Length == 2 && int.TryParse(input[0], out inputRow) && int.TryParse(input[1], out inputColumn))
                {
                    if (inputRow >= minRow && inputRow <= maxRow && inputColumn >= minColumn && inputColumn <= maxColumn)
                        break;

                    Console.WriteLine($"Error: Values must be between {minRow}-{maxRow} for row and {minColumn}-{maxColumn} for column.");
                }
                else
                    Console.WriteLine("Error: Please enter two valid integers.");
            }
            return (inputRow, inputColumn);
        }

        private static string[] GetValidLineInput()
        {
            bool validInput = false;
            string[] values = new string[column];
            while (!validInput)
            {
                string line = Console.ReadLine();
                values = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == column)
                {
                    validInput = true;
                    foreach (var value in values)
                    {
                        if (!int.TryParse(value, out _))
                        {
                            validInput = false;
                            Console.WriteLine("Error: Each value must be an integer.");
                            break;
                        }
                    }
                }
                else
                    Console.WriteLine($"Error: You must enter exactly {column} values.");
            }
            return values;
        }

        private static int GetNodeIndex(int x, int y)
        {
            return x * column + y;
        }
    }
}