using Akka.Actor;
using Serilog;
using Shareables.Messages;

namespace Shareables
{
    public class LogProcessedDataActor : ReceiveActor
    {
        private ILogger _logger;
        public LogProcessedDataActor()
        {
            _logger = Log.ForContext<LogProcessedDataActor>();

            Receive<DataProcessedMessage>(message =>
            {
                _logger.Information("Id: {id} {originalData} -> {updatedData}",message.Id, message.OriginalData, message.UpdatedData);
            });
        }
    }
}