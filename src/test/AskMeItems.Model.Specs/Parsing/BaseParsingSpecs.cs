using System.Linq;

using AskMeItems.Model.Parser;

using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing
    {
        protected static string Text;
        static readonly QuestionnaireParser QuestionnaireParser = new QuestionnaireParser();
        protected static Questionnaire Questionnaire;

        Because of = () => Questionnaire = QuestionnaireParser.Parse("questionnaireCode", Text);

        public static Answer GetAnswer(int itemIndex, int answer)
        {
            return GetItem(itemIndex).Answers.Values.Skip(answer).First();
        }

        public static Item GetItem(int itemIndex)
        {
            return Questionnaire.Items.Skip(itemIndex).First();
        }
    }

    public class when_parsing_from_file
    {
        protected static string FileName;

        static readonly QuestionnaireParser QuestionnaireParser = new QuestionnaireParser();
        protected static Questionnaire Questionnaire;

        Because of = () => Questionnaire = QuestionnaireParser.ParseFromFile(FileName);

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