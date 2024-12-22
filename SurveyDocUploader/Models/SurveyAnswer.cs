namespace SurveyDocUploader.Models;

public partial class SurveyAnswer
{
    public int SurveyAnswerId { get; set; }

    public int SurveyQuestionId { get; set; }

    public int SurveyId { get; set; }

    public string AnswerText { get; set; } = null!;

    public virtual ICollection<AnswersInSurvey> AnswersInSurveys { get; set; } = new List<AnswersInSurvey>();

    public virtual Survey Survey { get; set; } = null!;

    public virtual SurveyQuestion SurveyQuestion { get; set; } = null!;
}
