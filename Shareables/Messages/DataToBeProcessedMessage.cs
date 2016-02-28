namespace Shareables.Messages
{
    public class DataToBeProcessedMessage
    {
        public int Id { get; }
        public string Data { get; }

        public DataToBeProcessedMessage(int id, string data)
        {
            Id = id;
            Data = data;
        }
    }
}