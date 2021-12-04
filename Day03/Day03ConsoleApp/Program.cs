

namespace Day03ConsoleApp
{
    using AdventUtils;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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




        static void PartOne()
        {
            var fileData = DataReader.Read("data.txt", s => s);

            int[] n0 = new int[12];
            int[] n1 = new int[12];

            foreach (var data in fileData)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == '1')
                    {
                        n1[i]++;
                    }
                    else
                    {
                        n0[i]++;
                    }
                }
            }


            string bgamma = string.Empty,
                bepsilon = string.Empty;

            for (int i = 0; i < n0.Length; i++)
            {
                if (n0[i] < n1[i])
                {
                    bgamma += "1";
                    bepsilon += "0";
                }
                else
                {
                    bgamma += "0";
                    bepsilon += "1";
                }

            }

            Console.WriteLine($"There are gamma {bgamma} binary value bepsilon {bepsilon} binary value . The decimal is {Convert.ToInt32(bgamma, 2)} gamma and {Convert.ToInt32(bepsilon, 2)}  epsilon. The Product is  {Convert.ToInt32(bgamma, 2) * Convert.ToInt32(bepsilon, 2)}");

        }


        static void PartTwo()
        {
          var fileData = DataReader.Read("data.txt", s => new Model(s));
       

            string oxygen = string.Empty,
                co2 = string.Empty;



            oxygen = FindOxygenValue(fileData);
            co2 = FindCO2Value(fileData);

            Console.WriteLine($"There are oxygen {oxygen } binary value co2 {co2} binary value . The decimal is {Convert.ToInt32(oxygen , 2)} oxygen and {Convert.ToInt32(co2, 2)}  co2. The Product is  {Convert.ToInt32(oxygen , 2) * Convert.ToInt32(co2, 2)}");


        }



        private static string FindOxygenValue(IEnumerable<Model> data, int position = 0)
        {
            Debug.Assert(data != null, nameof(data) + " != null");

            var list = data.ToList();

            if (list.Count == 1)
            {
                return list.First().BinaryData;
            }



            var n0 = list.Count(w => w.BinaryData[position] == '0');
            var n1 = list.Count(w => w.BinaryData[position] == '1');

            if (position >= 12) throw new IndexOutOfRangeException($"Position {position}");

            IEnumerable<Model> dev;
            if (n0 > n1)
            {
                var position1 = position;
                dev = list.Where(s => s.BinaryData[position1] == '0');
            }
            else
            {
                var position1 = position;
                dev = list.Where(s => s.BinaryData[position1] == '1');
            }

            return FindOxygenValue(dev, ++position);


        }

        private static string FindCO2Value(IEnumerable<Model> data, int position = 0)
        {
            Debug.Assert(data != null, nameof(data) + " != null");

            var list = data.ToList();

            if (list.Count == 1)
            {
                return list.First().BinaryData;
            }



            var n0 = list.Count(w => w.BinaryData[position] == '0');
            var n1 = list.Count(w => w.BinaryData[position] == '1');

            if (position >= 12) throw new IndexOutOfRangeException($"Position {position}");

            IEnumerable<Model> dev;
            if (n0 > n1)
            {
                var position1 = position;
                dev = list.Where(s => s.BinaryData[position1] == '1');
            }
            else
            {
                var position1 = position;
                dev = list.Where(s => s.BinaryData[position1] == '0');
            }

            return FindCO2Value(dev, ++position);


        }
    }


    public class Model
    {
        public string BinaryData { get; set; }

        byte GetBit(byte position) => (byte)BinaryData[position];

        public Model(string binaryData)
        {
            BinaryData = binaryData.Trim();
        }

    }
}
