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

        public void MarkNumber(string number, bool debug = false)
        {
            foreach (var board in Boards)
            {
                var found = board.MarkNumber(number);


                if (!found || !debug) continue;
                Console.WriteLine($"\nMark {number}");
                board.PrintBoard();

            }
        }

        public Board PopBoard(int boardNumber)
        {
            var board = Boards.First(w => w.BoarNumber == boardNumber);

            Boards = Boards.Where(w => w.BoarNumber != boardNumber).ToList();

            return board;
        }

        public bool CheckIfLine() => Boards.Any(b => b.ThereAreAnyLine);


        //public int GetSumOfBoardWithWinLine()
        //{
        //    var winBoard = Boards.First(b => b.ThereAreAnyLine);

        //    var res = winBoard.GetPrizeLineWinByRows();

        //    Console.WriteLine($"Board {winBoard.BoarNumber} When {winBoard.FirstWin?.Ticks ?? -1 }");

        //    return res != 0 ? res : winBoard.GetSumLineWinByCols();
        //}


        public int GetSumWithOutMarkOfBoardWithWinLine(bool debug = false)
        {
            var winBoard = Boards.First(b => b.ThereAreAnyLine);

            if (debug) winBoard.PrintBoard();
            //Mark all winLine digits to check next prizes
            var markedInBoard = winBoard.Numbers.Where(w => w.IsMarked).ToList();
            markedInBoard.ForEach(m => m.IsOldPrize = true);
            winBoard.Numbers = winBoard.Numbers.Where(w => !w.IsMarked).ToList();
            winBoard.Numbers.AddRange(markedInBoard);

            return winBoard.Numbers.Where(w => !w.IsMarked).Sum(s => s.Digit);
        }

        public IEnumerable<(int, Board)> GetBoardWithWinLine(bool debug = false)
        {
            var winBoards = Boards.Where(b => b.ThereAreAnyLine);

            foreach (var winBoard in winBoards)
            {
                if (debug) winBoard.PrintBoard();
                //Mark all winLine digits to check next prizes
                var markedInBoard = winBoard.Numbers.Where(w => w.IsMarked).ToList();
                markedInBoard.ForEach(m => m.IsOldPrize = true);
                winBoard.Numbers = winBoard.Numbers.Where(w => !w.IsMarked).ToList();
                winBoard.Numbers.AddRange(markedInBoard);

                var sum = winBoard.Numbers.Where(w => !w.IsMarked).Sum(s => s.Digit);

                yield return (sum, winBoard);
            }

        }
    }
}