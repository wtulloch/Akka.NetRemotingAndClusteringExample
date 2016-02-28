using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Shareables;
using static System.Console;

namespace SampleSeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            Title = "Seed Node 1";
            Log.Logger = LoggerSetup.LogToConsoleAndSeq("http://localhost:5341");

            Log.Information("Seed Node 1 has started");

            ReadKey();
            Log.Information("Seed Node 1 has stopped");

        }
    }
}
