namespace SurveyDocUploader.Models;

public partial class Survey
{
    public int SurveyId { get; set; }

    public int SurveyTypeId { get; set; }

    public int SurveyStatusId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    //public virtual ICollection<AnswersInSurvey> AnswersInSurveys { get; set; } = new List<AnswersInSurvey>();

    public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; } = new List<SurveyAnswer>();

    public virtual SurveyStatus SurveyStatus { get; set; } = null!;

    public virtual SurveyType SurveyType { get; set; } = null!;
}
