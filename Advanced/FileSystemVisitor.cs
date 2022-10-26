using System;
using System.Collections.Generic;
using System.IO;

namespace Advanced
{
    public class FileSystemVisitor
    {
        private readonly Predicate<string> _filter;
        private readonly string _startFolder;
        private DirectoryInfo _directoryInfo;
        private bool _isFirstDirectory;
        private readonly bool _isFiltered;
        public event Notifier Notificator;
        public event FileFound FileFound;
        public event DirectoryFound DirectoryFound;
        public event FilteredFileFound FilteredFileFound;
        public event FilteredDirectoryFound FilteredDirectoryFound;
        public List<string> ExcludedNames = new List<string>();

        public FileSystemVisitor(string startFolder)
        {
            this._startFolder = startFolder ?? throw new ArgumentNullException(nameof(startFolder));
            CreateDirectory();
            _isFirstDirectory = true;
            _isFiltered = false;
        }

        public FileSystemVisitor(string startFolder, Predicate<string> filter)
        {
            this._startFolder = startFolder ?? throw new ArgumentNullException(nameof(startFolder));
            this._filter = filter ?? throw new ArgumentNullException(nameof(filter));
            CreateDirectory();
            _isFirstDirectory = true;
            _isFiltered = true;
        }

        public void StartSearch()
        {
            Notificator?.Invoke("Start");
            Search(_directoryInfo, 0);
            if (_isFiltered)
            {
                Console.WriteLine(
                    "----------------------------------------------Filtered----------------------------------------------");
                FilteredSearch(_directoryInfo, 0);
            }

            Notificator?.Invoke("Finish");
        }

        private void FilteredSearch(DirectoryInfo info, int tabsCount)
        {
            FilteredDirectoryFound?.Invoke(info.Name);
            var tabs = string.Empty;
            for (var i = 0; i < tabsCount; i++) tabs += "  ";
            if (IsNeedToPrint(info.Name))
            {
                Console.WriteLine(tabs + info.Name + "/");
                var directories = info.GetDirectories();
                DirectoryOutput(info);
                _isFirstDirectory = false;
                foreach (var directory in directories) FilteredSearch(directory, tabsCount + 1);
            }

            void DirectoryOutput(DirectoryInfo directory)
            {
                var files = directory.GetFiles();
                foreach (var file in files)
                    if (IsNeedToPrint(file.Name))
                    {
                        FilteredFileFound?.Invoke(file.Name);
                        Console.WriteLine("  " + tabs + file.Name);
                    }
            }
        }

        private void Search(DirectoryInfo info, int tabsCount)
        {
            DirectoryFound?.Invoke(info.Name);
            var tabs = string.Empty;
            for (var i = 0; i < tabsCount; i++) tabs += "  ";

            Console.WriteLine(tabs + info.Name + "/");
            var directories = info.GetDirectories();
            DirectoryOutput(info);
            foreach (var directory in directories) Search(directory, tabsCount + 1);

            void DirectoryOutput(DirectoryInfo directory)
            {
                var files = directory.GetFiles();
                foreach (var file in files)
                {
                    FileFound?.Invoke(file.Name);
                    Console.WriteLine("  " + tabs + file.Name);
                }
            }
        }

        public void StopSearch()
        {
            Notificator?.Invoke("Finish");
            Environment.Exit(0);
        }

        private bool CheckByName(string name)
        {
            return this._filter?.Invoke(name) ?? true;
        }

        private bool IsNeedToPrint(string name)
        {
            return !ExcludedNames.Contains(name) && (CheckByName(name) || _isFirstDirectory);
        }

        private void CreateDirectory()
        {
            if (Directory.Exists(_startFolder))
                _directoryInfo = new DirectoryInfo(_startFolder);
            else
                throw new ArgumentException("There is no such directory");
        }
    }
}