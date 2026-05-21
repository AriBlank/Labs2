using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class AuthenticationProxy : IDisposable
{
    private readonly string _apiKey;
    private readonly string _headerName;
    private readonly HttpClient _httpClient;
    public AuthenticationProxy(string baseAddress, string apiKey, string headerName = "X-API-Key")
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        _apiKey = apiKey;
        _headerName = headerName;
    }
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        request.Headers.Add(_headerName, _apiKey);
        return await _httpClient.SendAsync(request, cancellationToken);
    }
    public async Task<HttpResponseMessage> GetAsync(string path, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, path);
        return await SendAsync(request, cancellationToken);
    }
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
    public async Task<HttpResponseMessage> PostAsync(string path, HttpContent content, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, path) { Content = content };
        return await SendAsync(request, cancellationToken);
    }
}

public static class Program
{
    public static async Task Main()
    {
        using var proxy = new AuthenticationProxy("https://api.example.com", "my-secret-key", "X-API-Key");
        var response = await proxy.GetAsync("/users");
        Console.WriteLine($"status: {response.StatusCode}");
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
    }
}