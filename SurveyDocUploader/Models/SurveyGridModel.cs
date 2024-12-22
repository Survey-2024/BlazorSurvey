namespace SurveyDocUploader.Models
{
    public class SurveyGridModel
    {
        public int SurveyId { get; set; }
        public string? SurveyOrigin { get; set; }
        public string? SurveyStatus { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
