using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{

    public class NamedObject
    {
        public NamedObject(string name) //constructor. Every NamedObject required name
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
    }
    public abstract class Book : NamedObject, IBook //abstract class
    {
        public Book(string name) : base(name)
        {
        }

        public abstract void AddGrade(double grade); //abstract method - don't know what this implementaion sb

        public abstract Statistics GetStatistics();

    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            grades = new List<double>();
        }

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
            }
            //writer.Dispose();
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            // var fileGrades = File.ReadLines($"{Name}.txt");
            // foreach (var grade in fileGrades)
            // {
            //     result.Add(double.Parse(grade));
            // }
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    result.Add(double.Parse(line));
                    line = reader.ReadLine();
                }
            }
            
            return result;
        }
        private List<double> grades; //adding public in front would allow access outside of this code
    }
    public class InMemoryBook : Book 
    {
        //constructor:   use the name consctructor for Book for base class too
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            //this.Name = name; //this.name is the name below (this object), name is incoming
            Name = name;
        }

        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;

            }
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade > 0)  // || is or
            {
                grades.Add(grade);
            }
            else {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override Statistics GetStatistics() //override implmentation that's in the base class
        {
            var result = new Statistics();

            for(int index = 0; index < grades.Count; index += 1)
            {
                result.Add(grades[index]);
            };

            return result;
        }

        private List<double> grades; //adding public in front would allow access outside of this code

        // public string Name //property
        // {
        //     get; set; //shortcut! Autoproperty
            //private set; //can't write outside of Book.cs -> only write with constructor

            // get //control when reading this property
            // {
            //     return name; //could return uppercase, e.g.
            // }
            // set
            // {
            //     if (!String.IsNullOrEmpty(value))
            //     {
            //         name = value; //implicit variable value is incoming value someone is writing to property
            //     }
                
            // }
        //}
        //private string name; //uppercase Name would denote public

        //readonly string category = "Science"; //can only write to readonly in the constructor
        //public const string CATEGORY = "Math"; //can never change this, even in constructor



    }
}