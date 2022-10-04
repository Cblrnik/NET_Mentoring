using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using HelloLibrary;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            builder.AddCommandLine(args, new Dictionary<string, string>()
            {
                ["-Name"] = "Name"
            });

            var config = builder.Build();
            string name = config["Name"];

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("What is your name?");
                bool isCheck = true;
                while (isCheck)
                {
                    name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        isCheck = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid name.");
                    }
                }
            }

            Console.WriteLine(Helper.CreateHelloUserString(name));
        }
    }
}
