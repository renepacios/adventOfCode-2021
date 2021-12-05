using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04ConsoleApp
{
    public class SecondPart
    {
        private readonly Cartoon _cartoon;

        public SecondPart(Cartoon cartoon)
        {
            _cartoon = cartoon;
        }


        public IEnumerable<BoardWinInfo> GetBoardsWithLine()
        {
            return _cartoon.Boards.Where(HasWinLine)
                  .Select(s => new BoardWinInfo(s));
        }

        private bool HasWinLine(Board arg)
        {
            var marked = arg.Numbers
                .Where(w => w.IsMarked)
                .ToList();

            var hasRow = marked
                .GroupBy(g => g.X)
                .Any(g => g.Count() == 5);

            var hasCol = marked
                .GroupBy(g => g.Y)
                .Any(g => g.Count() == 5);

            return hasCol && hasRow;
        }
    }

    public class BoardWinInfo
    {
        public Board Board { get; }

        public BoardWinInfo(Board board)
        {
            Board = board;

        }
    }
}
