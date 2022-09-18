using Microsoft.Extensions.DependencyInjection;
using NotinoHomework.Application.Enums;
using NotinoHomework.Application.Services;

namespace NotinoHomework.UnitTest.Application;

[TestClass]
public class ConverterTypeResolverTests
{
    private ServiceProvider _serviceProvider;

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();
        services.AddTransient<ConverterTypeResolver>();
        _serviceProvider = services.BuildServiceProvider();
    }

    [TestMethod]
    [DynamicData(nameof(Get_TestInputs), DynamicDataSourceType.Method)]
    public void Get_ConverterTypeJsonAndXml_ReturnsCorrectConverter(ConverterType type)
    {
        var converterTypeResolver = _serviceProvider.GetService<ConverterTypeResolver>();

        var converter = converterTypeResolver.Get(type);

        Assert.AreEqual(type, converter.ConverterType);
    }

    private static IEnumerable<object[]> Get_TestInputs()
    {
        var inputs = new List<object[]>();

        var converterTypes = Enum.GetValues(typeof(ConverterType)).Cast<ConverterType>();
        foreach (var converterType in converterTypes)
            inputs.Add(new object[] { converterType });

        return inputs;
    }
}