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

    public class DuplicateQuestionException : Exception
    {
        public DuplicateQuestionException(string code, Exception innerException)
            : base(string.Format("The question {0} was used twice.", code), innerException)
        {
        }
    }

    public class DuplicateAnswerException : Exception
    {
        public DuplicateAnswerException(string questionCode, string answerCode, Exception innerException)
            : base(
                string.Format("The answer {0} was used twice in question {1}.", answerCode, questionCode),
                innerException)
        {
        }
    }
}