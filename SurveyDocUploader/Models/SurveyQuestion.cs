namespace SurveyDocUploader.Models;

public partial class SurveyQuestion
{
    public int SurveyQuestionId { get; set; }

    public int SurveyTypeId { get; set; }

    public string QuestionText { get; set; } = null!;

    public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; } = new List<SurveyAnswer>();

    public virtual SurveyType SurveyType { get; set; } = null!;
}
