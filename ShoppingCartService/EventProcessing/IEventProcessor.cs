namespace ShoppingCartService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}