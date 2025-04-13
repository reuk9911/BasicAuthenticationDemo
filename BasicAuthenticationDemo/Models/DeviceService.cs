using Microsoft.EntityFrameworkCore;

namespace BasicAuthenticationDemo.Models
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

    public class DeviceService : IDeviceService
    {
        private readonly UserDbContext _context;

        public DeviceService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices.AsNoTracking().ToListAsync();
        }

        public async Task<Device?> GetAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }

        public async Task<Device> CreateAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return false;

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteByTypeAsync(string type)
        {

            var del = _context.Devices.Where(x => x.Type == type).Take<Device>(10000).ToList();
            _context.Devices.RemoveRange(del);
            do
            {
                del = _context.Devices.Where(x => x.Type == type).Take<Device>(10000).ToList();
                _context.Devices.RemoveRange(del);
                await _context.SaveChangesAsync();
            }
            while (del.Count != 0);

            return true;
        }


        public async Task<bool> UpdateAsync(Device device)
        {
            var existingDevice = await _context.Devices.FindAsync(device.Id);
            if (existingDevice == null)
            {
                return false;
            }

            existingDevice.Type = device.Type;

            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
