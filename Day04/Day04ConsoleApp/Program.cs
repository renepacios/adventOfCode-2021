﻿using AdventUtils;
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
           // PartTwo();

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
            var fileData = DataReader.Read("data.txt", s => (s));

            string oxygen = string.Empty,
                co2 = string.Empty;

            Console.WriteLine($"There are oxygen {oxygen } binary value co2 {co2} binary value . The decimal is {Convert.ToInt32(oxygen, 2)} oxygen and {Convert.ToInt32(co2, 2)}  co2. The Product is  {Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2)}");
        }





    }


    public class Cartoon
    {
        public List<Board> Boards { get; set; }

        public Cartoon()
        {
            Boards=new List<Board>();
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

            return res != 0 ? res : winBoard.GetSumLineWinByCols();
        }


    }

    public class Board
    {
        private int rowNumber;
        public Board()
        {
            rowNumber = 0;
            Numbers = new List<Number>();
        }
        public List<Number> Numbers { get; set; }


        public bool ThereAreAnyLine => GetSumLineWinByRows() != -1 || GetSumLineWinByCols() != -1;


        public void AddRow(string numberRow)
        {
            var aux = numberRow
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select((s, i) => new Number(s, i, rowNumber));

            Numbers.AddRange(aux);
            rowNumber++;
        }



        public int GetSumLineWinByRows(bool debug)
        {
            for (var i = 0; i <= Numbers.Max(m => m.Y); i++)
            {
                var therAreAnyWithOutMark = Numbers.Any(w => w.Y == i && !w.IsMarked);

                if (!therAreAnyWithOutMark)
                    return Numbers.Where(w => w.Y == i).Sum(s => s.Digit);

            }

            return -1;

        }

        public int GetSumLineWinByCols()
        {
            for (var i = 0; i <= Numbers.Max(m => m.X); i++)
            {
                var therAreAnyWithOutMark = Numbers.Any(w => w.X == i && !w.IsMarked);
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
