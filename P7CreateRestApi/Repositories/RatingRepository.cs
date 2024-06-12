using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Controllers.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private LocalDbContext _context;

        public RatingRepository(LocalDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllRating()
        {
            var rating = await _context.Ratings.ToListAsync();

            if (rating == null)
            {
                throw new Exception("Rating does not exist.");
            }

            return rating;
        }

        public async Task Add(Rating rating)
        {
            if (RatingExists(rating.Id))
            {
                throw new Exception("Rating already exists.");
            }

            _context.Ratings.Add(rating);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Rating rating)
        {
            if (!RatingExists(rating.Id))
            {
                throw new Exception("Rating does not exist.");
            }

            _context.Ratings.Update(rating);

            await _context.SaveChangesAsync();
        }

        public async Task<Rating> GetRatingId(int id)
        {

            var rat = await _context.Ratings.FindAsync(id);

            if (rat == null)
            {
                throw new Exception("Rating does not exists.");
            }

            return rat;
        }

        public async Task Delete(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating != null)
            {
                _context.Ratings.Remove(rating);

                await _context.SaveChangesAsync();
            }
        }

        public bool RatingExists(int id)
        {
            return _context.Ratings.Any(r => r.Id == id);
        }
    }
}
