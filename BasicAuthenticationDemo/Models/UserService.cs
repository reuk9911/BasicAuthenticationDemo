using Microsoft.EntityFrameworkCore;
using BasicAuthenticationDemo.Models.Interfaces;

namespace BasicAuthenticationDemo.Models
{
    

    public class UserService : IUserService
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly UserDbContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public UserService(UserDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает коллекцию всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Возвращает пользователя по id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns></returns>
        public async Task<User?> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Записывает нового пользователя в БД
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <returns>Возвращает нового пользователя</returns>
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <param name="user">Измененный пользователь</param>
        /// <returns>true, если пользователь с данным id существует, false иначе</returns>
        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <param name="id"> id пользователя</param>
        /// <returns>true, если пользователь с данным id существует, false иначе</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Ищет пользователя по email и паролю 
        /// </summary>
        /// <param name="email">email пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns>Пользователь с заданным email и паролем</returns>
        public async Task<User?> ValidateAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
