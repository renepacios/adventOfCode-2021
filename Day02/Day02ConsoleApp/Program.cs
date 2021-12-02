using System;

namespace Day02ConsoleApp
{
    using AdventUtils;

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
            var fileData = DataReader.Read("data.txt", s => new DataModel(s));

            int depth = 0, horizontal = 0;

            foreach (var data in fileData)
            {
                if (data.Move == Move.forward)
                {
                    horizontal += data.Distance;
                }
                else
                {
                    depth += data.Distance;
                }
            }

            Console.WriteLine($"There are {depth} depth and {horizontal} horizontal. The product is {depth * horizontal}");

        }


        static void PartTwo()
        {
            var fileData = DataReader.Read("data.txt", s => new DataModel(s));

            int depth = 0, horizontal = 0, aim = 0;

            foreach (var data in fileData)
            {
                if (data.Move == Move.forward)
                {
                    horizontal += data.Distance;
                    depth += data.Distance * aim;
                }
                else
                {
                    aim += data.Distance;
                }
            }

            Console.WriteLine($"There are {depth} depth and {horizontal} horizontal. The product is {depth * horizontal}");


        }
    }

    public enum Move
    {
        forward,
        down,
        up
    }

    public class DataModel
    {


        public Move Move { get; set; }

        public int Distance { get; set; }

        public DataModel(string lineToParse)
        {

            var parts = lineToParse.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (!Enum.TryParse(typeof(Move), parts[0], true, out var move)) throw new ArgumentException($"Cannot parse move type {parts}");
            if (!int.TryParse(parts[1], out var distance)) throw new ArgumentException($"Cannot parse distance {parts}");

            Move = (Move)move;

            Distance = distance * (Move == Move.up ? -1 : 1);
        }

    }
}
