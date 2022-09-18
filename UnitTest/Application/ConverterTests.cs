using Microsoft.Extensions.DependencyInjection;
using NotinoHomework.Application;
using NotinoHomework.Application.Enums;
using NotinoHomework.Application.Services;

namespace NotinoHomework.UnitTest.Application;

[TestClass]
public class ConverterTests
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
    [DynamicData(nameof(ToAndFrom_TestInputs), DynamicDataSourceType.Method)]
    public void ToAndFrom_FilledDocument_SourceDocumentEqualsConvertedDocument(ConverterType converterType)
    {
        var converterTypeResolver = _serviceProvider.GetService<ConverterTypeResolver>();
        var converter = converterTypeResolver.Get(converterType);

        var sourceDocument = new Document { Title = "title", Text = "qwer" };

        var serializedDocument = converter.To(sourceDocument);
        var convertedDocument = converter.From(serializedDocument);

        Assert.IsTrue(AreDocumentsEqual(sourceDocument, convertedDocument));
    }

    [TestMethod]
    [DynamicData(nameof(ToAndFrom_TestInputs), DynamicDataSourceType.Method)]
    public void ToAndFrom_EmptyDocument_SourceDocumentEqualsConvertedDocument(ConverterType converterType)
    {
        var converterTypeResolver = _serviceProvider.GetService<ConverterTypeResolver>();
        var converter = converterTypeResolver.Get(converterType);

        var sourceDocument = new Document();

        var serializedDocument = converter.To(sourceDocument);
        var convertedDocument = converter.From(serializedDocument);

        Assert.IsTrue(AreDocumentsEqual(sourceDocument, convertedDocument));
    }

    private static IEnumerable<object[]> ToAndFrom_TestInputs()
    {
        var inputs = new List<object[]>();

        var converterTypes = Enum.GetValues(typeof(ConverterType)).Cast<ConverterType>();
        foreach (var converterType in converterTypes)
            inputs.Add(new object[] { converterType });

        return inputs;
    }

    private static bool AreDocumentsEqual(Document a, Document b)
    {
        if (a == null && b == null)
            return true;
        else if (a == null || b == null)
            return false;

        var areEqual = a.Text == b.Text && a.Title == b.Title;
        return areEqual;
    }
}