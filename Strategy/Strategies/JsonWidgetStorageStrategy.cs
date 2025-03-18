using Domain.Widget;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Strategy.Strategies
{
    public class JsonWidgetStorageStrategy : IWidgetStorageStrategy
    {
        private const string _filePath = "Widgets.json";

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { JsonPolymorphicTypeInfoResolver.Modifier }
            }
        };

        public async Task<List<WidgetBase>> Load()
        {
            if (File.Exists(_filePath))
            {
                using Stream stream = File.OpenRead(_filePath);
                return await JsonSerializer.DeserializeAsync<List<WidgetBase>>(stream, options);
            }

            return [];
        }

        public async Task Save(IEnumerable<WidgetBase> widgets)
        {
            using FileStream stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, widgets, options);
        }

        public static class JsonPolymorphicTypeInfoResolver
        {
            public static void Modifier(JsonTypeInfo typeInfo)
            {
                if (typeInfo.Type == typeof(WidgetBase))
                {
                    typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                    {
                        TypeDiscriminatorPropertyName = "$type",
                        DerivedTypes =
                {
                    new JsonDerivedType(typeof(TextWidget), "TextWidget"),
                    new JsonDerivedType(typeof(FileWidget), "FileWidget"),
                    new JsonDerivedType(typeof(NumericWidget), "NumericWidget"),
                    new JsonDerivedType(typeof(PictureWidget), "PictureWidget"),
                    new JsonDerivedType(typeof(DateWidget), "DateWidget")
                }
                    };
                }
            }
        }
    }
}
