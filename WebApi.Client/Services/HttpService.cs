using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using WebApi.Client.Models;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<ApiResult<T?>> SendRequestAsync<T>(HttpRequestMessage request, string? token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return ApiResult<T>.Failure("Unauthorized");
            }

            var content = await ValidateContentAsync(response);

            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(bool) && bool.TryParse(content, out var boolValue))
                {
                    return ApiResult<T>.Success((T)(object)boolValue);
                }

                var data = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return ApiResult<T>.Success(data);
            }

            return ApiResult<T>.Failure(content);
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Failure(ex.Message);
        }
    }

    public async Task<ApiResult<T?>> GetAsync<T>(string uri, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequestAsync<T>(request, token);
    }

    public async Task<ApiResult<T?>> PostAsync<T>(string uri, object value, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<T>(request, token);
    }

    public async Task<ApiResult<T?>> PutAsync<T>(string uri, object value, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<T>(request, token);   
    }

    public async Task<ApiResult<T?>> DeleteAsync<T>(string uri, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await SendRequestAsync<T>(request, token);
    }

    private async Task<string> ValidateContentAsync(HttpResponseMessage response)
    {
        if (response.Content == null)
        {
            return "null";
        }

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
        {
            response.Content = new StringContent("null", Encoding.UTF8, MediaTypeNames.Application.Json);
            return "null";
        }

        return content;
    }
}
