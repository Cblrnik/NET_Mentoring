using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string line = Console.ReadLine();

                Console.WriteLine(line[0]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Your string is empty");
            }
        }
    }
}