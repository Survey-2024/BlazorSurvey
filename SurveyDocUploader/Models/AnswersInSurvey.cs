namespace SurveyDocUploader.Models;

public partial class AnswersInSurvey
{
    public int AnswersInSurveysId { get; set; }

    public int SurveyAnswerId { get; set; }

    public int SurveyId { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public virtual SurveyAnswer SurveyAnswer { get; set; } = null!;
}
