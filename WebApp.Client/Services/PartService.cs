using System.Net.Http.Json;
using WebApp.Shared.Dto;
using static System.Net.WebRequestMethods;

namespace WebApp.Client.Services;

public class PartService
{
    private readonly HttpClient _httpClient;

    public PartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PartDto>> GetPartsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<PartDto>>("api/parts") ?? new List<PartDto>();
    }

    public async Task AddPartAsync(PartDto part)
    {
        var response = await _httpClient.PostAsJsonAsync("api/parts", part);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding part: {response.ReasonPhrase}");
        }
    }

    public async Task UpdatePartAsync(PartDto part)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/parts/{part.Id}", part);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating part: {response.ReasonPhrase}");
        }
    }

    public async Task DeletePartAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/parts/{id}");
        {
            throw new Exception($"Error deleting part: {response.ReasonPhrase}");
        }
    }
}
