using Domain.Widget;
using System.Xml.Serialization;

namespace Strategy.Strategies
{
    public class XmlWidgetStorageStrategy : IWidgetStorageStrategy
    {
        private const string _filePath = "Widgets.xml";

        public async Task<List<WidgetBase>> Load()
        {
            if (File.Exists(_filePath))
            {
                using Stream stream = File.OpenRead(_filePath);
                XmlSerializer formatter = new(typeof(List<WidgetBase>), new XmlRootAttribute("Widgets"));
                var widgets = formatter.Deserialize(stream) as List<WidgetBase>;

                return widgets ?? new List<WidgetBase>();
            }

            return new List<WidgetBase>();
        }

        public async Task Save(IEnumerable<WidgetBase> widgets)
        {
            if(File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }

            using Stream stream = File.OpenWrite(_filePath);
            XmlSerializer formatter = new(typeof(List<WidgetBase>), new XmlRootAttribute("Widgets"));
            formatter.Serialize(stream, widgets.ToList());
            await Task.CompletedTask;
        }
    }
}
