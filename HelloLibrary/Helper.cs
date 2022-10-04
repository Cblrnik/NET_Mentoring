using System;

namespace HelloLibrary
{
    public class Helper
    {
        public static string CreateHelloUserString(string name)
        {
            return $"{DateTime.Now} Hello {name}!";
        }
    }
}
