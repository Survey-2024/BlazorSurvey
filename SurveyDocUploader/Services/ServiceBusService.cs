using Azure.Messaging.ServiceBus;
using BlazorBootstrap;
using Microsoft.Extensions.Options;

namespace SurveyDocUploader.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusClientOptions _clientOptions = new()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;

        private readonly IToastService _toastService;

        public event Action? MessageEventCallback;

        public ServiceBusService(IToastService toastService, IOptions<KeyVaultOptions> options)
        {
            _client = new ServiceBusClient(options.Value.ServiceBusConnStr, _clientOptions);
            _processor = _client.CreateProcessor("surveyqueue", new ServiceBusProcessorOptions());

            // add handler to process messages
            _processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            _processor.ProcessErrorAsync += ErrorHandler;
            _toastService = toastService;
        }

        public Task StartPollServiceBus()
        {
            _processor.StartProcessingAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> StopPollServiceBus()
        {
            await _processor.StopProcessingAsync()!;
            await _processor.DisposeAsync();
            await _client.DisposeAsync();

            return Task.CompletedTask;
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string message = args.Message.Body.ToString();
            //Console.WriteLine($"Message from Service Bus: {message}");

            await _toastService.NewToastMessage(ToastType.Success, message);

            // complete the message. message is deleted from the queue.
            await args.CompleteMessageAsync(args.Message);

            MessageEventCallback?.Invoke();
        }

        // handle any errors when receiving messages
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
