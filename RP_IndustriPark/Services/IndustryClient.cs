using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace RP_IndustriPark.Services
{
    public class IndustryClient: IIndustryClient
    {
        private readonly HttpClient httpClient;

        public IndustryClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MachineIndustry>?> GetAsync()
        {

            return await httpClient.GetFromJsonAsync<MachineIndustry[]>("sample-data/industry.json");
        }
    }
}
