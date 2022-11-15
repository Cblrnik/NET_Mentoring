using System;
namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var temp = new ConfigurationComponentBase();
            temp.LoadSettings();

            temp.SecretString = "Secret value";
            temp.SecretNumber = 24;
            temp.SecretFloat = 985901.7684f;
            temp.SecretTimeSpan = new TimeSpan(12,25,14);

            temp.SaveSettings();
        }
    }
}
