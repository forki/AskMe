using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AskMeItems.Model.Export
{
    public class CSVExporterWithSubscales : IExporter
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
            subscales
                .ForEach(x => sb.AppendFormat("{5}{1}{0}{1}{2}{1}{3}{4}",
                                              x.Name,
                                              FieldDelimiter,
                                              x.Points.ToString(CultureInfo.InvariantCulture),
                                              x.Average.ToString(CultureInfo.InvariantCulture),
                                              LineDelimiter,
                                              x.Type.ToString().ToUpper()));
            return sb.ToString();
        }

        public string Prefix
        {
            get { return "ResultWithSubscales"; }
        }
    }
}