using Akka.Actor;
using Serilog;
using Shareables.Messages;

namespace Shareables
{
    public class SampleProcessActor : ReceiveActor
    {
        private readonly IActorRef _responseActor;
        private ILogger _logger;
        protected override void PreStart()
        {
            Log.Information("Prestart for {actor}", Self.Path);
        }

        public SampleProcessActor(IActorRef responseActor)
        {
            _responseActor = responseActor;
            _logger = Log.ForContext<SampleProcessActor>();
            Receive<IsChildNodeAvailable>(message => Sender.Tell(new ChildNodeRunning()));
            Receive<DataToBeProcessedMessage>(message =>
            {
                _logger.Information("Received : {@message}", message);

                var updatedData = message.Data.ToUpper();

                _responseActor.Tell(new DataProcessedMessage(message.Id, message.Data, updatedData));

            });

        }
         
    }
}