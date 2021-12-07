using AdventUtils;
using System;

namespace Day04ConsoleApp
{
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            PartOne();

            Console.WriteLine("----- PART TWO------");
            PartTwo();


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

                    var sum = cartoon.GetSumWithOutMarkOfBoardWithWinLine(true);

                    Console.WriteLine($"\nThe sum is {sum} with number {number}. Product and Result is {number * sum}");

                    break;
                }
            }





        }

        private static void PartTwo()
        {
            // var fileData = DataReader.Read("sample.txt", s => (s)).ToList();
            var fileData = DataReader.Read("data.txt", s => (s)).ToList();

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

            //  var second = new SecondPart(cartoon);

            List<Board> winBoards = new List<Board>();
            int lastNumberWithFirstBoardWin = -1;
            int lastSumWithFirstBoardWin = -1;

            //Play Bingo Game
            foreach (var s in randoms.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                cartoon.MarkNumber(s, false);

                //  var winBooards = second.GetBoardsWithLine();

                if (cartoon.CheckIfLine())
                {
                    int number = Convert.ToInt32(s);

                    IEnumerable<(int, Board)> wins = cartoon.GetBoardWithWinLine(true);

                    foreach ((int sum, Board b) in wins)
                    {

                        if (winBoards.Any(w => w.BoarNumber == b.BoarNumber)) throw new Exception("Repeated Board");

                        var winBoard = cartoon.PopBoard(b.BoarNumber);
                        winBoards.Add(b);
                        lastNumberWithFirstBoardWin = number;
                        lastSumWithFirstBoardWin = sum;

                        // b.PrintBoard();

                        Console.WriteLine($"\nThe sum is {sum} with number {number}. Product and Result is {number * sum}");


                    }

                }

            }
            var lastBoard = winBoards.LastOrDefault();

            if (lastBoard == null)
            {
                Console.WriteLine("Something was wrong!!!!");
                return;
            }


            Console.WriteLine($"Final result last Board {lastBoard.BoarNumber} sum is {lastSumWithFirstBoardWin} with number {lastNumberWithFirstBoardWin}. Product and Result is {lastSumWithFirstBoardWin * lastNumberWithFirstBoardWin}");

        }





    }
}
