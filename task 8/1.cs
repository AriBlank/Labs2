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
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}