namespace Day04ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Board
    {
        public int BoarNumber { get; }

        public Board(int boarNumber = 0)
        {
            BoarNumber = boarNumber;
            _rowNumber = 0;
            Numbers = new List<Number>();
        }

        public List<Number> Numbers { get; set; }

        public DateTime? FirstWin { get; set; }

        private int _rowNumber;
        public void AddRow(string numberRow)
        {
            var aux = numberRow
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select((s, i) => new Number(s, i, _rowNumber));

            Numbers.AddRange(aux);
            _rowNumber++;
        }




        public bool ThereAreAnyLine
        {
            get
            {
                var dev = GetPrizeLineWinByRows() || GetSumLineWinByCols();

                if (dev && !FirstWin.HasValue)
                {
                    FirstWin = DateTime.Now;
                }

                return dev;
            }
        }


        public bool GetPrizeLineWinByRows(bool debug = false)
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


                //if (!therAreAnyWithOutMark)
                //    return Numbers.Where(w => w.Y == i).Sum(s => s.Digit);

                //if all are this line has mark and is not old line
                if (!therAreAnyWithOutMark)// && Numbers.Any(w => w.Y == i && !w.IsOldPrize))
                {
                    return true;
                }

            }

            return false;

        }

        public bool GetSumLineWinByCols(bool debug = false)
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

                //if (!therAreAnyWithOutMark)
                //    return Numbers.Where(w => w.X == i).Sum(s => s.Digit);

                //if all are this line has mark and is not old line
                if (!therAreAnyWithOutMark)// && Numbers.Any(w => w.X == i && !w.IsOldPrize))
                {
                    return true;
                }

            }
            return false;
        }

        public bool MarkNumber(string number)
        {
            int n = Convert.ToInt32(number);

            var num = Numbers.FirstOrDefault(w => w.Digit == n);

            if (num == null) return false;

            num.IsMarked = true;
            Numbers = Numbers.Where(w => w.Digit != n).ToList();
            Numbers.Add(num);
            return true;
        }

        public int GetSumNotMarked() => Numbers.Where(w => !w.IsMarked).Sum(s => s.Digit);

        public void PrintBoard()
        {
            Console.WriteLine($"Board Number {BoarNumber} isFirstWin {!FirstWin.HasValue}");

            Enumerable.Range(0, Numbers.Max(m => m.X)+1).Select(i => i).ToList().ForEach(i => Console.Write($"  [{i}]"));
            Console.WriteLine(string.Empty);
            for (int i = 0, t = Numbers.Max(m => m.Y); i <= t; i++)
            {
                Console.Write($"({i}) ");
                for (int j = 0, tx = Numbers.Max(m => m.X); j <= tx; j++)
                {
                    var num = Numbers.First(w => w.Y == i && w.X == j);
                    num.Print();
                }
                Console.WriteLine();
            }

        }
    }
}