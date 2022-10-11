using RP_IndustriPark.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace RP_IndustriPark.Services
{
    public class IndustryClient : IIndustryClient
    {
        private readonly HttpClient httpClient;

        public IndustryClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Device>?> GetAsync()
        {

            return await httpClient.GetFromJsonAsync<Device[]>("api/industrypark");
        }

        public async Task<Device?> PostAsync(Device device)
        {

            var response = await httpClient.PostAsJsonAsync<Device>("api/industrypark", device);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Device>();

            return null;
        }

        public async Task<bool> ToggleAsync(Device device)
        {
            var toggeldevice = device;

            //togeldItem.Status = !device.Status;

            var response = await httpClient.PutAsJsonAsync($"api/industrypark/{device.Id}", toggeldevice);

            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await httpClient.DeleteAsync($"api/industrypark/{id}");

            return response.IsSuccessStatusCode ? true : false;
        }

        //public async Task<bool> EditAsync(Device device)
        //{
        //    var updateItem = new EditItem { Completed = device.Completed };

        //    var response = await httpClient.PutAsJsonAsync($"api/industrypark/{device.DeviceID}", updateItem);

        //    return response.IsSuccessStatusCode ? true : false;
        //}
    }
}
