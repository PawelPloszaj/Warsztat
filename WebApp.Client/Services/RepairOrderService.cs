using System.Net.Http.Json;
using WebApp.Shared.Dto;

namespace WebApp.Client.Services;

public class RepairOrderService
{
    private readonly HttpClient _httpClient;

    public RepairOrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RepairOrderDto>> GetRepairOrdersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<RepairOrderDto>>("api/repairorders") ?? new List<RepairOrderDto>();
    }

    public async Task<RepairOrderDto?> GetRepairOrderByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<RepairOrderDto>($"api/repairorders/{id}");
    }

    public async Task AddRepairOrderAsync(RepairOrderDto repairOrder)
    {
        var response = await _httpClient.PostAsJsonAsync("api/repairorders", repairOrder);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error adding repair order: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task UpdateRepairOrderAsync(RepairOrderDto repairOrder)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/repairorders/{repairOrder.Id}", repairOrder);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error updating repair order: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task DeleteRepairOrderAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/repairorders/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error deleting repair order: {response.StatusCode} - {errorMessage}");
        }
    }
}
