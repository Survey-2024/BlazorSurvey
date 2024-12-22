using BlazorBootstrap;

namespace SurveyDocUploader.Services
{
    public class ToastService : IToastService
    {
        public List<ToastMessage> Messages { get; set; } = [];

        public async Task NewToastMessage(ToastType toastType, string message)
        {
            Messages.Add(await CreateToastMessage(toastType, message));
        }

        private static async Task<ToastMessage> CreateToastMessage(ToastType toastType, string message)
        {
            var toastMessage = new ToastMessage
            {
                Title = "Notice",
                Type = toastType,
                Message = message
            };

            return await Task.FromResult(toastMessage);
        }
    }
}
