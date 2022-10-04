namespace RP_IndustriPark.Services
{
    public interface IIndustryClient
    {
        Task<IEnumerable<MachineIndustry>?> GetAsync();
        //Task<MachineIndustry?> PostAsync(CreateItem createItem);
    }
}
