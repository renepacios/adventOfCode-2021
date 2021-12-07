namespace Day04ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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


        //public int GetSumOfBoardWithWinLine()
        //{
        //    var winBoard = Boards.First(b => b.ThereAreAnyLine);

        //    var res = winBoard.GetPrizeLineWinByRows();

        //    Console.WriteLine($"Board {winBoard.BoarNumber} When {winBoard.FirstWin?.Ticks ?? -1 }");

        //    return res != 0 ? res : winBoard.GetSumLineWinByCols();
        //}


        public int GetSumWithOutMarkOfBoardWithWinLine()
        {
            var winBoard = Boards.First(b => b.ThereAreAnyLine);
            return winBoard.Numbers.Where(w => !w.IsMarked).Sum(s => s.Digit);
        }
    }
}