using System.Linq;
using AskMe.Model;
using Machine.Specifications;

namespace AskMe.TextParser.Specs
{
    public class when_parsing
    {
        protected static string Text;
        static readonly QuestionnaireParser QuestionnaireParser = new QuestionnaireParser();
        protected static Questionnaire Questionnaire;

        Because of = () => Questionnaire = QuestionnaireParser.Parse(Text);

        public static Answer GetAnswer(int itemIndex, int answer)
        {
            return GetItem(itemIndex).Answers.Values.Skip(answer).First();
        }

        public static Item GetItem(int itemIndex)
        {
            return Questionnaire.Items.Skip(itemIndex).First();
        }
    }
}