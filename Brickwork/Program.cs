using System;
using System.Linq;
using System.Text;

namespace Brickwork
{
    class Program
    {
        private static int[,] inputLayer;

        static void Main(string[] args)
        {
            var valid = ReadInputLayer();
            if (!valid)
            {
                return;
            }

            var solver = new BrickworkSolver(inputLayer);
            var outputLayer = solver.Solve();

            Console.WriteLine("Output layer");
            Print(outputLayer);
        }
        

        private static void Print(int[,] layer)
        {
            Console.WriteLine(string.Join("", Enumerable.Repeat('*', layer.GetLength(1) * 3 + 1)));
            for (var i = 0; i < layer.GetLength(0); i++)
            {
                var numericRowFormatted = new StringBuilder();
                numericRowFormatted.Append('*');

                var delimiterRowFormatter = new StringBuilder();
                delimiterRowFormatter.Append('*');

                for (var j = 0; j < layer.GetLength(1); j++)
                {
                    numericRowFormatted.AppendFormat("{0,2}", layer[i, j]);

                    if (j + 1 < layer.GetLength(1))
                    {
                        numericRowFormatted.Append(
                            layer[i, j] == layer[i, j + 1]
                                ? ' '
                                : '*');

                    }

                    if (i + 1 < layer.GetLength(0))
                    {
                        if (layer[i, j] == layer[i + 1, j])
                        {
                            delimiterRowFormatter.Append("  *");
                        }
                        else
                        {
                            delimiterRowFormatter.Append("***");
                        }
                    }
                }
                numericRowFormatted.Append('*');
                Console.WriteLine(numericRowFormatted.ToString());

                if (i + 1 < layer.GetLength(0))
                {
                    Console.WriteLine(delimiterRowFormatter.ToString());
                }
            }

            Console.WriteLine(string.Join("", Enumerable.Repeat('*', layer.GetLength(1) * 3 + 1)));
        }
        

        private static bool ReadInputLayer()
        {
            var dimentionString = Console.ReadLine();

            var dimentions = dimentionString
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
        
            if (dimentions.Count != 2)
            {
                Console.WriteLine("Only 2D array is allowed.");
                return false;
            }

            int n = 0;
            int m = 0;
            for (int i = 0; i < dimentions.Count; i++)
            {
                int dimention;

                string dimentionName = i == 0 ? "N" : "M";

                if (!int.TryParse(dimentions[i], out dimention))
                {
                    Console.WriteLine("{0} is not an integer. Please try again.", dimentionName);
                    return false;
                }

                if (dimention <= 0)
                {
                    Console.WriteLine("{0} should be greater than 0", dimentionName);
                    return false;
                }

                if (dimention % 2 != 0)
                {
                    Console.WriteLine("{0} should be even number.", dimentionName);
                    return false;
                }

                if (dimention > 100)
                {
                    Console.WriteLine("{0} should not exceed 100", dimentionName);
                    return false;
                }

                if (i == 0)
                {
                    n = dimention;
                }
                else
                {
                    m = dimention;
                }
            }

            inputLayer = new int[n, m];
            for (var i = 0; i < n; i++)
            {
                var numbersString = Console.ReadLine();

                var numbers = numbersString
                    .Split(' ')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                if (numbers.Count != m)
                {
                    Console.WriteLine("Line should contain {0} numbers but received {1}", m, numbers.Count);
                    return false;
                }

                for (var j = 0; j < numbers.Count; j++)
                {
                    int intValue;
                    if (int.TryParse(numbers[j], out intValue))
                    {
                        inputLayer[i, j] = intValue;
                    }
                    else
                    {
                        Console.WriteLine("Line should contain {0} integer values.", m);
                        return false;
                    }
                }
            }

            /* validate the first layer of bricks. */
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int counter = 0;
                    if (i >= 1 && inputLayer[i, j] == inputLayer[i - 1, j])
                    {
                        counter++;
                    }
                    if (j >= 1 && inputLayer[i, j] == inputLayer[i, j - 1])
                    {
                        counter++;
                    }
                    if (i + 1 < n && inputLayer[i, j] == inputLayer[i + 1, j])
                    {
                        counter++;
                    }
                    if (j + 1 < m && inputLayer[i, j] == inputLayer[i, j + 1])
                    {
                        counter++;
                    }

                    if (counter > 1)
                    {
                        Console.WriteLine("Invalid layer of bricks!");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}