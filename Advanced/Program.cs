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
            FileSystemVisitor file = new FileSystemVisitor("D:\\Temp", (name) => name.Contains("open"));
            #region Examples
            file.Notificator += Console.WriteLine;
            file.FilteredFileFound += (name) =>
            {
                if (name.StartsWith("s"))
                {
                    Console.WriteLine("Some Action");
                }
            };
            file.FileFound += (name) =>
            {
                if (name.Contains(".html"))
                {
                    file.ExcludedNames.Add(name);
                }
            };

            file.FilteredDirectoryFound += (name) =>
            {
                if (name.Contains("services"))
                {
                    file.StopSearch();
                }
            };
            #endregion
            file.StartSearch();
        }
    }
}
