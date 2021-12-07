namespace Day04ConsoleApp
{
    using System;

    public class Number
    {

        public bool IsMarked { get; set; }

        public bool IsOldPrize { get; set; }

        public int Digit { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Number(string stringDigit, int x, int y)
        {
            Digit = Convert.ToInt32(stringDigit);
            X = x;
            Y = y;
            IsOldPrize = false;
        }

        public void CheckAsPaidPrize() => IsOldPrize = true;

        public void Print()
        {
            if (IsOldPrize)
            {
                Console.Write("#");
            }
            else if (IsMarked) Console.Write("*");
            else
            {
                Console.Write(" ");
            }

            Console.Write($"{Digit.ToString().PadLeft(2, ' ')} ");
        }
    }
}