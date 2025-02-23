using Domain.Widget;
using FactoryMethod.Impl;
using System.Collections.Concurrent;

namespace FactoryMethod.Core
{
    public class WidgetsFactory
    {
        protected readonly ConcurrentDictionary<WidgetType, WidgetFactory> widgetsFactories;

        public WidgetsFactory()
        {
            widgetsFactories = new();
            widgetsFactories.TryAdd(WidgetType.Text, new TextWidgetFactory());
            widgetsFactories.TryAdd(WidgetType.Numeric , new NumericWidgetFactory());
            widgetsFactories.TryAdd(WidgetType.Date , new DateWidgetFactory());
            widgetsFactories.TryAdd(WidgetType.File , new FileWidgetFactory());
            widgetsFactories.TryAdd(WidgetType.Picture, new PictureWidgetFactory());
        }

        public WidgetBase? Create(WidgetType widgetType)
        {
            return widgetsFactories.TryGetValue(widgetType, out var factory)? factory.CreateEmpty() : null;
        }
    }

}
