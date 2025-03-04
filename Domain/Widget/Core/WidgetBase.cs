namespace Domain.Widget
{
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
