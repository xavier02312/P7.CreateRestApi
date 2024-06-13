using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _dbContext;
        public RatingRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Rating rating)
        {
            _dbContext.Ratings.Add(rating);
            _dbContext.SaveChanges();
        }

        public Rating? Delete(int id)
        {
            var rating = _dbContext.Ratings.FirstOrDefault(r => r.Id == id);
            if (rating is not null)
            {
                _dbContext.Ratings.Remove(rating);
                _dbContext.SaveChanges();
            }
            return rating;
        }

        public Rating? Get(int id) => _dbContext.Ratings.FirstOrDefault(r => r.Id == id);

        public List<Rating> List() => _dbContext.Ratings.ToList();

        public Rating? Update(Rating rating)
        {
            var ratingAModifier = _dbContext.Ratings.FirstOrDefault(r => r.Id == rating.Id);
            if (ratingAModifier is not null)
            {
                ratingAModifier.MoodysRating = rating.MoodysRating;
                ratingAModifier.SandPRating = rating.SandPRating;
                ratingAModifier.FitchRating = rating.FitchRating;
                ratingAModifier.OrderNumber = rating.OrderNumber;
                _dbContext.SaveChanges();
            }
            return ratingAModifier;
        }
    }
}
