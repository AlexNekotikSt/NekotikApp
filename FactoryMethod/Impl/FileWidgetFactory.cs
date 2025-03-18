using Domain.Media;
using Domain.Widget;

namespace FactoryMethod.Impl
{
    public class FileWidgetFactory : FileWidgetFactory<FileWidget>
    {

    }

    public class FileWidgetFactory<TFileWidget> : GenericWidgetFactory<TFileWidget>
        where TFileWidget : FileWidget, new()
    {
        protected override TFileWidget SetValue(TFileWidget widget)
        {
            widget.Medias ??= [];

            widget.Medias.Add(new MediaModel
            {
                Id = widget.Id,
                Name = $"Media {widget.Id}",
                Url = $"http://media/{widget.Id}",
            });

            return widget;
        }
    }

}
