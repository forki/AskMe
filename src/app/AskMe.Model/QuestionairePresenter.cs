namespace AskMe.Model
{
    public class QuestionairePresenter
    {
        int _answeredQuestions;

        public QuestionairePresenter(Questionaire questionaire)
        {
            Questionaire = questionaire;
        }

        public Questionaire Questionaire { get; private set; }

        public Item AskNextItem()
        {
            return Questionaire.Items[_answeredQuestions++];
        }
    }
}