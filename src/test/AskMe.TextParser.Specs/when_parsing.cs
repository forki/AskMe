using System.Collections.Generic;
using System.Linq;
using AskMe.Model;
using Machine.Specifications;

namespace AskMe.TextParser.Specs
{
    public class when_parsing
    {
        protected static string Text;
        static readonly QuestionaireParser QuestionaireParser = new QuestionaireParser();
        protected static Questionaire Questionaire;

        Because of = () => Questionaire = QuestionaireParser.Parse(Text);

        public static Answer GetAnswer(int question, int answer)
        {
            return GetQuestion(question).Answers.Values.Skip(answer).First();
        }

        public static Question GetQuestion(int question)
        {
            return Questionaire.Questions.Skip(question).First();
        }
    }
}