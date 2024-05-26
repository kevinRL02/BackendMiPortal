using System;
using System.Text.Json;
using AutoMapper;
using ShoppingCartService.Data;
using ShoppingCartService.Models;
using ShoppingCartService.EventModels;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingCartService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.OrderCreated:
                    ClearShoppingCart(message);
                    break;
                default:
                    break;
            }

        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Order_Published":
                    Console.WriteLine("--> Order Published Event Detected");
                    return EventType.OrderCreated;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void ClearShoppingCart(string orderCreatedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IShoppingCartRepo>();

                var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderCreatedMessage, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });

                if (orderCreatedEvent != null)
                {
                    var items = repo.GetItemsByCartId(orderCreatedEvent.ShoppingCartId); // Assuming UserId is used as CartId

                    foreach (var item in items)
                    {
                        repo.RemoveItemFromCart(item);
                    }

                    repo.SaveChanges();
                    Console.WriteLine("--> Shopping cart cleared after order creation");
                }
                else
                {
                    Console.WriteLine("--> OrderCreatedEvent is null, cannot clear shopping cart");
                }
            }
        }
    }

    enum EventType
    {
        OrderCreated,
        Undetermined
    }
}