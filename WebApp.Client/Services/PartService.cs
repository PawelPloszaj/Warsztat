using System.Net.Http.Json;
using WebApp.Shared.Dto;

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

    public async Task<PartDto?> GetPartByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PartDto>($"api/parts/{id}");
    }

    public async Task AddPartAsync(PartDto part)
    {
        var response = await _httpClient.PostAsJsonAsync("api/parts", part);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error adding part: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task UpdatePartAsync(PartDto part)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/parts/{part.Id}", part);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error updating part: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task DeletePartAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/parts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error deleting part: {response.StatusCode} - {errorMessage}");
        }
    }
}
