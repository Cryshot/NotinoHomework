using NotinoHomework.Application.Interfaces;

namespace NotinoHomework.Application.Services;

public class UrlDownloader : IUrlDownloader
{
    public string Download(string url)
    {
        if (!IsUrlValid(url))
            throw new Exception($"Url '{url}' is not valid");

        var client = new HttpClient();
        var response = client.Send(new HttpRequestMessage(HttpMethod.Get, url));
        using var reader = new StreamReader(response.Content.ReadAsStream());
        var data = reader.ReadToEnd();
        return data;
    }

    private static bool IsUrlValid(string url)
    {
        //source:https://stackoverflow.com/a/7581824
        var result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        return result;
    }
}