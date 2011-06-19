using System.Collections.Generic;

namespace AskMeItems.Model.Export
{
    public interface IExporter
    {
        string Export(List<Result> results, List<Subscale> subscales);
        string Prefix { get; }
    }
}