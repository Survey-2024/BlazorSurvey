using Microsoft.AspNetCore.Components.Forms;
using SurveyDocUploader.Models.Dtos;

namespace SurveyDocUploader.Services
{
    public interface IBlobService
    {
        Task<ContentDto> GetBlobFile(string name);
        Task UploadBlobFile(IBrowserFile file);
    }
}
