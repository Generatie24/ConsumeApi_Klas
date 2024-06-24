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
        public async Task<IEnumerable<ReservationDetailsDto>> GetReservationsAsync()
        {

            var response = await _httpClient.GetAsync("Reservations");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ReservationDetailsDto>>();

        }
    }
}
