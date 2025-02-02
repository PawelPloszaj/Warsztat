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

    public async Task<RepairDto?> GetRepairByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<RepairDto>($"api/repairs/{id}");
    }

    public async Task AddRepairAsync(RepairDto repair)
    {
        var response = await _httpClient.PostAsJsonAsync("api/repairs", repair);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error adding repair: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task UpdateRepairAsync(RepairDto repair)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/repairs/{repair.Id}", repair);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error updating repair: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task DeleteRepairAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/repairs/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error deleting repair: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task AssignMechanicToRepairAsync(int repairId, int mechanicId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/repairs/{repairId}/assign-mechanic", new { MechanicId = mechanicId });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error assigning mechanic: {response.StatusCode} - {errorMessage}");
        }
    }
    public async Task<List<MechanicDto>> GetAssignedMechanicsAsync(int repairId)
    {
        return await _httpClient.GetFromJsonAsync<List<MechanicDto>>($"api/repairs/{repairId}/mechanics") ?? new List<MechanicDto>();
    }
    public async Task RemoveMechanicFromRepairAsync(int repairId, int mechanicId)
    {
        var response = await _httpClient.DeleteAsync($"api/repairs/{repairId}/remove-mechanic/{mechanicId}");
        response.EnsureSuccessStatusCode();
    }
}
