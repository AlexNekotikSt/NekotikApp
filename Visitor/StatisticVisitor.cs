using Domain.Widget;

namespace Visitor
{
    public class StatisticVisitor : IWidgetVisitor
    {
        public int TextWidgetCount { get; private set; }
        public int TextSymbolCount { get; private set; }

        public int NumericWidgetCount { get; private set; }
        public decimal NumericSum { get; private set; }

        public int FileWidgetCount { get; private set; }
        public int MediaFileCount { get; private set; }

        public int DateWidgetCount { get; private set; }
        public DateTime? MinDate { get; private set; }
        public DateTime? MaxDate { get; private set; }

        public int Count { get; private set; }

        public void Visit(TextWidget widget)
        {
            TextWidgetCount++;
            Count++;
            if (!string.IsNullOrEmpty(widget.Text))
                TextSymbolCount += widget.Text.Length;
        }

        public void Visit(NumericWidget widget)
        {
            NumericWidgetCount++;
            Count++;
            NumericSum += widget.Value;
        }

        public void Visit(FileWidget widget)
        {
            FileWidgetCount++;
            Count++;
            MediaFileCount += widget.Medias?.Count ?? 0;
        }

        public void Visit(PictureWidget widget)
        {
            Visit(widget as FileWidget);
        }

        public void Visit(DateWidget widget)
        {
            DateWidgetCount++;
            Count++;

            if (MinDate == null || widget.Date < MinDate)
                MinDate = widget.Date;

            if (MaxDate == null || widget.Date > MaxDate)
                MaxDate = widget.Date;
        }

        public void Reset()
        {
            TextWidgetCount = 0;
            TextSymbolCount = 0;
            NumericWidgetCount = 0;
            NumericSum = 0;
            FileWidgetCount = 0;
            MediaFileCount = 0;
            DateWidgetCount = 0;
            MinDate = null;
            MaxDate = null;
        }

        public StatisticsResult GetStatistics()
        {
            return new StatisticsResult
            {
                TotalWidgets = Count,
                Metrics = new Dictionary<string, object>
                {
                    {"TextWidgetCount", TextWidgetCount },
                    {"TextSymbolCount", TextSymbolCount },
                    {"NumericWidgetCount", NumericWidgetCount },
                    {"NumericSum", NumericSum },
                    {"FileWidgetCount", FileWidgetCount },
                    {"MediaFileCount", MediaFileCount },
                    {"DateWidgetCount", DateWidgetCount },
                    {"MinDate", MinDate },
                    {"MaxDate", MaxDate },
                }
            };
        }
    }
}
