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
}