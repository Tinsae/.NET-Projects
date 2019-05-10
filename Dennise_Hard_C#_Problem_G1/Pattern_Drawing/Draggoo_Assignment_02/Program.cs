// Denise Draggoo
// BIT 142
// Assignment 02
// Assignment 2 program prints a specific "quilt" pattern
//   based on how wide the user requests. The general 
//   pattern is set, but the width varies based on user input.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draggoo_Assignment_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            int size = p.welcomeMessage();
            p.firstLastRow(size);
            for (int line_num = 1; line_num <= 8; line_num++)
                p.printLine(line_num, size);
            p.firstLastRow(size);
            Console.ReadKey();
        }

        // The welcomeMessage method prints the welcome message
        // and requests the size of the quilt to be printed.
        public int welcomeMessage()
        {
            Console.WriteLine("Welcome to Tina's Quilts! I'm glad you're here!");
            Console.WriteLine("What size quilt would you like?");
            int quiltSize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Sure! Coming right up...");
            return quiltSize;
        }

        // The firstLastRow method prints each of the first
        // and last rows of the quilt.
        public void firstLastRow(int size)
        {
            for (int i = 1; i <= size; i++)
            {
                Console.Write("#================#");
            }
            Console.WriteLine();
        }

        // The printLine method prints one of the center
        // rows of the quilt. 
        public void printLine(int line_number, int size)
        {
            if (line_number == 5)
            {
                line_number = 4;
            }
            else if (line_number == 6)
            {
                line_number = 3;

            }
            else if (line_number == 7)
            {
                line_number = 2;

            }
            else if (line_number == 8)
            {
                line_number = 1;

            }


            spaces(line_number, size);
            bar();
            if(line_number == 1)
            {
                arrows(size);

            }
            else
            {
                arrows(1);
            }
            dots(line_number, size);
            if (line_number == 1)
            {
                arrows(size);

            }
            else
            {
                arrows(1);
            }
            bar();
            Console.WriteLine();
        }

        // The spaces method takes the size of the quilt
        // and adds the requisite number of blank spaces
        // before each row of the quilt.

        public void spaces(int line_num, int size)
        {

            int spaces = ((line_num * -2) + 8) * size;

            if (line_num == 1)
            {
                spaces += size;
            }
            else
            {
                spaces += 3 + (3 * (size - 2));

            }
            int i = 0;

            while (i < spaces)
            {
                Console.Write(" ");
                i++;
            }

        }

        // The dots method takes the size of quilt and inserts the dot-
        // portion in the middle of the quilt.
        public void dots(int line_num, int size)
        {
            int dots = ((line_num * 4) - 4) * size;
            int i = 0;
            while (i < dots)
            {
                Console.Write(".");
                i++;
            }
        }

        // print bar
        public void bar()
        {
            Console.Write("|");
        }

        // print arrow
        public void arrows(int size)
        {
            for(int i=1; i <= size; i++)
            {
                Console.Write("<>");
            }
        }

    }
}




