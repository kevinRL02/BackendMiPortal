using OrderService.EventModels;


namespace OrderService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewOrder(OrderCreatedEvent orderCreatedEvent);
    }
}