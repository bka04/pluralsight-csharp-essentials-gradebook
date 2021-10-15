using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Scott's Grade Book");
            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The letter grade is {stats.Letter}");

            //double[] numbers = new double[] {12.7, 10.3, 6.11};
            //var numbers = new[] {12.7, 10.3, 6.11, 4.1};

            // var grades = new List<double>() {12.7, 10.3, 6.11, 4.1};
            // grades.Add(56.1);


            // if(args.Length > 0) {
            //     Console.WriteLine($"Hello {args[0]}!");
            // }
            // else {
            //     Console.WriteLine("Hello!");
            //}

            // var p = new Program();
            // Program.Main(args); //p.Main(args) would error. of course, now we'll infinite loop

        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade. Enter X when done.");
                var input = Console.ReadLine();
                if (input == "X")
                {
                    break;
                }
                try
                {
                    double grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex) //can safely catch an invalid grade
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex) //can safely catch invalid format on the double.Parse
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    //always process this -> success or if it throws an exception
                }

            }
        }
    }
}

