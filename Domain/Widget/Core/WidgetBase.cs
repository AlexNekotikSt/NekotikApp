using Domain.Memento;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Visitor;

namespace Domain.Widget
{
    [XmlInclude(typeof(TextWidget))]
    [XmlInclude(typeof(FileWidget))]
    [XmlInclude(typeof(NumericWidget))]
    [XmlInclude(typeof(PictureWidget))]
    [XmlInclude(typeof(DateWidget))]

    [JsonDerivedType(typeof(TextWidget), "TextWidget")]
    [JsonDerivedType(typeof(FileWidget), "FileWidget")]
    [JsonDerivedType(typeof(NumericWidget), "NumericWidget")]
    [JsonDerivedType(typeof(PictureWidget), "PictureWidget")]
    [JsonDerivedType(typeof(DateWidget), "DateWidget")]
    public abstract class WidgetBase : ICloneable, IMemento
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

        public WidgetMemento CreateSnapshot()
        {
            return new WidgetMemento(this);
        }

        public void Restore(WidgetMemento memento)
        {
            if (memento.Snapshot.GetType() != GetType())
                throw new InvalidOperationException("Cannot restore a widget of different type.");

            var restored = memento.Snapshot;

            Id = restored.Id;
            Name = restored.Name;
            Column = restored.Column;
            Order = restored.Order;

            RestoreInternal(restored);
        }

        protected abstract void RestoreInternal(WidgetBase from);

        public abstract void Accept(IWidgetVisitor visitor);
    }
}