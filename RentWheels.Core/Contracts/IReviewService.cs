using RentWheels.Core.ViewModels.Review;

namespace RentWheels.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewAllViewModel>> GetForCarAsync(int id);

        Task AddAsync(ReviewFormViewModel model, string reviewerId);

        Task<bool> ReviewExistsAsync(int reviewId);

		Task<bool> HasReviewerWithIdAsync(int reviewId, string reviewerId);

        Task RemoveReviewAsync(int reviewId);

        Task<int> GetReviewCarIdAsync(int reviewId);
	}
}
