using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Calculator
{
   public static class History
    {
        public static string Get()
        {
            string resut = string.Empty;
            using (StreamReader reader = new StreamReader(File.OpenRead(@"\History.txt")))
            {
                resut = reader.ReadToEnd();
            }
            return resut;
        }

        public static void Add(string inputString)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(@"\History.txt")))
            {
                writer.WriteLine(inputString);
            }
        }

        public static void Clear()
        {

            using (File.Create(@"\History.txt"))
            {

            }
            
        }
    }
}
