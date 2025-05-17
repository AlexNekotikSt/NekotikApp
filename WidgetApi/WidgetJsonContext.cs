using System.Text.Json.Serialization;

namespace WidgetApi
{
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]

    [JsonSerializable(typeof(Domain.Widget.WidgetBase))]
    [JsonSerializable(typeof(Domain.Widget.TextWidget))]
    [JsonSerializable(typeof(Domain.Widget.FileWidget))]
    [JsonSerializable(typeof(Domain.Widget.NumericWidget))]
    [JsonSerializable(typeof(Domain.Widget.PictureWidget))]
    [JsonSerializable(typeof(Domain.Widget.DateWidget))]
    [JsonSerializable(typeof(IEnumerable<Domain.Widget.WidgetBase>))]
    public partial class WidgetJsonContext : JsonSerializerContext
    {
    }
}
