using movie_client.Models;
using System;

namespace movie_client
{
    class Program
    {
        private static string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        static void search()
        {
            Console.WriteLine("Enter parameters or press enter to skip");
            string title = GetInput("title:");
            string movie_id = GetInput("movie id:");
            string year = GetInput("year:");

            DBConnect.querySelect(title, movie_id, year);
        }

        static void delete()
        {
            Console.WriteLine("Enter parameters or press enter to skip");
            string title = GetInput("title:");
            string movie_id = GetInput("movie id:");
            string year = GetInput("year:");

            DBConnect.queryDelete(title, movie_id, year);
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Contains("-delete"))
                {
                    delete();
                }
                if (args.Contains("-search"))
                {
                    search();
                }
                if (args.Contains("-all"))
                {
                    DBConnect.querySelectAll();
                }
                if (args.Contains("-help") || args.Contains("--help"))
                {
                    Console.WriteLine("Try the following: -delete, -search, -all");
                }
            }
            else
            {
                Console.WriteLine("Please provide arguments");
            }
        }
    }
}