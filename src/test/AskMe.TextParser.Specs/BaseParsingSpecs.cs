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

        public static Answer GetAnswer(int itemIndex, int answer)
        {
            return GetItem(itemIndex).Answers.Values.Skip(answer).First();
        }

        public static Item GetItem(int itemIndex)
        {
            return Questionaire.Items.Skip(itemIndex).First();
        }
    }
}