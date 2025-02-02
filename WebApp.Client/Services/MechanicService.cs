using System.Net.Http;
using System.Net.Http.Json;
using WebApp.Shared.Dto;
using static System.Net.WebRequestMethods;

namespace WebApp.Client.Services;

public class MechanicService
{
    private readonly HttpClient _httpClient;

    public MechanicService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MechanicDto>> GetMechanicsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<MechanicDto>>("api/mechanics") ?? new List<MechanicDto>();
    }

    public async Task<MechanicDto?> GetMechanicByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<MechanicDto>($"api/mechanics/{id}");
    }

    public async Task<Dictionary<int, List<RepairDto>>> GetRepairsByMechanicAsync()
    {
        return await _httpClient.GetFromJsonAsync<Dictionary<int, List<RepairDto>>>("api/mechanics/repairs");
    }


    public async Task AddMechanicAsync(MechanicDto mechanic)
    {
        var response = await _httpClient.PostAsJsonAsync("api/mechanics", mechanic);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding mechanic: {response.ReasonPhrase}");
        }
    }

    public async Task UpdateMechanicAsync(MechanicDto mechanic)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/mechanics/{mechanic.Id}", mechanic);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating mechanic: {response.ReasonPhrase}");
        }
    }

    public async Task DeleteMechanicAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/mechanics/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting mechanic: {response.ReasonPhrase}");
        }
    }
}
