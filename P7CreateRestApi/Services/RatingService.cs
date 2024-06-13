using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Models.OutputModel;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository) 
        { 
            _ratingRepository = ratingRepository;
        }

        public RatingOutputModel? Create(RatingInputModel inputModel)
        {
            var rating = new Rating
            {
                MoodysRating = inputModel.MoodysRating,
                SandPRating = inputModel.SandPRating,
                FitchRating = inputModel.FitchRating,
                OrderNumber = inputModel.OrderNumber
            };
            _ratingRepository.Create(rating);
            return ToOutputModel(rating);
        }

        public RatingOutputModel? Delete(int id)
        {
            var rating = _ratingRepository.Delete(id);
            if (rating is not null)
            {
                return ToOutputModel(rating);
            }
            return null;
        }

        public RatingOutputModel? Get(int id)
        {
            var rating = _ratingRepository.Get(id);
            if (rating is not null)
            {
                return ToOutputModel(rating);
            }
            return null;
        }

        public List<RatingOutputModel> List()
        {
            var list = new List<RatingOutputModel>();
            var ratings = _ratingRepository.List();
            foreach (var rating in ratings)
            {
                list.Add(ToOutputModel(rating));
            }
            return list;
        }

        public RatingOutputModel? Update(int id, RatingInputModel inputModel)
        {
            var rating = _ratingRepository.Update(new Rating
            {
                Id = id,
                MoodysRating = inputModel.MoodysRating,
                SandPRating = inputModel.SandPRating,
                FitchRating = inputModel.FitchRating,
                OrderNumber = inputModel.OrderNumber,
            });
            if (rating is not null)
            {
                return ToOutputModel(rating);
            }
            return null;
        }

        private RatingOutputModel ToOutputModel(Rating rating) =>
            new RatingOutputModel
            {
                Id = rating.Id,
                MoodysRating = rating.MoodysRating,
                SandPRating = rating.SandPRating,
                FitchRating = rating.FitchRating,
                OrderNumber = rating.OrderNumber
            };
    }
}
