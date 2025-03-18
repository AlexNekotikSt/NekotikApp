using System.Xml.Serialization;

namespace Domain.Widget
{
    [XmlInclude(typeof(TextWidget))]
    [XmlInclude(typeof(FileWidget))]
    [XmlInclude(typeof(NumericWidget))]
    [XmlInclude(typeof(PictureWidget))]
    [XmlInclude(typeof(DateWidget))]
    public abstract class WidgetBase : ICloneable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Column { get; set; }

        public int Order { get; set; }

        public abstract string GetValue();

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}