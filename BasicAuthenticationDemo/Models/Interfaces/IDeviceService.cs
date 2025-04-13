namespace BasicAuthenticationDemo.Models.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetAsync(int id);
        Task<Device> CreateAsync(Device device);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Device device);
        Task<bool> DeleteByTypeAsync(string type);
    }
}
