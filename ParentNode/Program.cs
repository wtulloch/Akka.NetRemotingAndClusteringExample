using Akka.Actor;
using Serilog;
using static System.Console;
using Shareables;

namespace ParentNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Title = "Parent node - remoting";

            Log.Logger = LoggerSetup.LogToConsoleAndSeq("http://localhost:5341");
            Log.Information("Start parent node");
            using (var actorSystem = ActorSystem.Create("remoteExample"))
            {
                var logProcessedActor = actorSystem.ActorOf(Props.Create<LogProcessedDataActor>(), "LogProcessed");
                var processActor = actorSystem.ActorOf(Props.Create(() => new SampleProcessActor(logProcessedActor)),"ProcessActor");
                var remoteManager = actorSystem.ActorOf(Props.Create(() => new RemoteManagerActor(processActor)),"RemoteManager");

                ReadKey();
            }
        }
    }
}
