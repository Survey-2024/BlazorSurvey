namespace SurveyDocUploader.Models;

public partial class SurveyType
{
    public int SurveyTypeId { get; set; }

    public string? Origin { get; set; }

    public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
