namespace SurveyDocUploader.Services
{
    public interface IServiceBusService
    {
        Task StartPollServiceBus();
        Task<Task> StopPollServiceBus();
        event Action MessageEventCallback;
    }
}
