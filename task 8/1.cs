using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class AuthenticationProxy : IDisposable
{
    private readonly HttpClient _httpClient;
    public AuthenticationProxy(string baseAddress, string apiKey, string headerName = "X-API-Key")
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
    }
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}