using Microsoft.EntityFrameworkCore;
using BasicAuthenticationDemo.Models.Interfaces;

namespace BasicAuthenticationDemo.Models
{
    

    public class DeviceService : IDeviceService
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly UserDbContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public DeviceService(UserDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает коллекцию всех устройств
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices.AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// Возвращает устройство по id
        /// </summary>
        /// <param name="id">id устройства</param>
        /// <returns></returns>
        public async Task<Device?> GetAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }

        /// <summary>
        /// Записывает новое устройство в БД
        /// </summary>
        /// <param name="user">Новое устройство</param>
        /// <returns>Возвращает новое устройство</returns>
        public async Task<Device> CreateAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        /// <summary>
        /// Обновляет данные устройства
        /// </summary>
        /// <param name="user">Измененное устройство</param>
        /// <returns>true, если устройство с данным id существует, false иначе</returns>
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

        /// <summary>
        /// Удаляет устройство по id
        /// </summary>
        /// <param name="id"> id устройства</param>
        /// <returns>true, если устройство с данным id существует, false иначе</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return false;

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удаляет все устройства с заданным типом 
        /// </summary>
        /// <param name="type">Тип устройства</param>
        /// <returns>true, если устройства с заданным типом нашлись и удалились, false иначе</returns>
        public async Task<bool> DeleteByTypeAsync(string type)
        {

            var del = _context.Devices.Where(x => x.Type == type).Take<Device>(10000).ToList();
            if (del.Count == 0)
                return false;
            _context.Devices.RemoveRange(del);
            await _context.SaveChangesAsync();
            
            return true;
        }


        

        
    }
}
