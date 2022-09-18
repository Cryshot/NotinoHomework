using NotinoHomework.Application.Enums;
using NotinoHomework.Application.Interfaces;
using System.Reflection;

namespace NotinoHomework.Application.Services;

public class ConverterTypeResolver
{
    private readonly Dictionary<ConverterType, IConverter> _converters = new();

    public ConverterTypeResolver()
    {
        RegisterAllIConverters();
    }

    private void RegisterAllIConverters()
    {
        var converterTypes = Assembly
            .GetAssembly(typeof(IConverter))
            .GetTypes()
            .Where(x => IsImplementationOfIConverter(x));

        foreach (var converterType in converterTypes)
            RegisterConverterType(converterType);
    }

    private static bool IsImplementationOfIConverter(Type type)
    {
        return
            !type.IsInterface &&
            type.GetInterface(typeof(IConverter).Name) != null;
    }

    private void RegisterConverterType(Type converterType)
    {
        var converter = (IConverter)Activator.CreateInstance(converterType);
        _converters.Add(converter.ConverterType, converter);
    }

    public IConverter Get(ConverterType type)
    {
        var converter = _converters[type];
        return converter;
    }
}