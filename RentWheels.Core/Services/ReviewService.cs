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
				CarId = r.CarId,
				ReviewerId = r.ReviewerId
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

		public async Task<bool> ReviewExistsAsync(int reviewId)
		{
			return await repository.AllAsReadOnly<Review>().AnyAsync(r => r.Id == reviewId);
		}

		public async Task<bool> HasReviewerWithIdAsync(int reviewId, string reviewerId)
		{
			return await repository.AllAsReadOnly<Review>().AnyAsync(r => r.Id == reviewId && r.ReviewerId == reviewerId);
		}

		public async Task RemoveReviewAsync(int reviewId)
		{
			var review = await repository.All<Review>().Where(r => r.Id == reviewId).FirstOrDefaultAsync();

			if (review != null)
			{
				repository.Delete(review);
				await repository.SaveChangesAsync();
			}
		}

		public async Task<int> GetReviewCarIdAsync(int reviewId)
		{
			var review = await repository.AllAsReadOnly<Review>().Where(r => r.Id == reviewId).FirstOrDefaultAsync();
			
			return review.CarId;			
		}

		public async Task<ReviewFormViewModel> CreateReviewFormViewModelByIdAsync(int id)
		{
			return await repository.AllAsReadOnly<Review>().Where(r => r.Id == id).Select(r => new ReviewFormViewModel()
			{
				Rating = r.Rating,
				Comment = r.Comment,
				CarId = r.CarId
			}).FirstOrDefaultAsync();
		}

		public async Task EditAsync(int id, ReviewFormViewModel model)
		{
			var reviewToEdit = await repository.All<Review>().Where(r => r.Id == id).FirstOrDefaultAsync();

			if (reviewToEdit != null)
			{
				reviewToEdit.Rating = model.Rating;
				reviewToEdit.Comment = model.Comment;

				await repository.SaveChangesAsync();
			}
		}
	}
}
