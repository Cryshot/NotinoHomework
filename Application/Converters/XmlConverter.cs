using NotinoHomework.Application.Enums;
using NotinoHomework.Application.Interfaces;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NotinoHomework.Application.Converters;

public class XmlConverter : IConverter
{
    public ConverterType ConverterType => ConverterType.XML;

    public Document From(string serializedDocument)
    {
        var xdoc = XDocument.Parse(serializedDocument);
        var doc = new Document
        {
            Title = xdoc.Root.Element("Title")?.Value,
            Text = xdoc.Root.Element("Text")?.Value
        };

        return doc;
    }

    public string To(Document document)
    {
        var xDocument = new XDocument();

        using (var writer = xDocument.CreateWriter())
        {
            var xmlSerializer = new XmlSerializer(document.GetType());
            xmlSerializer.Serialize(writer, document);
        }

        xDocument.Root.RemoveAttributes();
        var xmlDocument = xDocument.ToString();
        return xmlDocument;
    }
}