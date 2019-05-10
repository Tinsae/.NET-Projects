using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int die1 = 0;
            int die2 = 0;
            int[] frequency = new int[11];
            int sum = 0;
            for (int i = 0; i < 36000; i++)
            {
                die1 = rnd.Next(1, 6);
                die2 = rnd.Next(1, 6);
                sum = die1 + die2;
                frequency[sum - 2] += 1;
            }
            for (int i = 0; i < frequency.Length; i++)
            {
                Console.WriteLine(frequency[i]);
            }
            Console.ReadKey();
        }
    }
}
