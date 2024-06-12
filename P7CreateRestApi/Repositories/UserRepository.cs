using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    /*public class UserRepository
    {
        private LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        public User FindByUserName(string userName)
        {
            return _context.Users.Where(user => user.UserName == userName)
                                  .FirstOrDefault();
        }

        public async Task<List<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public void Add(User user)
        {
        }

        public User FindById(int id)
        {
            return null;
        }
    }*/
}