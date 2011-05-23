namespace AskMeItems.Model
{
    public static class QuestionaireExtensions
    {
        public static QuestionnairePresenter ToPresenter(this Questionnaire questionnaire)
        {
            return new QuestionnairePresenter(questionnaire);
        }
    }
}