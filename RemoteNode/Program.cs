using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Serilog;
using Shareables;
using static System.Console;

namespace RemoteNode
{
    class Program
    {

        static void Main(string[] args)
        {
            Title = "Remote Node";
            BackgroundColor = ConsoleColor.DarkBlue;
            Log.Logger = LoggerSetup.LogToConsoleAndSeq("http://localhost:5341");

            using (var actorSystem = ActorSystem.Create("RemoteNode"))
            {
                Log.Information("remote Node up");

                ReadKey();
            }



        }
    }
}
