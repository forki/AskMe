using System;

namespace AskMe.Model
{
    public class QuestionnairePresenter
    {
        int _answeredQuestions = 0;

        public QuestionnairePresenter(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public Questionnaire Questionnaire { get; private set; }

        public Item CurrentItem
        {
            get { return Questionnaire.Items[_answeredQuestions]; }
        }

        public void AnswerCurrentItem(Answer answer)
        {
            _answeredQuestions++;
        }

        public bool HasItem()
        {
            return _answeredQuestions < Questionnaire.Items.Count;
        }

        public override string ToString()
        {
            return string.Format("Answered: {0} - {1}", _answeredQuestions, CurrentItem);
        }
    }
}