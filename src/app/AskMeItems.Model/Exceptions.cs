using System;

using AskMeItems.Model.Properties;

namespace AskMeItems.Model
{
    public class AnswerNotAllowedException : ArgumentException
    {
        public AnswerNotAllowedException(Item item, string option)
            : base(string.Format(Resources.AnswerNotAllowedForItem, option, item.Code))
        {
        }

        public AnswerNotAllowedException()
            : base(Resources.SpecifyAnswerForItem)
        {
           
        }
    }

    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string code, Exception innerException)
            : base(string.Format(Resources.ItemUsedTwice, code), innerException)
        {
        }
    }

    public class DuplicateAnswerException : Exception
    {
        public DuplicateAnswerException(string itemCode, string answerCode, Exception innerException)
            : base(
                string.Format(Resources.AnswerUsedTwiceForItem, answerCode, itemCode),
                innerException)
        {
        }
    }
}