using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace OOP_Fundamentals.Dao
{
    public static class LoaderService
    {
        public static List<T> Load<T>(Type type)
        {
            var path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(path).Select(fileName => fileName.Replace(path + "\\", string.Empty)).Where(f => f.StartsWith(type.Name)).ToList();
            var outList = new List<T>();
            foreach (var file in files)
            {
                var jsonString = File.ReadAllText(file);
                outList.Add(JsonSerializer.Deserialize<T>(jsonString));
            }

            return outList;
        }
    }
}
