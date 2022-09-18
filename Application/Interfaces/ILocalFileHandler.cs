namespace NotinoHomework.Application.Interfaces;

public interface ILocalFileHandler
{
    string ReadFile(string filePath);
    string WriteFile(string filePath, string data);
}