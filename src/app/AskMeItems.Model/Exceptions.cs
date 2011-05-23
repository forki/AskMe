using System;

namespace AskMeItems.Model
{
    public class AnswerNotAllowedException : ArgumentException
    {
        public AnswerNotAllowedException(Item item, string option)
            : base(string.Format("The answer {0} is not allowed for item {1}.", option, item.Code))
        {
        }

        public AnswerNotAllowedException(Item item)
            : base(string.Format("You have to specify an answer for item {0}.", item.Code))
        {
           
        }
    }

    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string code, Exception innerException)
            : base(string.Format("The item {0} was used twice.", code), innerException)
        {
        }
    }

    public class DuplicateAnswerException : Exception
    {
        public DuplicateAnswerException(string itemCode, string answerCode, Exception innerException)
            : base(
                string.Format("The answer {0} was used twice in item {1}.", answerCode, itemCode),
                innerException)
        {
        }
    }
}