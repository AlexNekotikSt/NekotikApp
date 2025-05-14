using Domain.Media;

namespace Domain.Widget
{
    public class FileWidget : WidgetBase
    {
        public List<MediaModel>? Medias { get; set; }

        public override string GetValue()
        {
            return string.Join(", ", Medias?.Select(m => m.Name));
        }

        protected override void RestoreInternal(WidgetBase from)
        {
            if (from is FileWidget widget)
            {
                Medias = widget.Medias;
            }
        }
    }
}
