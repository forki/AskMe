using System;
using System.Collections.Generic;

namespace AskMe.Model
{
    public class Questionaire
    {
        readonly Dictionary<string, Question> _questions;

        public Questionaire()
        {
            _questions = new Dictionary<string, Question>();
            Questions = new List<Question>();
        }

        public List<Question> Questions { get; private set; }

        public void AddQuestion(Question question)
        {
            try
            {
                _questions.Add(question.Code, question);
            }
            catch (Exception ex)
            {
                throw new DuplicateQuestionException(question.Code, ex);
            }
            Questions.Add(question);
        }
    }
}