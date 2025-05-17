using Domain.Widget;

namespace Visitor
{
    public interface IWidgetVisitor
    {
        void Visit(TextWidget widget);
        void Visit(NumericWidget widget);
        void Visit(DateWidget widget);
        void Visit(FileWidget widget);
        void Visit(PictureWidget widget);
        StatisticsResult GetStatistics();
    }

    public class StatisticsResult
    {
        public int TotalWidgets { get; set; }
        public IDictionary<string, object> Metrics { get; set; }
    }
}
