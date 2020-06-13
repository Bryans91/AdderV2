using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Adder.IO
{
    public class FileReader
    {

        private String _pathString;
        public string PathString { get => _pathString; set => _pathString = value; }

        public FileReader(String pathString)
        {
            PathString = pathString;
        }

        public List<String> GetLines()
        {
            if(IsValidFile())
            {
                return ReadLines();
            }
            else
            {
                Console.WriteLine("File does not exist or has wrong extension (needs to be .txt).");

                return null;
            }
        }

        // Check if filepath is not empty, ends with .txt and exists.
        private bool IsValidFile()
        {
            try
            {
                Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "" + PathString));
                return ! String.IsNullOrEmpty(PathString) && PathString.ToLower().EndsWith(".txt") && File.Exists(Path.Combine(Environment.CurrentDirectory, "" + PathString));
            }
            catch (IOException e)
            {
                Console.Write(e.StackTrace);

                return false;
            }
        }

        private List<String> ReadLines()
        {
            List<String> list = new List<String>();

            try
            {
                string[] lines = System.IO.File.ReadAllLines(PathString);
                foreach (string line in lines)
                {
                    list.Add(line);
                }

            }
            catch (IOException e)
            {
                Console.Write(e.StackTrace);
            }

            return list;
        }
    }
}
