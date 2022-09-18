using NotinoHomework.Application.Enums;
using NotinoHomework.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NotinoHomework.Application.Services;

public class ConvertService
{
    private readonly ConverterTypeResolver _converterTypeResolver;
    private readonly ILocalFileHandler _localFileHandler;
    private readonly IUrlDownloader _urlDownloader;
    private readonly IEmailService _emailService;

    private const string ATTACHMENT_NAME = "converted_document";

    public ConvertService(
        ConverterTypeResolver converterTypeMapping,
        ILocalFileHandler localFileHandler,
        IUrlDownloader urlDownloader,
        IEmailService emailService
        )
    {
        _converterTypeResolver = converterTypeMapping;
        _localFileHandler = localFileHandler;
        _urlDownloader = urlDownloader;
        _emailService = emailService;
    }

    public string Convert(ConvertOptions options, string sourceDocument)
    {
        if (string.IsNullOrWhiteSpace(sourceDocument))
            throw new ArgumentException($"Source document is empty");

        var fromConverter = _converterTypeResolver.Get(options.FromType);
        var toConverter = _converterTypeResolver.Get(options.ToType);

        var document = fromConverter.From(sourceDocument);
        var convertedDocument = toConverter.To(document);

        if (options.SendByEmail)
            _emailService.Send(convertedDocument, $"{ATTACHMENT_NAME}.{options.ToType.ToString().ToLower()}");

        return convertedDocument;
    }

    public string ConvertLocalFile(ConvertOptions options, string sourceFilePath, string targetFilePath)
    {
        if (sourceFilePath == targetFilePath)
            throw new ArgumentException("sourceFilePath is same as targetFilePath");

        var sourceDocument = _localFileHandler.ReadFile(sourceFilePath);
        var convertedDocument = Convert(options, sourceDocument);
        targetFilePath = _localFileHandler.WriteFile(targetFilePath, convertedDocument);

        return targetFilePath;
    }

    public string ConvertFromUrl(ConvertOptions options, string url)
    {
        var sourceDocument = _urlDownloader.Download(url);
        var convertedDocument = Convert(options, sourceDocument);

        return convertedDocument;
    }

    public class ConvertOptions
    {
        [Required]
        public ConverterType FromType { get; set; }
        [Required]
        public ConverterType ToType { get; set; }
        public bool SendByEmail { get; set; }
    }
}