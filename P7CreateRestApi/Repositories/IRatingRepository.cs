using Dot.Net.WebApi.Controllers.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRating();

        Task Add(Rating rating);

        Task Update(Rating rating);

        Task<Rating> GetRatingId(int id);

        Task Delete(int id);
    }
}
