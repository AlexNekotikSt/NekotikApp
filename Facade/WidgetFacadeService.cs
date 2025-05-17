using Domain.Memento;
using Domain.Widget;
using Facade.Subsystems;
using Strategy;
using Strategy.Strategies;
using Visitor;

namespace Facade
{
    public class WidgetFacadeService
    {
        private readonly StorageContext _storage;
        private readonly IWidgetHistoryManager _history;
        private readonly IWidgetValidator _validator;
        private readonly IWidgetFilterInterpreter _filterInterpreter;
        private readonly IWidgetExporter _exporter;

        public WidgetFacadeService()
        {
            _storage = new StorageContext();
            _history = new WidgetHistory();
            _validator = new WidgetValidator();
            _filterInterpreter = new WidgetFilterInterpreter();
            _exporter = new WidgetExporter();

            _storage.SetStrategy(new JsonWidgetStorageStrategy());
        }

        public async Task Save(WidgetBase widget)
        {
            var widgets = await _storage.Load();

            widgets.Add(widget);

            _history.SaveState(widget);

            await _storage.Save(widgets);
        }

        public void UndoChanges(WidgetBase widget)
        {
            _history.Undo(widget);
        }

        public bool ApplyValidation(WidgetBase widget)
        {
            return _validator.Validate(widget);
        }

        public StatisticsResult GetStatistics(IEnumerable<WidgetBase> widgets)
        {
            var statisticVisitor = new StatisticVisitor();

            foreach (WidgetBase widget in widgets)
            {
                widget.Accept(statisticVisitor);
            }

            return statisticVisitor.GetStatistics();
        }

        public IEnumerable<WidgetBase> Filter(IEnumerable<WidgetBase> widgets, string query)
        {
            return _filterInterpreter.Interpret(widgets, query);
        }

        public string Export(IEnumerable<WidgetBase> widgets)
        {
            return _exporter.Export(widgets);
        }

        public WidgetBase Clone(WidgetBase widget)
        {
            return widget.Clone() as WidgetBase;
        }
    }
}
