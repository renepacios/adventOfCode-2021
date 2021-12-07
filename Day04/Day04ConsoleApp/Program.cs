using AdventUtils;
using System;

namespace Day04ConsoleApp
{
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
          //  PartTwo();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static void PartOne()
        {
            //var fileData = DataReader.Read("sample.txt", s => (s)).ToList();
            var fileData = DataReader.Read("data.txt", s => (s)).ToList();

            string randoms = fileData.First();


            //LoadCartoon
            Board board = null;
            Cartoon cartoon = new Cartoon();

            foreach (var line in fileData.Skip(1))
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (board != null) { cartoon.Boards.Add(board); }
                    board = new Board();
                    continue;
                }

                board.AddRow(line);
            }
            cartoon.Boards.Add(board); //file not has blank line at end

            //Play Bingo Game
            foreach (var s in randoms.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                cartoon.MarkNumber(s);
                if (cartoon.CheckIfLine())
                {
                    int number = Convert.ToInt32(s);

                    var sum = cartoon.GetSumWithOutMarkOfBoardWithWinLine();

                    Console.WriteLine($"The sum is {sum} with number {number}. Product and Result is {number * sum}");

                    break;
                }
            }





        }

        private static void PartTwo()
        {
            var fileData = DataReader.Read("sample.txt", s => (s)).ToList();

            string randoms = fileData.First();


            //LoadCartoon
            Board board = null;
            Cartoon cartoon = new Cartoon();
            int boarNumber = 0;
            foreach (var line in fileData.Skip(1))
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (board != null) { cartoon.Boards.Add(board); }
                    board = new Board(++boarNumber);
                    continue;
                }

                board.AddRow(line);
            }
            cartoon.Boards.Add(board); //file not has blank line at end

            var second = new SecondPart(cartoon);

            //Play Bingo Game
            foreach (var s in randoms.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                cartoon.MarkNumber(s);

                var winBooards = second.GetBoardsWithLine();

                if (cartoon.CheckIfLine())
                {
                    int number = Convert.ToInt32(s);

                    var sum = cartoon.GetSumWithOutMarkOfBoardWithWinLine();

                    Console.WriteLine($"The sum is {sum} with number {number}. Product and Result is {number * sum}");


                }
            }

        }





    }
}
