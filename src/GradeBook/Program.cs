﻿using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            var book = new Book("Al's Grade Book");
            book.GradeAdded += OnGradeAdded;


            while(true)
            {   
                Console.WriteLine("Enter A Grade or 'q' to quit");
                var input = Console.ReadLine();

                if (input.Equals("q"))
                    {
                        break;
                    }
                    try
                    {
                        var grade = double.Parse(input);
                        book.AddGrade(grade);
                    
                    }
                    catch(ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        
                    }
                    catch(FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    finally
                    {
                        Console.WriteLine("**");
                    }

            } 


            var stats = book.GetStattistics();

            Console.WriteLine(Book.CATEGORY);
            Console.WriteLine($"For the Book named {book.Name}");
            Console.WriteLine($"The Lowest Grade is {stats.Low}");
            Console.WriteLine($"The Highest Grade is {stats.High}");
            Console.WriteLine($"The Average Grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");

        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A Grade was Added");
        }
    }
}
