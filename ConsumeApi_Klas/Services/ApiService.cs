using ConsumeApi_Klas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeApi_Klas.Services
{

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7157/api/");
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {

           var response = await _httpClient.GetAsync("Customers");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>();
        }
        public async Task<IEnumerable<ReservationDetailsDto>> GetReservationsAsync()
        {

            var response = await _httpClient.GetAsync("Reservations");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ReservationDetailsDto>>();

        }

        public async Task<ReservationDetailsDto> CreateReservationAsync(CreateReservationDto reservation)
        {
           var response = await _httpClient.PostAsJsonAsync("Reservations", reservation);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReservationDetailsDto>();
        }

        public async Task<ReservationDetailsDto> GetReservationAsync(int reservationId)
        {
            var response = await _httpClient.GetAsync($"Reservations/{reservationId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReservationDetailsDto>();
        }

        public async Task UpdateReservationAsync(int id, UpdateReservationDto reservation)
        {
            var response = await _httpClient.PutAsJsonAsync($"Reservations/{id}", reservation);
            response.EnsureSuccessStatusCode();
        }

    }
}
