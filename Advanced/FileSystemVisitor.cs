using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advanced
{
    public class FileSystemVisitor
    {
        private readonly Predicate<string> filter;
        private readonly string startFolder;
        private DirectoryInfo directoryInfo;
        private bool isFirstDirectory;
        private readonly bool isFiltered;
        public event Notifier Notificator;
        public event FileFound FileFound;
        public event DirectoryFound DirectoryFound;
        public event FilteredFileFound FilteredFileFound;
        public event FilteredDirectoryFound FilteredDirectoryFound;
        public List<string> excludedNames = new List<string>();

        public FileSystemVisitor(string startFolder)
        {
            this.startFolder = startFolder ?? throw new ArgumentNullException(nameof(startFolder));
            this.CreateDirectory();
            this.isFirstDirectory = true;
            this.isFiltered = false;
        }

        public FileSystemVisitor(string startFolder, Predicate<string> filter)
        {
            this.startFolder = startFolder ?? throw new ArgumentNullException(nameof(startFolder));
            this.filter = filter ?? throw new ArgumentNullException(nameof(filter));
            this.CreateDirectory();
            this.isFirstDirectory = true;
            this.isFiltered = true;
        }

        public void StartSearch()
        {
            this.Notificator?.Invoke("Start");
            this.Search(this.directoryInfo, 0);
            if (isFiltered)
            {
                Console.WriteLine("----------------------------------------------Filtered----------------------------------------------");
                this.FilteredSearch(this.directoryInfo, 0);
            }

            this.Notificator?.Invoke("Finish");
        }

        private void FilteredSearch(DirectoryInfo info, int tabsCount)
        {
            this.FilteredDirectoryFound?.Invoke(info.Name);
            string tabs = string.Empty;
            for (int i = 0; i < tabsCount; i++)
            {
                tabs += "  ";
            }
            if (this.isNeedToPrint(info.Name))
            {
                Console.WriteLine(tabs + info.Name + "/");
                var directories = info.GetDirectories();
                directoryOutput(info);
                this.isFirstDirectory = false;
                foreach (var directory in directories)
                {
                    this.FilteredSearch(directory, tabsCount + 1);
                }
            }

            void directoryOutput(DirectoryInfo directory)
            {
                var files = directory.GetFiles();
                foreach (var file in files)
                {
                    if (this.isNeedToPrint(file.Name))
                    {
                        this.FilteredFileFound?.Invoke(file.Name);
                        Console.WriteLine("  " + tabs + file.Name);
                    }
                }
            }
        }

        private void Search(DirectoryInfo info, int tabsCount)
        {
            this.DirectoryFound?.Invoke(info.Name);
            string tabs = string.Empty;
            for (int i = 0; i < tabsCount; i++)
            {
                tabs += "  ";
            }

            Console.WriteLine(tabs + info.Name + "/");
            var directories = info.GetDirectories();
            directoryOutput(info);
            foreach (var directory in directories)
            {
                this.Search(directory, tabsCount + 1);
            }

            void directoryOutput(DirectoryInfo directory)
            {
                var files = directory.GetFiles();
                foreach (var file in files)
                {
                    this.FileFound?.Invoke(file.Name);
                    Console.WriteLine("  " + tabs + file.Name);
                }
            }
        }

        public void StopSearch()
        {
            this.Notificator?.Invoke("Finish");
            Environment.Exit(0);
        }

        private bool CheckByName(string name)
        {
            return this.filter?.Invoke(name) ?? true;
        }

        private bool isNeedToPrint(string name)
        {
            return !this.excludedNames.Contains(name) && (this.CheckByName(name) || this.isFirstDirectory);
        }

        private void CreateDirectory()
        {
            if (Directory.Exists(this.startFolder))
            {
                this.directoryInfo = new DirectoryInfo(startFolder);
            }
            else
            {
                throw new ArgumentException("There is no such directory");
            }
        }
    }
}
