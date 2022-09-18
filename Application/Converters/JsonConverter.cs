using NotinoHomework.Application.Enums;
using Newtonsoft.Json;
using NotinoHomework.Application.Interfaces;

namespace NotinoHomework.Application.Converters;

public class JsonConverter : IConverter
{
    public ConverterType ConverterType => ConverterType.JSON;

    public Document From(string serializedDocument)
    {
        var document = JsonConvert.DeserializeObject<Document>(serializedDocument);
        return document;
    }

    public string To(Document document)
    {
        var jsonDocument = JsonConvert.SerializeObject(document);
        return jsonDocument;
    }
}
