using System;
using Akka.Actor;
using Akka.Actor.Dsl;
using Serilog;
using Shareables;
using Shareables.Messages;

namespace ParentNode
{
    /// <summary>
    /// Parent actor. Manages creating additional actors.
    /// </summary>
    public class RemoteManagerActor : ReceiveActor
    {
        private IActorRef _processActor;
        private IActorRef _dataSourceActor;
        private ILogger _logger;
 private ICancelable _task;
        public RemoteManagerActor(IActorRef processActor)
        {
            _processActor = processActor;
            _dataSourceActor =
                Context.ActorOf(
                    Props.Create(() => new DataSourceActor(100, TimeSpan.FromMilliseconds(300), _processActor)),"DataSource");
            
            _logger = Log.ForContext<RemoteManagerActor>();
            Become(WaitForRemoteActor);
        }

       

        private void WaitForRemoteActor()
        {
            _logger.Information("Waiting...");
            Receive<ChildNodeRunning>(message =>
            {
                Become(StartProcessing);
            });

            //Ping remote actor to check if is available
            _task = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1), _processActor, new IsChildNodeAvailable(), Self);
        }

        private void StartProcessing()
        {
            _logger.Information("Started...");
            _task.Cancel();

            _dataSourceActor.Tell(new StartGeneratingData());


        }
    }
}