using NotinoHomework.Application.Enums;

namespace NotinoHomework.Application.Interfaces;

public interface IConverter
{
    public ConverterType ConverterType { get; }
    public Document From(string serializedDocument);
    public string To(Document document);
}