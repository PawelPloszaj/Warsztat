using System.Net.Http.Json;
using WebApp.Shared.Dto;

namespace WebApp.Client.Services;

public class RepairService
{
    private readonly HttpClient _httpClient;

    public RepairService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RepairDto>> GetRepairsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<RepairDto>>("api/repairs") ?? new List<RepairDto>();
    }

    public async Task AddRepairAsync(RepairDto repair)
    {
        var response = await _httpClient.PostAsJsonAsync("api/repairs", repair);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding repair: {response.ReasonPhrase}");
        }
    }

    public async Task UpdateRepairAsync(RepairDto repair)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/repairs/{repair.Id}", repair);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating repair: {response.ReasonPhrase}");
        }
    }

    public async Task DeleteRepairAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/repairs/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting repair: {response.ReasonPhrase}");
        }
    }
}
