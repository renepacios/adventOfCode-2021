using AdventUtils;
using System;

namespace Day04ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static void PartTwo()
        {
            var fileData = DataReader.Read("data.txt", s => (s));

            string oxygen = string.Empty,
                co2 = string.Empty;

            Console.WriteLine($"There are oxygen {oxygen } binary value co2 {co2} binary value . The decimal is {Convert.ToInt32(oxygen, 2)} oxygen and {Convert.ToInt32(co2, 2)}  co2. The Product is  {Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2)}");
        }

        private static void PartOne()
        {
            var fileData = DataReader.Read("data.txt", s => (s));

            string oxygen = string.Empty,
                co2 = string.Empty;

            Console.WriteLine($"There are oxygen {oxygen } binary value co2 {co2} binary value . The decimal is {Convert.ToInt32(oxygen, 2)} oxygen and {Convert.ToInt32(co2, 2)}  co2. The Product is  {Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2)}");
        }
    }
}
