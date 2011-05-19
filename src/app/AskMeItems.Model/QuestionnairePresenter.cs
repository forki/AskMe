using System.Collections.Generic;

namespace AskMeItems.Model
{
    public class QuestionnairePresenter
    {
        public QuestionnairePresenter(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
            Answers = new List<Answer>();
        }

        public Questionnaire Questionnaire { get; private set; }

        public Item CurrentItem
        {
            get { return Questionnaire.Items[Answers.Count]; }
        }

        public List<Answer> Answers { get; private set; }

        public void AnswerCurrentItem(Answer answer)
        {
            Answers.Add(answer);
        }

        public bool HasItem()
        {
            return Answers.Count < Questionnaire.Items.Count;
        }

        public override string ToString()
        {
            return string.Format("Answered: {0} - {1}", Answers.Count, CurrentItem);
        }
    }
}