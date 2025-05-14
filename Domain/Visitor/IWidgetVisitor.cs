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
    }
}
