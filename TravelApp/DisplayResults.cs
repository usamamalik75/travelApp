using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp
{
    public class DisplayResults
    {
        public void Print<T>(T[] results, string resultType, string[] headers)
        {
            if (results.Length == 0)
            {
                Console.WriteLine($"No {resultType} found.");
                return;
            }

            Console.WriteLine($"\n{resultType} Search Results:");

            PrintTableHeader(headers);
            PrintTableDivider(headers.Length);

            foreach (var item in results)
            {
                Console.WriteLine(item.ToString());
            }

            PrintTableDivider(headers.Length);
        }

        private static void PrintTableHeader(string[] headers)
        {
            Console.WriteLine("| " + string.Join(" | ", headers) + " |");
        }

        private static void PrintTableDivider(int columnCount)
        {
            string divider = new string('-', 15 * columnCount);
            Console.WriteLine($"|{divider}|");
        }
    }
}
