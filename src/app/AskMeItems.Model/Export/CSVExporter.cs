using System.Collections.Generic;
using System.Text;

namespace AskMeItems.Model.Export
{
    public class CSVExporter : IExporter
    {
        public string Export(List<Result> results, List<Subscale> subscales)
        {
            const string FieldDelimiter = "\t";
            const string LineDelimiter = "\r\n";
            var sb = new StringBuilder();
            results
                .ForEach(x => sb.AppendFormat("ITEM{1}{0}{1}{2}{1}{3}{4}",
                                              x.Item.Code,
                                              FieldDelimiter,
                                              x.SelectedAnswer.Code,
                                              x.SelectedAnswer.Points,
                                              LineDelimiter));
            return sb.ToString();
        }

        public string Prefix
        {
            get { return "Result"; }
        }
    }
}