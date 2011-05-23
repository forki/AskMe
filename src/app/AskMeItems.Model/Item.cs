using System;
using System.Collections.Generic;

namespace AskMeItems.Model
{
    public class Item
    {
        public Item(string code, string text, IEnumerable<Answer> answers)
        {
            Code = code;
            Text = text;

            AddAnswers(answers);
        }

        public string Text { get; private set; }

        public Dictionary<string, Answer> Answers { get; private set; }

        public string Code { get; private set; }

        void AddAnswers(IEnumerable<Answer> answers)
        {
            Answers = new Dictionary<string, Answer>();
            foreach (var answer in answers)
                try
                {
                    Answers.Add(answer.Code, answer);
                }
                catch (Exception ex)
                {
                    throw new DuplicateAnswerException(Code, answer.Code, ex);
                }
        }

        public Result AnswerWith(string answerCode)
        {
            Answer selectedAnswer;
            if (Answers.TryGetValue(answerCode, out selectedAnswer))
                return new Result(this, selectedAnswer);
            throw new AnswerNotAllowedException(this, answerCode);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Code, Text);
        }
    }
}