using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace AdventUtils
{
    public static class DataReader
    {

        public static IEnumerable<T> Read<T>(string fileName, Func<string, T> transform)
        {

            var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(appPath ?? string.Empty, fileName);

            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                yield return transform(line);
            }
        }


        public static IEnumerable<int> ReadInts(string fileName)
        {
            Func<string, int> exp = s => int.TryParse(s, out var o) ? o : 0;

            var data = Read<int>(fileName, exp);

            return data;

        }

    }




}

