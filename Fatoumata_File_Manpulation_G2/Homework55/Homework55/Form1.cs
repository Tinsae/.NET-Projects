using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework55
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory("output");

            Random rnd = new Random();
            using (System.IO.StreamWriter file =

            new System.IO.StreamWriter(@"output/random_number.txt"))
            {
                for (int i = 0; i < 20; i++)
                {

                    file.WriteLine(rnd.Next(1, 41));

                }
            }

            string[] lines = System.IO.File.ReadAllLines(@"output/random_number.txt");

            int sum = 0;
            foreach (string line in lines)
            {
                sum += int.Parse(line);
            }

            MessageBox.Show("sum is " + sum);
            MessageBox.Show("average is " + sum / 20.0);


            // initialize to some big number
            int min = 10000;
            foreach (string line in lines)
            {
                int num = int.Parse(line);
                if (num < min)
                    min = num;

            }
            MessageBox.Show("smallest is " + min);

            richTextBox1.Text += "sum is " + sum + "\n";
            richTextBox1.Text += "average is " + sum / 20.0 + "\n";
            richTextBox1.Text += "smallest is " + min + "\n";


            System.IO.StreamReader readFile = new System.IO.StreamReader(@"output/random_number.txt");
            string eachLine;
            char counter = 'a';
            System.IO.Directory.CreateDirectory("temp_files");

            while ((eachLine = readFile.ReadLine()) != null)
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"temp_files/temp" + counter + ".txt"))
                {

                    file.WriteLine(eachLine);

                }
                counter++;

            }
            readFile.Close();


           
            int ic = 0;

            for (char i = 'a'; i < 'u'; i++, ic++)
            {

                for (char j = 'a'; j < 'u' - ic - 1; j++)
                {
                    int arrj;
                    int arrjplus1;
                    // read the jth file like tempa for the first round
                    using (System.IO.StreamReader read1 = new System.IO.StreamReader(@"temp_files/temp" + (char)j + ".txt"))
                    {
                        // read number
                        arrj = int.Parse(read1.ReadLine());

                    }
                    // read the j+1 file like tempb for th first round
                    using (System.IO.StreamReader read2 = new System.IO.StreamReader(@"temp_files/temp" + (char)(j + 1) + ".txt"))
                    {
                        // read number
                        arrjplus1 = int.Parse(read2.ReadLine());

                    }

                    // if a file with a smaller letter name has larger number than a file with a larger letter name
                    // swap the numbers
                    if (arrj > arrjplus1)
                    {
                        // write the number in j+1 file to j file
                        // like writing whatever in tempb to tempa because tempa is greater
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"temp_files/temp" + (char)j + ".txt"))
                        {
                            file.WriteLine(arrjplus1);
                        }
                        // write the number in j file to j+1 file
                        // like writing whatever in tempa to tempb because tempa is greater
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"temp_files/temp" + (char)(j + 1) + ".txt"))
                        {
                            file.WriteLine(arrj);
                        }

                    }

                }

            }

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(@"output/sorted_random_number.txt"))
            {
                foreach (string fileName in System.IO.Directory.GetFiles("temp_files"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(@fileName))
                    {
                        writer.WriteLine(reader.ReadLine());

                    }

            }
        }



            //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("temp_files");

            //foreach (System.IO.FileInfo file in di.GetFiles())
            //{
            //    file.Delete();
            //}


            int line_number = 0;
            double median = 0;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"output/sorted_random_number.txt"))
            {
                while ((eachLine = reader.ReadLine()) != null)
                {
                    // if index is either of the two add the read number to median
                    if (line_number == 9 || line_number == 10)
                        median += int.Parse(reader.ReadLine());
                    line_number++;
                }
            }
            // calculate median by dividing the sum by 2
            median = median / 2.0;
            richTextBox1.Text += "Median = " + median;

            }
    }
}
