using Microsoft.Extensions.DependencyInjection;
using NotinoHomework.Application.Enums;
using NotinoHomework.Application.Interfaces;
using NotinoHomework.Application.Services;

namespace NotinoHomework.UnitTest.Application;

[TestClass]
public class ConvertServiceTests
{
    private ServiceProvider _serviceProvider;
    private const string _sourceDocument = "{\"Title\":\"title\",\"Text\":\"text\"}";

    [TestInitialize]
    public void Initialize()
    {
        var services = new ServiceCollection();

        services.AddTransient<ConverterTypeResolver>();
        services.AddTransient<ConvertService>();

        services.AddTransient(typeof(IUrlDownloader), typeof(UrlDownloader));
        services.AddTransient(typeof(IEmailService), typeof(EmailService));
        services.AddTransient(typeof(ILocalFileHandler), typeof(LocalFileHandler));

        _serviceProvider = services.BuildServiceProvider();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Convert_EmptySourceDocument_ThrowsException()
    {
        var convertService = _serviceProvider.GetService<ConvertService>();

        var options = new ConvertService.ConvertOptions();
        convertService.Convert(options, "");
    }

    [TestMethod]
    public void Convert_JsonFromAndToType_SourceAndConvertedAreSame()
    {
        var options = new ConvertService.ConvertOptions
        {
            FromType = ConverterType.JSON,
            ToType = ConverterType.JSON
        };

        var convertService = _serviceProvider.GetService<ConvertService>();

        var convertedDocument = convertService.Convert(options, _sourceDocument);

        Assert.AreEqual(_sourceDocument, convertedDocument);
    }
}