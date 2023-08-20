namespace CommentsService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}