using NotinoHomework.Application.Interfaces;

namespace NotinoHomework.Application.Services;

public class LocalFileHandler : ILocalFileHandler
{
    public string ReadFile(string filePath)
    {
        filePath = PrepareFilePath(filePath);

        using var fileStream = File.Open(filePath, FileMode.Open);
        using var reader = new StreamReader(fileStream);
        var data = reader.ReadToEnd();

        return data;
    }

    public string WriteFile(string filePath, string data)
    {
        filePath = PrepareWriteFilePath(filePath);

        using var fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write);
        using var writer = new StreamWriter(fileStream);
        writer.Write(data);

        return filePath;
    }

    private static string PrepareFilePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("filePath is empty");

        filePath = Path.Combine(Environment.CurrentDirectory, filePath);

        return filePath;
    }

    private static string PrepareWriteFilePath(string filePath)
    {
        filePath = PrepareFilePath(filePath);

        var directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        return filePath;
    }
}