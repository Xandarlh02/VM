using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public static class FileReader
    {
        private static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File {path} not found.");
            }

            return File.ReadAllText(path);
        }

        private static string RemoveCommentsAndSpaces(string text)
        {
            var lines = text.Split('\n')
                            .Where(line => !line.TrimStart().StartsWith("//"))  // Remove lines starting with comments
                            .Select(line => line.Contains("//")
                                            ? line.Substring(0, line.IndexOf("//")).Trim() // Remove inline comments and trim spaces 
                                            : line.Trim())
                            .Where(line => !string.IsNullOrWhiteSpace(line));  // Remove empty lines

            return string.Join("\n", lines);
        }
        public static string ProcessFile(string path)
        {
            var pureFile = ReadFile(path);
            return RemoveCommentsAndSpaces(pureFile);
        }
    }
}
