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

    public async Task<List<RepairOrderDto>> GetRepairOrdersForVehicleAsync(int vehicleId)
    {
        return await _httpClient.GetFromJsonAsync<List<RepairOrderDto>>($"api/history/vehicle/{vehicleId}")
               ?? new List<RepairOrderDto>();
    }

    public async Task<List<RepairDto>> GetRepairsForRepairOrderAsync(int repairOrderId)
    {
        return await _httpClient.GetFromJsonAsync<List<RepairDto>>($"api/history/repairorder/{repairOrderId}")
               ?? new List<RepairDto>();
    }

    public async Task<List<PartDto>> GetPartsForRepairAsync(int repairId)
    {
        return await _httpClient.GetFromJsonAsync<List<PartDto>>($"api/history/repair/{repairId}/parts")
               ?? new List<PartDto>();
    }
}
