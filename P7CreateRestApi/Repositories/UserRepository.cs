using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _dbContext;

        public UserRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> FindAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User? FindById(int id) => _dbContext.Users.FirstOrDefault(user => user.Id == id);

        public User? FindByUserName(string username) => _dbContext.Users.FirstOrDefault(user => user.UserName == username);
    }
}