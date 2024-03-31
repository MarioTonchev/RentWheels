using Microsoft.EntityFrameworkCore;
using RentWheels.Core.Contracts;
using RentWheels.Core.ViewModels.Review;
using RentWheels.Infrastructure.Common;
using RentWheels.Infrastructure.Models;

namespace RentWheels.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;

        public ReviewService(IRepository _repository)
        {
            repository = _repository;
        }

		public async Task<IEnumerable<ReviewAllViewModel>> GetForCarAsync(int id)
        {
            var reviews = await repository.AllAsReadOnly<Review>().Where(r => r.CarId == id).Select(r => new ReviewAllViewModel()
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CarId = r.CarId
            }).ToListAsync();

            return reviews;
        }

		public async Task AddAsync(ReviewFormViewModel model, string reviewerId)
		{
            var review = new Review()
            {
                Rating = model.Rating,
                Comment = model.Comment,
                CarId = model.CarId,
                ReviewerId = reviewerId
            };

            await repository.AddAsync(review);
            await repository.SaveChangesAsync();
		}
    }
}
