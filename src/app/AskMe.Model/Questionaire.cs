using System;
using System.Collections.Generic;

namespace AskMe.Model
{
    public class Questionaire
    {
        readonly Dictionary<string, Question> _questions;

        public Questionaire(IEnumerable<Question> questions)
        {
            Questions = new List<Question>();
            _questions = new Dictionary<string, Question>();
            foreach (var question in questions)
                AddQuestion(question);
        }

        public List<Question> Questions { get; private set; }

        void AddQuestion(Question question)
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