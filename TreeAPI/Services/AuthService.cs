using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreeAPI.Context;
using TreeAPI.Models;

namespace TreeAPI.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string login, string password);
        Task<bool> UserIsExist(string name);

    }
    public class AuthService : IAuthService
    {
        private readonly TreeDbContext _context;

        public AuthService(TreeDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePassword(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        
        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }


        }


                public async Task<User> Login(string login, string password)
        {
            var worker = await _context.Users.FirstOrDefaultAsync(w => w.Name == login);

            if (worker == null)
                return null;

            if (!VerifyPasswordHash(password, worker.PasswordHash, worker.PasswordSalt))
                return null;

            return worker;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computtedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computtedHash.Length; i++)
                {
                    if (computtedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }

                return true;

            }
        }

                public async Task<bool> UserIsExist(string name)
        {
            if (await _context.Users.AnyAsync(x => x.Name == name))
            {
                return true;
            }


            return false;
        }



    }
}