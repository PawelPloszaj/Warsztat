using System.Net.Http.Json;
using WebApp.Shared.Dto;

namespace WebApp.Client.Services;

public class HistoryService
{
    private readonly HttpClient _httpClient;

    public HistoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RepairDto>> GetRepairHistoryAsync(int vehicleId)
    {
        return await _httpClient.GetFromJsonAsync<List<RepairDto>>($"api/history/{vehicleId}") ?? new List<RepairDto>();
    }
}
