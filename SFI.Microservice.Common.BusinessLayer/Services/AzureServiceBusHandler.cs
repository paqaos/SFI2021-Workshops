using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using SFI.Microservice.Common.BusinessLayer.CommandStack.EventHandlers;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Events;
using SimpleInjector;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public class AzureServiceBusHandler : IAzureServiceBusHandler, IDisposable
    {
        private Container _container;
        private string _topicName;
        private string _connectionString;
        private string _subscriptionName;
        private IEventDeserializer _eventDeserializer;
        private ServiceBusProcessor processor;
        private ServiceBusClient _client;

        public AzureServiceBusHandler(Container container, string connectionString, string topicName, IEventDeserializer eventDeserializer, string subscriptionName)
        {
            _container = container;
            _connectionString = connectionString;
            _topicName = topicName;
            _eventDeserializer = eventDeserializer;
            _subscriptionName = subscriptionName;
        }

        /// <inheritdoc />
        public async Task Initialize()
        {
            _client = new ServiceBusClient(_connectionString);

            processor = _client.CreateProcessor(_topicName, _subscriptionName, new ServiceBusProcessorOptions());

            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs arg)
        {
            string body = arg.Message.Body.ToString();

            var preparsed = arg.Message.Body.ToObjectFromJson<EventBase>();

            var parsed = _eventDeserializer.DeserializeEvent(preparsed.Name, body);

            if (parsed != null)
            {
                await HandleEvent(parsed);
            }

            // complete the message. messages is deleted from the queue. 
            await arg.CompleteMessageAsync(arg.Message);
        }

        private async Task HandleEvent<T>(T @event) where T : EventBase
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = _container.GetAllInstances(handlerType);
            foreach (dynamic handler in handlers)
            {
                await handler.HandleAsync((dynamic)@event);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task SendEvent <T>(T eventData) where T : EventBase
        {
            await using (ServiceBusClient client = new ServiceBusClient(_connectionString))
            {
                // create a sender for the topic
                ServiceBusSender sender = client.CreateSender(_topicName);
                var content = JsonSerializer.Serialize(eventData);
                await sender.SendMessageAsync(new ServiceBusMessage(content));
                Console.WriteLine($"Sent a single message to the topic: {_topicName}");
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            processor.StopProcessingAsync().Wait();
            _client.DisposeAsync();
        }
    }
}
