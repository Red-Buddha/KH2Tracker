using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhTracker
{
    public class Log
    {
        string path;
        StreamWriter writer;

        public Log(string Path)
        {
            path = Path;

            writer = new StreamWriter(path);
        }

        public void Close()
        {
            writer.Close();
        }

        public void Record(string text)
        {
            writer.WriteLine(text);
        }

        public void RecordWorld(string text)
        {
            writer.WriteLine("");
            writer.WriteLine("Entered " + text);
            writer.WriteLine("----------------------");
        }
    }
}
