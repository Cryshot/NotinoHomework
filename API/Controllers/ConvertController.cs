using NotinoHomework.Application.Services;

namespace NotinoHomework.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConvertController : ControllerBase
{
    private readonly ConvertService _convertService;

    public ConvertController(
        ConvertService convertService
        )
    {
        _convertService = convertService;
    }

    [HttpGet("local")]
    public string ConvertLocalFile([FromQuery] ConvertLocalOptions options)
    {
        var targetFilePath = _convertService.ConvertLocalFile(options, options.SourceFilePath, options.TargetFilePath);
        return targetFilePath;
    }

    [HttpGet("url")]
    public string ConvertUrlFile([FromQuery] ConvertUrlOptions options)
    {
        var convertedDocument = _convertService.ConvertFromUrl(options, options.Url);
        return convertedDocument;
    }

    [HttpPost("upload")]
    public string ConvertUploadedFile([FromQuery] ConvertService.ConvertOptions options, IFormFile file)
    {
        string sourceDocument;
        using (var reader = new StreamReader(file.OpenReadStream()))
            sourceDocument = reader.ReadToEnd();

        var convertedDocument = _convertService.Convert(options, sourceDocument);
        return convertedDocument;
    }

    public class ConvertLocalOptions : ConvertService.ConvertOptions
    {
        [Required]
        public string SourceFilePath { get; set; }
        [Required]
        public string TargetFilePath { get; set; }
    }

    public class ConvertUrlOptions : ConvertService.ConvertOptions
    {
        [Required]
        public string Url { get; set; }
    }
}