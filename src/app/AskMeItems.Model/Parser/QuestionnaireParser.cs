using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace AskMeItems.Model.Parser
{
    public class QuestionnaireParser
    {
        public Questionnaire Parse(string questionnaireCode, string text)
        {
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var lineNo = 0;
            var questions = new List<Item>();
            var questionCount = 0;
            QuestionnaireType type;
            Enum.TryParse(Helpers.ParseProperty("Questionnaire-Type", lines, ref lineNo, "ListedAnswers"), out type);
            var instruction = Helpers.ParseProperty("Instruction", lines, ref lineNo, null);

            while (ItemParser.HasNextQuestion(lines, lineNo))
                questions.Add(ItemParser.Parse(lines, questionCount++, ref lineNo));
            return new Questionnaire(questionnaireCode, type, instruction, questions);
        }

        public Questionnaire ParseFromFile(string fileName)
        {
            var text = File.ReadAllText(fileName, Encoding.Default);
            var fileInfo = new FileInfo(fileName);
            return Parse(fileInfo.Name.Replace(fileInfo.Extension, ""), text);
        }
    }
}