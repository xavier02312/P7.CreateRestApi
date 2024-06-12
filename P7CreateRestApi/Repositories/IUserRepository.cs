using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> FindAll();

        Task Add(User user);

        Task<User> FindById(int id);

        Task<User> FindByUserName(string userName);
    }
}
