using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace JebApp10
{
    class Program
    {
       static string connString = @"Data Source =.\SQLEXPRESS; Initial Catalog = ETL; Integrated Security = True";
        // alternate connection string
        ///static string connString = @"Data Source=tcp:s18.winhost.com; Initial Catalog=DB_120806_etl; User ID=DB_120806_etl_user; Password=Mis3013; Integrated Security=False;";
        // create connection object
        static SqlConnection conn = new SqlConnection(connString);
        static List<string> fileNames;


        // a function returns a csv file in a list format
        public static List<string> LoadCSVFile(string filePath)
        {
            // create a StreamReader object with the given filePath
            var reader = new StreamReader(File.OpenRead(filePath));
            // create new list to store the file content
            List<string> content = new List<string>();

            // skip one line
            reader.ReadLine();
            // while end of file
          
            while (!reader.EndOfStream)
            {
                // read a line
                var line = reader.ReadLine();
                // add to the list
                content.Add(line);
            }
            // return the list
            return content;
        }
        // function takes a directory and returns list of file names
        public static List<string> ReadAllFiles(string directory)
        {
            List<string> fileNames = new List<string>();

            foreach (string fileName in Directory.GetFiles(directory))
            {
                // add file name to a list
                fileNames.Add(fileName);
            }
            // return list
            return fileNames;

        }


        public static void DoCourseSummary()
        {
            // try to open connection
            conn.Open();
            // if it succeds
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection Succeded\n");
            }
            // otherwise
            else
            {
                Console.WriteLine("Connection Failed\n");


            }


            // call the function and get file names
            fileNames = ReadAllFiles("input");

            // print file names
            Console.WriteLine("The following CSV files have been read\n");
            foreach (String f in fileNames)
            {
                Console.WriteLine(f);
            }

            // test load CSV file only with the first file
            List<String> firstContent = LoadCSVFile(fileNames[0]);
            Console.WriteLine("Displaying the head of the csv file");
            int counter = 0;
            foreach (String c in firstContent)
            {
                if (counter > 6)
                    break;
                Console.Write(c.Split(',')[0] + "\t");
                Console.Write(c.Split(',')[1]);
                Console.WriteLine();
                counter++;

            }

            // Do the required Analysis for course table
            // Read all filepaths and store it in a list
            fileNames = ReadAllFiles("input");


            // delete all rows first
            string deleteAllCourse = "DELETE FROM Course";
            string deleteAllCourseStat = "DELETE FROM CourseStatistics";

            SqlCommand delCmd = new SqlCommand(deleteAllCourse, conn);
            SqlCommand delCmd2 = new SqlCommand(deleteAllCourseStat, conn);
            // if query works
            if (delCmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("cleared Course table");
            }
            // if delete query don't work
            else
            {
                Console.WriteLine("Error while clearing Course table");

            }

            // if query works for course statistics
            if (delCmd2.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("cleared Course Stat table");
            }
            // if delete query don't work
            else
            {
                Console.WriteLine("Error while clearing Course Statistics table");

            }



            // for each file path
            foreach (String f in fileNames)
            {
                // input\class_roster_35371_201810.csv
                // use regular expressons
                // find one or more occureances of digits
                String expr = @"\d+";
                // apply the regex expression to the filenames(or paths)
                MatchCollection mc = Regex.Matches(f, expr);
                // print the CRN and Term-Code for each
                Console.WriteLine("Matches: \n\n");

                Console.WriteLine("CRN: " + mc[0]);
                Console.WriteLine("Term-Code: " + mc[1]);
                // Store CRN and Term-Code in a variable
                int CRN = -1;
                int termCode = -1;
                try
                {
                    CRN = Int32.Parse(mc[0].ToString().Trim());
                    termCode = Int32.Parse(mc[1].ToString().Trim());

                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }



                // call the function which returns the content of 
                // a file as a list and find the size of the csv
                int numOfStudents = LoadCSVFile(f).Count();
                // insert query
                // hard to understnad
                //string insertCourse = "INSERT INTO Course(CRN, Term-Code, NumberOfStudents) " +
                //   "VALUES('" + CRN + "','" + termCode + "','" + numOfStudents + "')";
                // create sqlcommand object
                // easy to understand
                string insertCourse = "INSERT INTO Course(Subject, CRN, TermCode, NumberOfStudents) VALUES(@Subject, @CRN,@TermCode,@NumberOfStudents)";
                SqlCommand cmd = new SqlCommand(insertCourse, conn);
                cmd.Parameters.AddWithValue("@Subject", "Unknown");
                cmd.Parameters.AddWithValue("@CRN", CRN);
                cmd.Parameters.AddWithValue("@TermCode", termCode);
                cmd.Parameters.AddWithValue("@NumberOfStudents", numOfStudents);
                // if the query affects one row
                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("One row inserted");

                }
                // else query has error
                else
                {
                    Console.WriteLine("query error!!");


                }


                Console.WriteLine("\n\n");

            }
        }


        public static void ResidencyCount()
        {

            // if it succeds
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection Succeded\n");
            }
            // otherwise
            else
            {
                Console.WriteLine("Connection Failed\n");
            }



            // call the function and get file names
            fileNames = ReadAllFiles("input");
            // for each file path
            foreach (String f in fileNames)
            {
                // call the function which returns the content of 
                List<String> content = LoadCSVFile(f);
                // count number of residents
                int residentCounter = 0;
                // count number of non-residents
                int nonResidentCounter = 0;
                // count international
                int internationalCounter = 0;

                foreach (String line in content)
                {
                    string status = line.Split(',')[5];
                    // if a student is an Oklahoma resident
                    if (status.Equals("Resident of Oklahoma"))
                    {
                        residentCounter++;
                    }
                    else if (status.Equals("Non-Resident of Oklahoma"))
                    {
                        nonResidentCounter++;
                    }
                    else if (status.Equals("International"))
                    {
                        internationalCounter++;
                    }

                }

                string name1 = "How many students in the cohort are Oklahoma residents?";
                string name2 = "How many students in the cohort are non-residents?";
                string name3 = "How many students in the cohort are international?";


                // find courseid for the given file path
                int courseid = findCourseId(f);


                // now insert a row in the course statistics table(for resident)

                string insertCourseStat1 = "INSERT INTO CourseStatistics(Name, Count, CourseId) VALUES(@name, @count,@courseid)";
                SqlCommand cmd1 = new SqlCommand(insertCourseStat1, conn);
                cmd1.Parameters.AddWithValue("@name", name1);
                cmd1.Parameters.AddWithValue("@count", residentCounter);
                cmd1.Parameters.AddWithValue("@courseid", courseid);

                // if the query affects one row
                if (cmd1.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("One row inserted(course stat)-residents of oklohama");

                }
                // else query has error
                else
                {
                    Console.WriteLine("coursestat query error!!(resident)");


                }

                // insert row for non-residents

                string insertCourseStat2 = "INSERT INTO CourseStatistics(Name, Count, CourseId) VALUES(@name2, @count2,@courseid2)";
                SqlCommand cmd2 = new SqlCommand(insertCourseStat2, conn);
                cmd2.Parameters.AddWithValue("@name2", name2);
                cmd2.Parameters.AddWithValue("@count2", nonResidentCounter);
                cmd2.Parameters.AddWithValue("@courseid2", courseid);

                // if the query affects one row
                if (cmd2.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("One row inserted(course stat)-non residents of oklohama");

                }
                // else query has error
                else
                {
                    Console.WriteLine("coursestat query error!!(nonresident)");


                }

                // insert row for non-residents

                string insertCourseStat3 = "INSERT INTO CourseStatistics(Name, Count, CourseId) VALUES(@name3, @count3,@courseid3)";
                SqlCommand cmd3 = new SqlCommand(insertCourseStat3, conn);
                cmd3.Parameters.AddWithValue("@name3", name3);
                cmd3.Parameters.AddWithValue("@count3", internationalCounter);
                cmd3.Parameters.AddWithValue("@courseid3", courseid);

                // if the query affects one row
                if (cmd3.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("One row inserted(course stat)-international");

                }
                // else query has error
                else
                {
                    Console.WriteLine("coursestat query error!!(international)");

                }



                Console.WriteLine("\n\n");

            }

        }


        // this function will read the Course table
        // and determine the id field of the course given the filepath
        public static int findCourseId(string filepath)
        {
            String expr = @"\d+";
            // apply the regex expression to the filename(or path)
            MatchCollection mc = Regex.Matches(filepath, expr);
            // find the first matches number(with is the CRN)
            int CRN = -1;
            try
            {
                CRN = Int32.Parse(mc[0].ToString().Trim());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }


            string squery = "SELECT Id FROM Course WHERE CRN = '" + CRN + "'";
            // create an adapter
            SqlDataAdapter adp = new SqlDataAdapter(squery, conn);
            // fill a datatable with the returned data
            DataTable dt = new DataTable();
            adp.Fill(dt);
            // this will give the Id that we need
            //dt.Rows[0].ItemArray[0];
            // parse the id into integer
            int id = -1;
            try
            {
                id = Int32.Parse(dt.Rows[0].ItemArray[0].ToString().Trim());
                return id;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }


            return -1;


        }


        static void CohorotMajors()
        {
            // if it succeds
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection Succeded\n");
            }
            // otherwise
            else
            {
                Console.WriteLine("Connection Failed\n");
            }



            // call the function and get file names
            fileNames = ReadAllFiles("input");
            // for each file path
            foreach (String f in fileNames)
            {
                var map = GetMajorCountMap(f);
                
                // find courseid for the given file path
                int courseid = findCourseId(f);

                // ... Loop over the map
                foreach (var pair in map)
                {
                    // the major name
                    string major_name = pair.Key;
                    // how many times that major occured
                    int count = pair.Value;

                    // insert into course statistics table

                    string insertQuery = "INSERT INTO CourseStatistics(Name, Count, CourseId) VALUES(@name, @count,@courseid)";
                    SqlCommand cmdx = new SqlCommand(insertQuery, conn);
                    cmdx.Parameters.AddWithValue("@name", major_name);
                    cmdx.Parameters.AddWithValue("@count", count);
                    cmdx.Parameters.AddWithValue("@courseid", courseid);

                    // if the query affects one row
                    if (cmdx.ExecuteNonQuery() > 0)
                    {
                        // informative display
                        Console.WriteLine("found " + major_name + " " + count, " times for courseid", courseid);
                    }
                    // else query has error
                    else
                    {
                        // if error occurs the following will be displayed
                        Console.WriteLine("unable to count major: ", major_name, " in course " + courseid);
                 
                    }

                    

                }

                
                Console.WriteLine("\n\n");

            }




        }
        static Dictionary<string, int> GetMajorCountMap(string filepath)
        {

            // call the function which returns the content of file 
            List<String> content = LoadCSVFile(filepath);

            // declare dictionary

            var map = new Dictionary<string, int>();

            foreach (String line in content)
            {
                // the 8th element is the major
                // we do a count of the majors in the given file
                string major = line.Split(',')[8];

                int value = -1;
                // if the major is in the map
                if (map.TryGetValue(major, out value))
                {
                    // increment it's value by one
                    map[major] = value + 1;
                }
                // if the name doesn't exist in the map
                else
                {
                    // insert the major as a new element
                    // in the map
                    map[major] = 1;
                }

            }

            return map;

        }
    static void Main(string[] args)
    {
        DoCourseSummary();
        ResidencyCount();
        CohorotMajors();
        conn.Close();
        Console.ReadKey();
    }
}
}
