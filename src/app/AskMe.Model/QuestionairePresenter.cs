using System;

namespace AskMe.Model
{
    public class QuestionairePresenter
    {
        int _answeredQuestions = 0;

        public QuestionairePresenter(Questionaire questionaire)
        {
            Questionaire = questionaire;
        }

        public Questionaire Questionaire { get; private set; }

        public Item CurrentItem
        {
            get { return Questionaire.Items[_answeredQuestions]; }
        }

        public void AnswerCurrentItem(Answer answer)
        {
            _answeredQuestions++;
        }

        public bool HasItem()
        {
            return _answeredQuestions < Questionaire.Items.Count;
        }

        public override string ToString()
        {
            return string.Format("Answered: {0} - {1}", _answeredQuestions, CurrentItem);
        }
    }
}