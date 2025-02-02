using System.Net.Http.Json;
using WebApp.Shared.Dto;

namespace WebApp.Client.Services;

public class ClientService
{
    private readonly HttpClient _httpClient;

    public ClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ClientDto>> GetClientsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ClientDto>>("api/clients") ?? new List<ClientDto>();
    }

    public async Task<ClientDto?> GetClientByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ClientDto>($"api/clients/{id}");
    }

    public async Task AddClientAsync(ClientDto client)
    {
        var response = await _httpClient.PostAsJsonAsync("api/clients", client);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding client: {response.ReasonPhrase}");
        }
    }

    public async Task UpdateClientAsync(ClientDto client)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/clients/{client.Id}", client);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating client: {response.ReasonPhrase}");
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/clients/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting client: {response.ReasonPhrase}");
        }
    }
}
