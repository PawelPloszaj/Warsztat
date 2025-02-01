using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using WebApp.Shared.Dto;
using WebApp.Client.Utility;
using System.Net.Http.Json;

namespace WebApp.Client.Services;

public class VehicleService
{
    private readonly HttpClient _httpClient;

    public VehicleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<VehicleDto>>("api/vehicles") ?? new List<VehicleDto>();
    }

    public async Task AddVehicleAsync(VehicleDto vehicle)
    {
        await _httpClient.PostAsJsonAsync("api/vehicles", vehicle);
    }

    public async Task UpdateVehicleAsync(VehicleDto vehicle)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/vehicles/{vehicle.Id}", vehicle);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating vehicle: {response.ReasonPhrase}");
        }
    }

    public async Task DeleteVehicleAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/vehicles/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting vehicle: {response.ReasonPhrase}");
        }
    }
}
