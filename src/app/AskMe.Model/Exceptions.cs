using System;

namespace AskMe.Model
{
    public class AnswerNotAllowedException : Exception
    {
        public AnswerNotAllowedException(string option)
            : base(string.Format("The answer {0} is not allowed.", option))
        {
        }
    }

    public class QuestionException : Exception
    {
        public QuestionException(string code, Exception innerException)
            : base(string.Format("The question {0} was used twice.", code), innerException)
        {
        }
    }
}