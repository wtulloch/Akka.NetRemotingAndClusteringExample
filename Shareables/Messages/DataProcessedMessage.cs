namespace Shareables.Messages
{
    public class DataProcessedMessage
    {
     
        public int Id { get; } 
        public string OriginalData { get; }

        public string UpdatedData { get; }

        public DataProcessedMessage(int id, string originalData, string updatedData)
        {
            Id = id;
            OriginalData = originalData;
            UpdatedData = updatedData;
        }
    }
}