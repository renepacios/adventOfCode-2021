using System;

namespace Day01ConsoleApp
{
    using AdventUtils;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            PartOne();

            PartTwo();
            Console.ReadLine();
        }

        static void PartOne()
        {
            var fileData = DataReader.ReadInts("data.txt");
            int prev = int.MaxValue;
            int count = 0;
            foreach (int n in fileData)
            {
                if (n > prev) count++;
                prev = n;
            }
            Console.WriteLine($"There are {count} measurements are larger than the previous measurement");

        }


        static void PartTwo()
        {
            var fileData = DataReader.ReadInts("data.txt").ToArray();

            int count = 0;

            for (int i = 0; i <= fileData.Length - 4; i ++)
            {
                var win1 = fileData[i..(i + 3)].Sum();
                var win2 = fileData[(i + 1)..(i + 4)].Sum();

                if (win1 < win2) count++;

            }
            Console.WriteLine($"There are {count} sums are larger than the previous sum");

        }
    }
}
