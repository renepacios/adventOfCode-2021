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
            PartTwo();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static void PartOne()
        {
            var fileData = DataReader.Read("sample.txt", s => (s)).ToList();

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

                    var sum = cartoon.GetSumOfBoardWithWinLine();

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

                    var sum = cartoon.GetSumOfBoardWithWinLine();

                    Console.WriteLine($"The sum is {sum} with number {number}. Product and Result is {number * sum}");


                }
            }

        }





    }


    public class Cartoon
    {
        public List<Board> Boards { get; set; }

        public Cartoon()
        {
            Boards = new List<Board>();
        }

        public void MarkNumber(string number)
        {
            foreach (var board in Boards)
            {
                board.MarkNumber(number);
            }
        }


        public bool CheckIfLine() => Boards.Any(b => b.ThereAreAnyLine);


        public int GetSumOfBoardWithWinLine()
        {
            var winBoard = Boards.First(b => b.ThereAreAnyLine);

            var res = winBoard.GetSumLineWinByRows();

            Console.WriteLine($"Board {winBoard.BoarNumber} When {winBoard.FirstWin?.Ticks ?? -1 }");

            return res != 0 ? res : winBoard.GetSumLineWinByCols();
        }


    }

    public class Board
    {
        public int BoarNumber { get; }
        private int rowNumber;
        public Board(int boarNumber = 0)
        {
            BoarNumber = boarNumber;
            rowNumber = 0;
            Numbers = new List<Number>();
        }
        public List<Number> Numbers { get; set; }


        public bool ThereAreAnyLine
        {
            get
            {
                var dev = GetSumLineWinByRows() != -1 || GetSumLineWinByCols() != -1;

                if (dev && !FirstWin.HasValue)
                {
                    FirstWin = DateTime.Now;
                }

                return dev;
            }
        }

        public DateTime? FirstWin { get; set; }


        public void AddRow(string numberRow)
        {
            var aux = numberRow
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select((s, i) => new Number(s, i, rowNumber));

            Numbers.AddRange(aux);
            rowNumber++;
        }



        public int GetSumLineWinByRows(bool debug = false)
        {

            if (debug) Console.WriteLine("Debug Line By Row");
            for (var i = 0; i <= Numbers.Max(m => m.Y); i++)
            {
                var therAreAnyWithOutMark = Numbers.Any(w => w.Y == i && !w.IsMarked);

                if (debug)
                {

                    Numbers
                        .Where(w => w.Y == i)
                        .ToList()
                        .ForEach(s => Console.WriteLine($"[{s.X},{s.Y}] - {s.Digit} ({(s.IsMarked ? "*" : " ")})"));
                }


                if (!therAreAnyWithOutMark)
                    return Numbers.Where(w => w.Y == i).Sum(s => s.Digit);


            }

            return -1;

        }

        public int GetSumLineWinByCols(bool debug = false)
        {
            if (debug) Console.WriteLine("Debug Line By Col");


            for (var i = 0; i <= Numbers.Max(m => m.X); i++)
            {
                var therAreAnyWithOutMark = Numbers.Any(w => w.X == i && !w.IsMarked);


                if (debug)
                {

                    Numbers
                        .Where(w => w.X == i)
                        .ToList()
                        .ForEach(s => Console.WriteLine($"[{s.X},{s.Y}] - {s.Digit} ({(s.IsMarked ? "*" : " ")})"));
                }

                if (!therAreAnyWithOutMark)
                    return Numbers.Where(w => w.X == i).Sum(s => s.Digit);
            }
            return -1;
        }



        public void MarkNumber(string number)
        {
            int n = Convert.ToInt32(number);

            var num = Numbers.FirstOrDefault(w => w.Digit == n);

            if (num == null) return;

            num.IsMarked = true;
            Numbers = Numbers.Where(w => w.Digit != n).ToList();
            Numbers.Add(num);
        }
    }

    public class Number
    {

        public bool IsMarked { get; set; }

        public int Digit { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Number(string stringDigit, int x, int y)
        {
            Digit = Convert.ToInt32(stringDigit);
            X = x;
            Y = y;
        }



    }


}
