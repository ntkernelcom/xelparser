using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.XEvent.XELite;

namespace XelReader
{
    class Program
    {
        internal class XELFileParser
        {
            public string InputFilePath { get; }

            public XELFileParser(string filePath)
            {
                InputFilePath = filePath;
            }

            public void Process()
            {

                if (!File.Exists(InputFilePath))
                {
                    return;
                }

                string inputFileName = Path.GetFileName(InputFilePath);
                Console.WriteLine($"{inputFileName}");
                string extension = Path.GetExtension(InputFilePath);

                var xeStream = new XEFileEventStreamer(InputFilePath);

                xeStream.ReadEventStream(

                    xevent =>
                    {
                        Console.WriteLine(xevent);
                        Console.WriteLine("");
                        return Task.CompletedTask;
                    },
                    CancellationToken.None).Wait();
            }
        }


        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: XelReader file_name");
                return;
            }
            var xel = new XELFileParser(args[0]);
            xel.Process();
        }
    }
}
