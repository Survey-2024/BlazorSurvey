using BlazorBootstrap;

namespace SurveyDocUploader.Services
{
    public interface IToastService
    {
        List<ToastMessage> Messages { get; set; }
        public Task NewToastMessage(ToastType toastType, string message);
    }
}
