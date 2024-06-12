using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) 
            {
                throw new Exception("User does not exist.");
            }
            return user;
        }

        public async Task<User> FindByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }
    }
}