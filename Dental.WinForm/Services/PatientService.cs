using Dental.WinForm.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dental.WinForm.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateProductAsync(Patient patient)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/patient", content);
            return response.IsSuccessStatusCode; 
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/patient/{id}");
            return response.IsSuccessStatusCode; 
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            var response = await _httpClient.GetAsync("api/patient");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Patient>>(json);
        }

        public async Task<Patient> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/patient/{id}");

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Patient>(json);
        }

        public Task<Patient> GetProductByNameAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateProductAsync(Patient patient)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/patient", content);
            return response.IsSuccessStatusCode; 
        }
    }
}
