using System;

namespace FizzBuzz
{
    public static class FizzBuzz
    {
        public static void PrintAllNumbers()
        {
            for (int i = 1; i < 101; i++)
            {
                Console.WriteLine(GetFizzOrBuzz(i));
            }
        }

        public static string GetFizzOrBuzz(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                return "FizzBuzz";
            }
            else if (number % 3 == 0)
            {
                return "Fizz";
            }
            else if (number % 5 == 0)
            {
                return "Buzz";
            }
            else
            {
                return number.ToString();
            }
        }

        public static bool IsInRange(int number)
        {
            return number > 0 && number <= 100;
        }
    }
}
