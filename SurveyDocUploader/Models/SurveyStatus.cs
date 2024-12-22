namespace SurveyDocUploader.Models;

public partial class SurveyStatus
{
    public int SurveyStatusId { get; set; }

    public string? SurveyStatus1 { get; set; }

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
