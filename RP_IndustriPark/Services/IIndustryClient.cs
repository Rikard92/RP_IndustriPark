using RP_IndustriPark.Shared;

namespace RP_IndustriPark.Services
{
    public interface IIndustryClient
    {
        Task<IEnumerable<Device>?> GetAsync();
        Task<Device?> PostAsync(Device device);
        Task<bool> ToggleAsync(Device device);
        Task<bool> RemoveAsync(string id);
    }
}
