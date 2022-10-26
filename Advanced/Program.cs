using System;

namespace Advanced
{
    public delegate void Notifier(string step);
    public delegate void FileFound(string name);
    public delegate void DirectoryFound(string name);
    public delegate void FilteredFileFound(string name);
    public delegate void FilteredDirectoryFound(string name);
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemVisitor visitor = new FileSystemVisitor("D:\\Temp", (name) => name.Contains("open"));
            #region Examples
            visitor.Notificator += (str) =>
            {
                Console.WriteLine(str);
                if (str.Contains("Stop"))
                {
                    Environment.Exit(0);
                }
            };
            visitor.FilteredFileFound += (name) =>
            {
                if (name.StartsWith("s"))
                {
                    Console.WriteLine("Some Action");
                }
            };
            visitor.FileFound += (name) =>
            {
                if (name.Contains(".html"))
                {
                    visitor.ExcludedNames.Add(name);
                }
            };

            visitor.FilteredDirectoryFound += (name) =>
            {
                if (name.Contains("services"))
                {
                    visitor.StopSearch();
                }
            };
            #endregion
            visitor.StartSearch();
        }
    }
}
