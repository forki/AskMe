using System.Collections.Generic;

namespace AskMe.Model
{
    public class Questionaire
    {
        public Questionaire()
        {
            Questions = new List<Question>();
        }

        public List<Question> Questions { get; private set; }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }
}