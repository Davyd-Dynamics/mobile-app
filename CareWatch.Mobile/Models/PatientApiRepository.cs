using CareWatch.Mobile.Models.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CareWatch.Mobile.Models
{
    public class PatientApiRepository
    {
        private readonly HttpClient httpClient;
        private readonly string baseApiUrl = "http://192.168.0.109:5000";

        public PatientApiRepository()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync(SearchFIlter searchFilter = null)
        {
            try
            {
                string apiUrl = $"{baseApiUrl}/api/v1/patients/";

                if (searchFilter != null)
                {
                    // Serialize the search filter and append it to the URL
                    var queryString = string.Join("&", searchFilter.GetType()
                        .GetProperties()
                        .Where(prop => prop.GetValue(searchFilter) != null)
                        .Select(prop => $"{prop.Name}={Uri.EscapeDataString(prop.GetValue(searchFilter).ToString())}"));

                    apiUrl += $"?{queryString}";
                }

                var uri = new Uri("http://10.0.2.2:5000/api/v1/patients");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Patient>>(content);
            }
            catch (Exception ex)
            {
                // Handle the error
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(Guid patientId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseApiUrl}/api/v1/patients/{patientId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Patient>(content);
            }
            catch (Exception ex)
            {
                // Обробити помилку
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task UpdatePatientAsync(Guid patientId, PatientRequest updatePatientRequestCommand)
        {
            try
            {
                var json = JsonConvert.SerializeObject(updatePatientRequestCommand);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{baseApiUrl}/api/v1/patients/{patientId}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Обробити помилку
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task<Patient> CreatePatientAsync(PatientRequest createPatientCommand)
        {
            try
            {
                var json = JsonConvert.SerializeObject(createPatientCommand);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{baseApiUrl}/api/v1/patients", content);
                response.EnsureSuccessStatusCode();
                var createdPatientContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Patient>(createdPatientContent);
            }
            catch (Exception ex)
            {
                // Обробити помилку
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeletePatientAsync(Guid patientId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseApiUrl}/api/v1/patients/{patientId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Обробити помилку
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
